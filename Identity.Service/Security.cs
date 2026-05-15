using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Identity.Service.Models;

namespace Identity.Service.Security;

public class TokenGenerator
{
    private readonly ECDsa _privateKey;
    private readonly ECDsaSecurityKey _securityKey;

    public TokenGenerator()
    {
        var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP521);
        _privateKey = ecdsa;
        _securityKey = new ECDsaSecurityKey(_privateKey);
    }

    public TokenResponse Generate(UserAccount user)
    {
        var handler = new JwtSecurityTokenHandler();

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(
                new[]
                {
                    new System.Security.Claims.Claim("sub", user.Id),
                    new System.Security.Claims.Claim("username", user.Username),
                    new System.Security.Claims.Claim("roles", string.Join(" , ", user.Roles))
                }
            ),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.EcdsaSha512)
        };

        var token = handler.CreateToken(descriptor);

        return new TokenResponse
        {
            Token = handler.WriteToken(token),
            ExpiresAt = descriptor.Expires!.Value
        };
    }
}