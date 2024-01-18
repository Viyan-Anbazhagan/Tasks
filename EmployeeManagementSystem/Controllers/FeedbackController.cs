using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FeedbackController : Controller
{
    private readonly ILogger<FeedbackController> _logger;
    private readonly AppDbContext _context;

    public FeedbackController(ILogger<FeedbackController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
    {
        return await _context.Feedbacks.ToListAsync();
    }
    [HttpPost]
    public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
    {
        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();

        return CreatedAtAction( nameof(GetFeedbacks),new { id = feedback.ID }, feedback);
    }
}