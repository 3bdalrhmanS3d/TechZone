using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechZone.Api.DTOs.Auth;
using TechZone.Api.Services.Interfaces;
using TechZone.Core.models;

namespace TechZone.Api.Services.Implementations
{
    public class AuthService(UserManager<ApplicationUser> userManager, IOptions<helper.JWT> jwt) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly helper.JWT _jwt = jwt.Value;

        public async Task<AuthDto> RegisterAsync(RegisterDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) is not null)
            {
                return new AuthDto
                {
                    Message = "Email is already registered",
                    IsAuthenticated = false
                };
            }
            //if the user name is already registered
            if (await _userManager.FindByNameAsync(dto.UserName) is not null)
            {
                return new AuthDto
                {
                    Message = "Username is already registered",
                    IsAuthenticated = false
                };
            }
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthDto
                {
                    Message = errors,
                    IsAuthenticated = false
                };
            }
            await _userManager.AddToRoleAsync(user, "User");
            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthDto
            {
                Email = user.Email,
                Expiration = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }
        public async Task<AuthDto> GetTokenAsync(TokenRequestDto dto)
        {
            var authModel = new AuthDto();

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.Expiration = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            return authModel;
        }
        public async Task<string> AddRoleAsync(AddRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(dto.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, dto.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, dto.Role);

            return result.Succeeded ? string.Empty : "Sonething went wrong";
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.ExpiryInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
    
}
