using LoginAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configration;
    public LoginController(AppDbContext context, IConfiguration configuration){
        _context=context;
        _configration = configuration;
    }
    [HttpPost("Login")]
    public IActionResult Login(UserCredential userCredential){
        var data = _context.UserCredentials.FirstOrDefault(u=>u.Username==userCredential.Username&&u.Password==userCredential.Password);
        if(data==null){
            return Unauthorized();
        }
        var secrectkey = _configration["JWT:secretkey"];
        Console.WriteLine(secrectkey);
        var tokenhandler = new JwtSecurityTokenHandler();
        var tokenkey = Encoding.UTF8.GetBytes(secrectkey);
        var tokendesc = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity ( new Claim[] { new Claim(ClaimTypes.Name, userCredential.Username)} ),
            Expires = DateTime.Now.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey),SecurityAlgorithms.HmacSha256) 
        };
        var token = tokenhandler.CreateToken(tokendesc);
        string finaltoken = tokenhandler.WriteToken(token);
        return Ok(finaltoken);
    }
    [HttpPost("Register")]
    public IActionResult Register(UserCredential userCredential){
        _context.UserCredentials.Add(userCredential);
        _context.SaveChanges();
        return Ok(userCredential);
    }
    
}
