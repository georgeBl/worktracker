using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WorckTracker.Services;


public class JwtGenerator
{
    public string GenerateJwt(string[] claims, DateTime expires, string signingKey, string? issuer = null, string? audience = null)
    {
        // Set the signing key and the claims for the token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        var tokenClaims = new List<Claim>();
        foreach (var claim in claims)
        {
            var parts = claim.Split(':');
            tokenClaims.Add(new Claim(parts[0], parts[1]));
        }

        // Create a JWT security token
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: tokenClaims,
            expires: expires,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        // Serialize the JWT and return it as a string
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }
}

// how to use:
// var jwt = GenerateJwt(new[] { "name:John Doe", "role:Administrator" }, DateTime.Now.AddHours(1), "my-secret-key", "http://mysite.com", "http://mysite.com");
// Console.WriteLine(jwt);
// var jwt = GenerateJwt(new[] { "name:John Doe", "role:Administrator" }, DateTime.Now.AddHours(1), "my-secret-key");