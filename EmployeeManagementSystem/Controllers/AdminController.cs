using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagementSystem.Controllers;
[Authorize(Roles ="Admin")]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly IRepository _repository;
    public AdminController(ILogger<AdminController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public IActionResult Index(){
        return View();
    }

    public IActionResult UserDetails(){
        string? Username= HttpContext.Session.GetString("Username");
        return View(_repository.ViewUser(Username));
    }
    [HttpGet]
    public IActionResult EmployeeDetails(){
        List<User> userlist=_repository.ViewUserAdmin("Employee");
        return View(userlist);
    }

    [HttpPost]
    public IActionResult EmployeeDetails(string username){
        User user= _repository.ViewUser(username);
        return RedirectToAction("UpdateEmployee",user);
    }

    [HttpGet]
     public IActionResult ManagerDetails(){
        List<User> userlist=_repository.ViewUserAdmin("Manager");
        return View(userlist);
    }

    [HttpGet]
     public IActionResult SystemAdminDetails(){
        List<User> userlist=_repository.ViewUserAdmin("SystemAdmin");
        return View(userlist);
    }

    [HttpPost]
    public IActionResult Delete(string username){
        _repository.DeleteUser(username);
        return RedirectToAction("EmployeeDetails");
    }

    [HttpGet]
    public IActionResult UpdateEmployee(User user){
        return View(user);
    }

    [HttpPost]
    public IActionResult UpdateEmploye(User user){
        _repository.UpdateUser(user);
        string? Role = _repository.GetRole(user.Username);
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
        if(ModelState.IsValid){
            string result = _repository.ValidateUser(user);
            if(result.Equals("Valid")){
                _repository.AddUser(user);
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
                ViewBag.message="Already Exist";
                return View();
            }
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
        if(ModelState.IsValid){
            string result = _repository.ValidateUser(user);
            if(result.Equals("Valid")){
                _repository.AddUser(user);
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
                ViewBag.message="Already Exist";
                return View();
            }
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
        if(ModelState.IsValid){
            string result = _repository.ValidateUser(user);
            if(result.Equals("Valid")){
                _repository.AddUser(user);
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
                ViewBag.message="Already Exist";
                return View();
            }
        }
        else{
            return View();
        }
    }
    public IActionResult ProjectDetails(){
        List<Project> projects = _repository.ViewProject();
        return View(projects);
    }
    public IActionResult ViewPDF(string name)
    {
        Console.WriteLine(name);
        byte[] document = _repository.ViewPDF(name);
        return File(document,"application/pdf");
    }
    [HttpGet]
    public IActionResult AddProject(Project project){
        return View(project);
    }
    [HttpPost]
    public IActionResult AddProjects(Project project){
        if(ModelState.IsValid){
        _repository.AddProject(project);
        return RedirectToAction("ProjectDetails");}
        else{
            return View(); 
        }
    }

    [HttpGet]
    public IActionResult AssignProject(){
        return View(_repository.ViewManager());
    }
    [HttpPost]
    public IActionResult AssignProject(string username){
        Project project = new Project();
        project.Manager= username;
        return RedirectToAction("AddProject","Admin",project); 
    }
    [HttpPost]
    public IActionResult DeleteProject(string Name){
        _repository.DeleteProject(Name);
        return RedirectToAction("ProjectDetails");
    }

    [HttpGet]
    public IActionResult RequestDetails()
    {
        List<Request> requestList=_repository.ViewRequestReceiver("Admin");
        return View(requestList);
    }

    [HttpPost]
    public IActionResult RequestDetails(string username,int requestID,string status)
    {
        _repository.UpdateRequest(requestID,status);
        User user= _repository.ViewUser(username);
        return RedirectToAction("UpdateEmployee",user);
    }

    [HttpPost]
    public IActionResult Reject(int requestID,string status)
    {
        _repository.UpdateRequest(requestID,status);
        return RedirectToAction("FRequestDetails");
    }

    [HttpGet]
    public IActionResult FRequestDetails()
    {
        List<Request> requestlist=_repository.ViewRequest("Admin");
        return View(requestlist);
    }
    
}
