using EFInheritance.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFInheritance.Controllers;

[ApiController]
[Route("[controller]")]
public class EFController : ControllerBase
{
    private readonly AppDbContext _context;
    public EFController(AppDbContext context){
        _context=context;
    } 
    [HttpPost("Register Student")]
    public IActionResult AddStudent(Student student){
        _context.People.Add(student);
        _context.SaveChanges();
        return Ok(student);
    }
    [HttpPost("Register Teacher")]
    public IActionResult AddTeacher(Teacher teacher){
        _context.People.Add(teacher);
        _context.SaveChanges();
        return Ok(teacher);
    }
    [HttpGet("GetData")]
    public IActionResult GetData(){
        var data = _context.People.ToList();
        return Ok(data);
    }
}
