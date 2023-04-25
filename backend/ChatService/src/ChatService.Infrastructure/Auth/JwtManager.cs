using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChatService.Domain.Auth;
using ChatService.Domain.Entities;
using ChatService.Infrastructure.Support.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChatService.Infrastructure.Auth;

public class JwtManager : IJwtManager
{
    private readonly JwtSettings _jwtSettings;
    
    public JwtManager(IOptions<JwtSettings> options)
    {
        _jwtSettings = options.Value;
    }
    public string GetJwt(User user)
    {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, _jwtSettings.Subject),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim("UserName", user.Name),
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddHours(10),
            signingCredentials: signIn);

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        
        return jwtSecurityTokenHandler.WriteToken(token);
        
    }
}