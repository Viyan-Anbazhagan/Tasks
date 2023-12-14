using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EmployeeManagementSystem.Models;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Controllers;

public class HomeController : Controller
{
    Repository repository=new Repository();
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [HttpGet]
    public IActionResult Login(){
        return View();
    }

    [HttpPost]
    public IActionResult Login(User user){

        bool flag = repository.login(user);
        if(flag){
            string role =repository.GetRole(user);
            if(role.Equals("Admin")){
                HttpContext.Session.SetString("Username",Convert.ToString(user.Username));
                return RedirectToAction("Index","Admin");
            }
            else if(role.Equals("Manager")){
                HttpContext.Session.SetString("Username",Convert.ToString(user.Username));
                return RedirectToAction("Index","Manager");
            }
            else if(role.Equals("SystemAdmin")){
                HttpContext.Session.SetString("Username",Convert.ToString(user.Username));
                return RedirectToAction("Index","SystemAdmin");
            }
            else if(role.Equals("Employee")){
                HttpContext.Session.SetString("Username",Convert.ToString(user.Username));
                return RedirectToAction("Index","Employee");
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

        string flag = repository.change(user);
        if(flag.Equals("exist")){
            ViewBag.message="Invalid Username";
            return View();
        }
        else if(flag.Equals("dismatch")){
            ViewBag.message="Password doesn't match Confirmpassword";
            return View();
        }
        else if(flag.Equals("success")){
            return RedirectToAction("Login");
        }
        return View();

    }
     
     public IActionResult Thanks()
    {
        return View();
    }
}
