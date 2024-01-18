using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagementSystem.Controllers;
[Authorize(Roles ="Manager")]
public class ManagerController : Controller
{
    private readonly ILogger<ManagerController> _logger;
    private readonly IRepository _repository;
    public ManagerController(ILogger<ManagerController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult ProjectDetails(){
        Project project = _repository.ViewProject(HttpContext.Session.GetString("Username"));
        return View(project);
    }
    public IActionResult UserDetails(){
        string? Username= HttpContext.Session.GetString("Username");
        return View(_repository.ViewUser(Username));
    }
    public IActionResult RequestDetails()
    {
        string? Username= HttpContext.Session.GetString("Username");
        List<Request> requestlist=_repository.ViewRequestUser(Username);
        return View(requestlist);
    }
    [HttpGet]
    public IActionResult AddRequest()
    {
        return View();
    }
    [HttpPost]
    public IActionResult AddRequest(Request request)
    {
        request.Username=HttpContext.Session.GetString("Username");
        _repository.AddRequest(request);
        return RedirectToAction("RequestDetails");
    }
    [HttpGet]
    public IActionResult TeamDetails(){
        List<User> teamlist=_repository.ViewTeam(HttpContext.Session.GetString("Username"));
        return View(teamlist);
    }
    [HttpPost]
    public IActionResult TeamDetails(string username){
        _repository.RemoveTeam(username);
        return RedirectToAction("TeamDetails");
    }
    [HttpGet]
    public IActionResult AddTeam(){
        List<User> teamlist=_repository.ViewEmployee();
        return View(teamlist);
    }
    [HttpPost]
    public IActionResult AddTeam(string username){
        _repository.AddTeam(username,HttpContext.Session.GetString("Username"));
        return RedirectToAction("AddTeam");
    }
    [HttpGet]
    public IActionResult TaskDetails(){
        List<Team> tasklist=_repository.ViewTaskManager(HttpContext.Session.GetString("Username"));
        return View(tasklist);
    }
    [HttpGet]
    public IActionResult FTaskDetails(){
        List<Team> tasklist=_repository.ViewTasksManager(HttpContext.Session.GetString("Username"));
        return View(tasklist);
    }
    [HttpGet]
    public IActionResult AddTask(){
        string result=_repository.ValidateTask(HttpContext.Session.GetString("Username"));
        if(result.Equals("Valid")){
            return View();
        }
        else{
            return RedirectToAction("TaskDetails");
        }
    }
    [HttpPost]
    public IActionResult AddTask(Team task){
        task.Manager=HttpContext.Session.GetString("Username");
        string result = _repository.AddTask(task);
        if(result.Equals("Invalid")){
            ViewBag.message="No Teammate available for specific Domain";
            return View();
        }
        else if(result.Equals("Valid")){
            return RedirectToAction("TaskDetails");
        }
        return RedirectToAction("TaskDetails");
    }
    public IActionResult ViewPDF(string name)
    {
        Console.WriteLine(name);
        byte[] Document = _repository.ViewPDF(name);
        return File(Document,"application/pdf");
    }

}