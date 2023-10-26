using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace LakeXplorer.Security;
public class TokenService
{
    // A static key for signing and verifying JWT tokens
    public static SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Generate256BitKey());
    private readonly int _tokenExpirationInMinutes;

    public TokenService(int tokenExpirationInMinutes)
    {
        _tokenExpirationInMinutes = tokenExpirationInMinutes;
    }

    // Generates a 256-bit random key
    public static byte[] Generate256BitKey()
    {
        using (var randomNumberGenerator = RandomNumberGenerator.Create())
        {
            var key = new byte[32]; // 256 bitë (32 bajtë)
            randomNumberGenerator.GetBytes(key);
            return key;
        }
    }

    // Generates a JWT token for the given username
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


    // Generates a random secret key with the specified size
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