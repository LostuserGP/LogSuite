using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RiskSuite.DataAccess;
using RiskSuite.Server.Helpers;
using RiskSuite.Shared.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RiskSuite.Server.Controllers
{
    [Authorize(AuthenticationSchemes = NegotiateDefaults.AuthenticationScheme)]
    //[AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class WindowsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly APISettings _apiSettings;

        public WindowsController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<APISettings> options)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _apiSettings = options.Value;
        }

        //[Authorize(AuthenticationSchemes = NegotiateDefaults.AuthenticationScheme)]
        //[AllowAnonymous]
        [Route("GetToken")]
        [HttpPost]
        public async Task<IActionResult> GetToken()
        {
            //string userName = HttpContext.User.Identity.Name;
            var userName = User.Identity.Name;
            userName = userName.Substring(userName.IndexOf(@"\") + 1);
            userName = userName + "@lostuser.ru";
            var user = await _userManager.FindByNameAsync(userName);
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
                    Email = user.Email
                    //Department = user.Department
                }
            });
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
    }
}
