using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PaperUniverse.Core.Contexts;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate;

namespace PaperUniverse.Api.Extensions;

public static class JwtExtensions
{
    public static string Generate(Response.ResponseData user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var crendetials = new SigningCredentials(new SymmetricSecurityKey(key), 
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = crendetials
        };

        var token = handler.CreateToken(tokenDescriptor);
        
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(Response.ResponseData user)
    {
        var claimsIdentity = new ClaimsIdentity();
        
        claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        
        return claimsIdentity;
    }
}