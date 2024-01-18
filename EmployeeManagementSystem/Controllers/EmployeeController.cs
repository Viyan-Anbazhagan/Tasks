using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;


namespace EmployeeManagementSystem.Controllers;
[Authorize(Roles ="Employee")]
public class EmployeeController : Controller
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IRepository _repository;
    public EmployeeController(ILogger<EmployeeController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult TeamDetails(){
        List<User> teamlist=_repository.ViewTeamMember(HttpContext.Session.GetString("Username"));
        return View(teamlist);
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
    public IActionResult TaskDetails(){
        List<Team> tasklist=_repository.ViewTaskUser(HttpContext.Session.GetString("Username"));
        return View(tasklist);
    }
    [HttpPost]
    public IActionResult TaskDetails(string taskname,string deadline,string manager){
        Team team = new Team();
        team.Username = HttpContext.Session.GetString("Username");
        team.Manager = manager;
        team.Taskname = taskname;
        team.Deadline = deadline;
        team.Dateofcompletion = Convert.ToString(DateTime.Now);
        _repository.UpdateTask(team);
        return RedirectToAction("TaskDetails");
    }

    [HttpGet]
    public IActionResult FTaskDetails(){
        List<Team> tasklist=_repository.ViewTasksUser(HttpContext.Session.GetString("Username"));
        return View(tasklist);
    }

}