using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WorckTracker.Services;

[ApiController]
[Route("api/[controller]")]
public class JwtController : ControllerBase
{
    private readonly JwtGenerator _jwtGenerator;

    public JwtController(JwtGenerator jwtGenerator)
    {
        _jwtGenerator = jwtGenerator;
    }

    [HttpPost]
    public ActionResult<string> Generate([FromBody] JwtRequest request)
    {
        // var jwt = _jwtGenerator.GenerateJwt(request.Claims, DateTime.Now.AddHours(1), "password", request.Issuer, request.Audience);
        // var jwt = _jwtGenerator.GenerateJwt(request.Claims, DateTime.Now.AddHours(1), "password");
        var jwt = _jwtGenerator.GenerateJwt(new[] { "name:John Doe", "role:Administrator" }, DateTime.Now.AddHours(1), "f0G6vY8RUu$7MhD9XuV7q3#2FtZ5gH1j"); 
        return Content(jwt);
    }

    [HttpGet("{jwt:length(128)}")]
    public ActionResult<IEnumerable<Claim>> DecodeJWT(string jwt)
    {
        Console.WriteLine("JWT is valid");
        if (jwt is null){
            return Content("Nothing");
        
        
        }
        string secret ="f0G6vY8RUu$7MhD9XuV7q3#2FtZ5gH1j";
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        JwtSecurityToken token = handler.ReadJwtToken(jwt);
        // Validate the JWT using the secret
        ClaimsPrincipal principal = handler.ValidateToken(jwt, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ValidateLifetime = true,
        }, out SecurityToken validatedToken);
        if (principal != null)
        {
            // The JWT is valid
            Console.WriteLine("JWT is valid");

            // Get the claims (or payload) from the JWT
            var claims = principal.Claims;

            // Print the claims
        
            return Ok(claims);
        }
        else
        {
            // The JWT is invalid
            Console.WriteLine("JWT is invalid");
            return NoContent();
        }












        // JwtSecurityToken token = new JwtSecurityToken(jwt);

        // var claims = token.Claims;

        // return Ok(claims);


    }

}

public class JwtRequest
{
    public string[] Claims { get; set; }
    public DateTime Expires { get; set; }
    public string SigningKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}