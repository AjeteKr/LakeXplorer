using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace LakeXplorer.Security;
public class TokenService
{
    public static SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Generate256BitKey());
    private readonly int _tokenExpirationInMinutes;

    public TokenService(int tokenExpirationInMinutes)
    {
        _tokenExpirationInMinutes = tokenExpirationInMinutes;
    }

    public static byte[] Generate256BitKey()
    {
        using (var randomNumberGenerator = RandomNumberGenerator.Create())
        {
            var key = new byte[32]; 
            randomNumberGenerator.GetBytes(key);
            return key;
        }
    }

    public string GenerateToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_tokenExpirationInMinutes),
            SigningCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


    public string GenerateRandomSecretKey(int keySize)
    {
        using (var randomNumberGenerator = RandomNumberGenerator.Create())
        {
            var key = new byte[keySize / 8]; 
            randomNumberGenerator.GetBytes(key);
            return Convert.ToBase64String(key);
        }
    }
}