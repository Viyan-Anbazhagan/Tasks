using LoginAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginAPI;
[Authorize]
[ApiController]
[Route("[controller]")]
public class DataController:ControllerBase
{
     private readonly AppDbContext _context;
    public DataController(AppDbContext context, IConfiguration configuration){
        _context=context;
    }
    
    [HttpGet("GetData")]
    public IActionResult GetData(){
        var data = _context.UserCredentials.ToList();
        return Ok(data);
    }
}
