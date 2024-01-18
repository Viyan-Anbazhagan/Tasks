using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagementSystem.Controllers;
[Authorize(Roles ="SystemAdmin")]
public class SystemAdminController : Controller
{
    private readonly ILogger<SystemAdminController> _logger;
    private readonly IRepository _repository;
    public SystemAdminController(ILogger<SystemAdminController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult UserDetails(){
        string? Username= HttpContext.Session.GetString("Username");
        return View(_repository.ViewUser(Username));
    }
    [HttpGet]
    public IActionResult RequestDetails()
    {
        List<Request> requestlist=_repository.ViewRequestReceiver("SystemAdmin");
        return View(requestlist);
    }
    [HttpPost]
    public IActionResult RequestDetails(int requestID,string status)
    {
        _repository.UpdateRequest(requestID,status);
        return RedirectToAction("FRequestDetails");
    }

    [HttpGet]
    public IActionResult FRequestDetails()
    {
        List<Request> requestlist=_repository.ViewRequest("SystemAdmin");
        return View(requestlist);
    }
}