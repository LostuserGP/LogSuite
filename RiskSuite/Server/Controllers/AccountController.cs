using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RiskSuite.DataAccess;
using RiskSuite.Server.Helpers;
using RiskSuite.Shared;
using RiskSuite.Shared.Authorization;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Negotiate;
using AutoMapper;
using RiskSuite.Shared.Models;
using RiskSuite.Server.Services.IServices;
using System.Text.Encodings.Web;

namespace RiskSuite.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly APISettings _apiSettings;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<APISettings> options,
            IMapper mapper,
            IMailService mailService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _apiSettings = options.Value;
            _mapper = mapper;
            _mailService = mailService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserRequestDTO userRequestDTO)
        {
            if (userRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new ApplicationUser
            {
                UserName = userRequestDTO.Email,
                Email = userRequestDTO.Email,
                Name = userRequestDTO.Name,
                DepartmentId = userRequestDTO.DepartmentId,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRequestDTO.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDTO { Errors = errors, IsRegistrationSuccessful = false });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, SD.Role_User);
            if (!roleResult.Succeeded)
            {
                var errors = roleResult.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDTO { Errors = errors, IsRegistrationSuccessful = false });
            }

            return StatusCode(201);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationDTO authenticationDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(authenticationDTO.UserName, authenticationDTO.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(authenticationDTO.UserName);
                if (user == null)
                {
                    return Unauthorized(new AuthenticationResponseDTO
                    {
                        IsAuthSuccessful = false,
                        ErrorMessage = "Invalid Authentication"
                    });
                }

                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims(user);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _apiSettings.ValidIssuer,
                    audience: _apiSettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(_apiSettings.ExpirationDays),
                    signingCredentials: signingCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new AuthenticationResponseDTO
                {
                    IsAuthSuccessful = true,
                    Token = token,
                    UserDTO = new UserDTO
                    {
                        Name = user.Name,
                        Id = user.Id,
                        Email = user.Email,
                        Department = _mapper.Map<Department, DepartmentDTO>(user.Department)
                    }
                });
            }
            else
            {
                return Unauthorized(new AuthenticationResponseDTO
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Invalid Authentication"
                });
            }
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id),
            };
            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRequestDTO accountDTO)
        {
            if (accountDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var generatedPassword = GenerateRandomPassword();
            var user = new ApplicationUser
            {
                UserName = accountDTO.Email,
                Email = accountDTO.Email,
                Name = accountDTO.Name,
                DepartmentId = accountDTO.DepartmentId,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, generatedPassword);
            

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDTO { Errors = errors, IsRegistrationSuccessful = false });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, SD.Role_User);
            if (!roleResult.Succeeded)
            {
                var errors = roleResult.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDTO { Errors = errors, IsRegistrationSuccessful = false });
            }
            var createdUser = await _userManager.FindByNameAsync(user.Name);
            var token = _userManager.GeneratePasswordResetTokenAsync(createdUser);
            var callback = Url.Action("/loginwa", "Account", new { token, email = user.Email }, Request.Scheme);
            var htmlMessage = "You can login by <a href='" + HtmlEncoder.Default.Encode(callback) + "'>clicking here</a>.";
            await _mailService.SendEmailAsync(user.Email, "Link for login", htmlMessage);

            return StatusCode(201);
        }

        private static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
