using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Controllers;

public class AdminController : Controller
{
    Repository repository=new Repository();
    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(){
        return View();
    }

    public IActionResult UserDetails(){
        string? Username= HttpContext.Session.GetString("Username");
        return View(repository.display(Username));
    }
    [HttpGet]
    public IActionResult EmployeeDetails(){
        string Role = "Employee";
        List<User> userlist=repository.Display(Role);
        return View(userlist);
    }

    [HttpPost]
    public IActionResult EmployeeDetails(string username){
        User user= repository.display(username);
        return RedirectToAction("UpdateEmployee",user);
    }

    [HttpGet]
     public IActionResult ManagerDetails(){
        string Role = "Manager";
        List<User> userlist=repository.Display(Role);
        return View(userlist);
    }

    [HttpGet]
     public IActionResult SystemAdminDetails(){
        string Role = "SystemAdmin";
        List<User> userlist=repository.Display(Role);
        return View(userlist);
    }

    [HttpPost]
    public IActionResult Delete(string username){
        repository.Delete(username);
        return RedirectToAction("EmployeeDetails");
    }

    [HttpGet]
    public IActionResult UpdateEmployee(User user){
        return View(user);
    }

    [HttpPost]
    public IActionResult UpdateEmploye(User user){
        repository.update(user);
        string? Role = repository.GetRole(user);
        if(Role=="Employee"){
            return RedirectToAction("EmployeeDetails");}
        else if(Role=="Manager"){
            return RedirectToAction("ManagerDetails");
        }
        else{
            return RedirectToAction("SystemAdminDetails");
        }
    }

    [HttpGet]
    public IActionResult AddEmployee(){
        return View();
    }

    [HttpPost]
    public IActionResult AddEmployee(User user){
       string result = repository.ValidUser(user);
        if(result.Equals("Valid")){
            repository.insert(user);
            return RedirectToAction("EmployeeDetails");}
        else if(result.Equals("Exist Username")){
            ViewBag.message="Username already Exist";
            return View();
        }
        else if(result.Equals("Exist Emailid")){
            ViewBag.message="EmailID already Exist";
            return View();
        }
        else{
            return View();
        }
    }
    [HttpGet]
    public IActionResult AddManager(){
        return View();
    }

    [HttpPost]
    public IActionResult AddManager(User user){
        string result = repository.ValidUser(user);
        if(result.Equals("Valid")){
            repository.insert(user);
            return RedirectToAction("ManagerDetails");}
        else if(result.Equals("Exist Username")){
            ViewBag.message="Username already Exist";
            return View();
        }
        else if(result.Equals("Exist Emailid")){
            ViewBag.message="EmailID already Exist";
            return View();
        }
        else{
            return View();
        }
    }
    [HttpGet]
    public IActionResult AddSystemAdmin(){
        return View();
    }

    [HttpPost]
    public IActionResult AddSystemAdmin(User user){
        string result = repository.ValidUser(user);
        if(result.Equals("Valid")){
            repository.insert(user);
            return RedirectToAction("SystemAdminDetails");}
        else if(result.Equals("Exist Username")){
            ViewBag.message="Username already Exist";
            return View();
        }
        else if(result.Equals("Exist Emailid")){
            ViewBag.message="EmailID already Exist";
            return View();
        }
        else{
            return View();
        }
    }
    public IActionResult ProjectDetails(){
        List<Project> projects = repository.Viewproject();
        return View(projects);
    }
    [HttpGet]
    public IActionResult AddProject(){
        return View();
    }
    [HttpPost]
    public IActionResult AddProject(Project project){
        Console.WriteLine(project.Deadline);
        repository.AddProject(project);
        return RedirectToAction("ProjectDetails");
    }
    [HttpGet]
    public IActionResult RequestDetails()
    {
        List<Request> requestlist=repository.Requestdetail("Admin");
        return View(requestlist);
    }

    [HttpPost]
    public IActionResult RequestDetails(string Username,int RequestID)
    {
        repository.UpdateStatus(RequestID,"Done");
        User user= repository.display(Username);
        return RedirectToAction("UpdateEmployee",user);
    }

    [HttpPost]
    public IActionResult Reject(int RequestID)
    {
        repository.UpdateStatus(RequestID,"Rejected");
        return RedirectToAction("FRequestDetails");
    }

    [HttpGet]
    public IActionResult FRequestDetails()
    {
        List<Request> requestlist=repository.Requestdetail("Admin","In Progress");
        return View(requestlist);
    }
}
