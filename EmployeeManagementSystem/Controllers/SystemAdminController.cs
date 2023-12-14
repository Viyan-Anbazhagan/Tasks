using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EmployeeManagementSystem.Models;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Controllers;

public class SystemAdminController : Controller
{
    Repository repository=new Repository();
    private readonly ILogger<HomeController> _logger;

    public SystemAdminController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult UserDetails(){
        string? Username= HttpContext.Session.GetString("Username");
        return View(repository.display(Username));
    }
    [HttpGet]
    public IActionResult RequestDetails()
    {
        List<Request> requestlist=repository.Requestdetail("SystemxAdmin");
        return View(requestlist);
    }
    [HttpPost]
    public IActionResult RequestDetails(int RequestID)
    {
        repository.UpdateStatus(RequestID,"Done");
        return RedirectToAction("FRequestDetails");
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
        List<Request> requestlist=repository.Requestdetail("SystemAdmin","In Progress");
        return View(requestlist);
    }
}