using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EmployeeManagementSystem.Models;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Controllers;

public class ManagerController : Controller
{
    Repository repository=new Repository();
    private readonly ILogger<HomeController> _logger;

    public ManagerController(ILogger<HomeController> logger)
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
    public IActionResult RequestDetails()
    {
        string? Username= HttpContext.Session.GetString("Username");
        List<Request> requestlist=repository.Requestdetail(Username);
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
        request.Status="In Progress";
        repository.Addrequest(request);
        return RedirectToAction("RequestDetails");
    }
}