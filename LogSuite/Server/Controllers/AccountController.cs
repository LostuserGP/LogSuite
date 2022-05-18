using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using LogSuite.DataAccess;
using LogSuite.Server.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Authorization;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using LogSuite.Shared.Models;
using LogSuite.Server.Services.IServices;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using LogSuite.Business;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using LogSuite.DataAccess.References;

namespace LogSuite.Server.Controllers
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
        private readonly IConfiguration _configuration;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<APISettings> options,
            IMapper mapper,
            IMailService mailService,
            IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _apiSettings = options.Value;
            _mapper = mapper;
            _mailService = mailService;
            _configuration = configuration;
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

                //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");
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
            //var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}
            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            //var userRoles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
            //foreach (var userRole in userRoles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, userRole));
            //    var role = await _roleManager.FindByNameAsync(userRole);
            //    if (role != null)
            //    {
            //        var roleClaims = await _roleManager.GetClaimsAsync(role);
            //        foreach (Claim roleClaim in roleClaims)
            //        {
            //            claims.Add(roleClaim);
            //        }
            //    }
            //}
            return claims;
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDetailDTO accountDTO)
        {
            if (accountDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!(await IsUnique(accountDTO)))
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "User is not unique",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var generatedPassword = GenerateRandomPassword();
            var user = new ApplicationUser()
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

            if (accountDTO.Roles.Any())
            {
                var roleResult = await _userManager.AddToRolesAsync(user, accountDTO.Roles);
                if (!roleResult.Succeeded)
                {
                    var errors = roleResult.Errors.Select(e => e.Description);
                    return BadRequest(new RegistrationResponseDTO { Errors = errors, IsRegistrationSuccessful = false });
                }
            }
            else
            {
                var roleResult = await _userManager.AddToRoleAsync(user, SD.Role_User);
                if (!roleResult.Succeeded)
                {
                    var errors = roleResult.Errors.Select(e => e.Description);
                    return BadRequest(new RegistrationResponseDTO { Errors = errors, IsRegistrationSuccessful = false });
                }
            }            
            var createdUser = await _userManager.FindByNameAsync(user.Name);
            var site = _configuration.GetValue<string>("RiskSuite_Client_URL");
            var link = site + "/loginwa?user=" + accountDTO.Email + "&p=" + generatedPassword;
            //callback = Url.
            var htmlMessage = "Have been created account for <a href='" + HtmlEncoder.Default.Encode(site) + "'>Risk Suite</a>.<br/>" + 
                "Username: " + accountDTO.Email + "<br/>" + 
                "Password: " + generatedPassword + "<br/>" +
                "You can login by <a href='" + HtmlEncoder.Default.Encode(link) + "'>clicking here</a>.";
            await _mailService.SendEmailAsync(user.Email, "Link for login", htmlMessage);

            return StatusCode(201);
        }

        private static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 10,
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

        //[Authorize(Roles = SD.Role_Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] Params parameters)
        {

            var source = _userManager.Users
                    .Include(x => x.Department)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result = await PagedList<ApplicationUser>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            List<UserDetailDTO> users = new List<UserDetailDTO>();
            foreach (var user in result)
            {
                var roles = (await _userManager.GetRolesAsync(user)).ToList();
                users.Add(new UserDetailDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Department = _mapper.Map<DepartmentDTO>(user.Department),
                    Roles = roles
                });
            };
            var pagedUsers = new PagedList<UserDetailDTO>(users, result.MetaData);
            //var departments = pagedUsers.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedUsers.MetaData));
            
            return Ok(users);
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpGet("{accountId}")]
        public async Task<IActionResult> Get(string accountId)
        {
            if (accountId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Account Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var account = await _userManager.Users
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == accountId);
            if (account == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Account Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            var accountDTO = new UserDetailDTO()
            {
                Id = account.Id,
                Name = account.Name,
                Email = account.Email,
                Roles = (await _userManager.GetRolesAsync(account)).ToList(),
                DepartmentId = account.DepartmentId,
                Department = _mapper.Map<Department, DepartmentDTO>(account.Department)
            };
            return Ok(accountDTO);
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var identityRoles = await _roleManager.Roles.ToListAsync();
            List<string> roles = new List<string>();
            foreach (var role in identityRoles)
            {
                roles.Add(role.Name);
            }
            return Ok(roles);
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpPut("{accountId}")]
        public async Task<IActionResult> Update([FromBody] UserDetailDTO accountDTO, string accountId)
        {
            if (accountDTO == null || !ModelState.IsValid || accountId == null)
            {
                return BadRequest();
            }
            if (accountDTO.Id != accountId)
            {
                return BadRequest();
            }
            if (!(await IsUnique(accountDTO)))
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "User is not unique",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var userFromDB = await _userManager.FindByIdAsync(accountId);
            var existingRoles = await _userManager.GetRolesAsync(userFromDB);
            List<string> rolesToDelete = existingRoles
                .Where(c1 => accountDTO.Roles.All(c2 => c2 != c1)).ToList();
            List<string> rolesToAdd = accountDTO.Roles
                .Where(c1 => existingRoles.All(c2 => c2 != c1)).ToList();
            try
            {
                await _userManager.RemoveFromRolesAsync(userFromDB, rolesToDelete);
                await _userManager.AddToRolesAsync(userFromDB, rolesToAdd);
                userFromDB.Email = accountDTO.Email;
                userFromDB.Name = accountDTO.Name;
                userFromDB.DepartmentId = accountDTO.DepartmentId;
                await _userManager.UpdateAsync(userFromDB);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<bool> IsUnique(UserDetailDTO accountDTO)
        {
            var user = await _userManager.Users
                .Where(x => x.Id != accountDTO.Id && 
                (x.Name.ToLower() == accountDTO.Name.ToLower() 
                || x.Email.ToLower() == accountDTO.Email.ToLower()))
                .FirstOrDefaultAsync();
            if (user != null)
            {
                return false;
            }
            return true;
        }
    }
}
