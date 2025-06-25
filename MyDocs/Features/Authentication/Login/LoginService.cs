using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyDocs.Features.Authentication.Login
{
    public class LoginService : ILoginService
    {
        private readonly Context _context;
        private readonly JwtSettings _jwtSettings;
        public LoginService(Context context, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResponse> GenerateToken(LoginRequest loginRequest)
        {
            bool userExists = await UserExists(loginRequest);
            if (!userExists)
                throw new Exception("Login ou senha incorretos");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, loginRequest.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                    signingCredentials: credentials
                );

            return new LoginResponse(new JwtSecurityTokenHandler().WriteToken(token));
        }

        private async Task<bool> UserExists(LoginRequest loginRequest)
        {
            return await _context
                    .UsersCredentials
                    .AnyAsync(uc => uc.Email == loginRequest.Email && uc.Password == loginRequest.Password);
        }
    }
}
