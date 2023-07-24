using LivrariaFuturo.Core.Helpers;
using LivrariaFuturo.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LivrariaFuturo.Application.Services
{
    public interface IAuthService
    {
        Task<UserModel?> Authenticate(string email, string password);
        Task<string> GenerateJwtToken(UserModel user);
    }


    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthService(IUserService userService, IConfiguration configuration)
        {
            this._userService = userService;

            this._configuration = configuration;
        }

        public async Task<UserModel?> Authenticate(string username, string password)
        {
            var user = await this._userService.GetByUsername(username);

            if (user == null) return null;

            if (!user.password.Equals(password.ToMd5Hash()))
                return null;

            return user;
        }
        
        public async Task<string> GenerateJwtToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Jwt:Secret"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var audiences = this._configuration.GetSection("Jwt:Audiences").Get<List<string>>();
            var version = this._configuration["Jwt:Version"]!;

            var claims = new List<Claim>()
            {
                new Claim("nameIdentifier", user.email.ToBase64Encode()),
                new Claim("name", user.name.ToBase64Encode()),
                new Claim("sid", user.id.ToString()),
            };

            if (audiences == null)
                audiences = new List<string>();

            foreach (var audience in audiences)
                claims.Add(new Claim("aud", audience));

            var token = new JwtSecurityToken(
                this._configuration["Jwt:Issuer"],
                null,
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
