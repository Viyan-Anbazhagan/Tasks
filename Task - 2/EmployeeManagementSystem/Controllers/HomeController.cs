using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace EmployeeManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository _repository;
    public HomeController(ILogger<HomeController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login(){
        string username=HttpContext.Session.GetString("username");
        if(!string.IsNullOrEmpty(username)){
           string role = _repository.GetRole(username);
           return RedirectToAction("Index",$"{role}");        
        }
        else{
            return View();    
        }
    }

    [HttpPost]
    public IActionResult Login(User user){
        if(_repository.AuthenticateUser(user)){
            string role =_repository.GetRole(user.Username);
            if(role!=null){
                HttpContext.Session.SetString("Username",Convert.ToString(user.Username));
                TempData["SuccessMessage"]="Log In Successfull";
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, role),
                };
                
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index",$"{role}");
            }
            else{
                return View();
            }
        }
        else{
            ViewBag.message="Invalid credentials";
            return View();
        }

    }
    [HttpGet]
    public IActionResult Forgotpassword(){
        return View();
    }

    [HttpPost]
    public IActionResult Forgotpassword(User user){

        string flag = _repository.ChangePassword(user);
        if(flag.Equals("exist")){
            ViewBag.message="Invalid Username";
            return View();
        }
        else if(flag.Equals("dismatch")){
            ViewBag.message="Password doesn't match Confirmpassword";
            return View();
        }
        else if(flag.Equals("success")){
            ViewBag.message="Password Changed";
            return View("Login");
        }
        return View();

    }
    public IActionResult Logout(){
        HttpContext.Session.Remove("Username");
        return RedirectToAction("Login");
    }
     
}
