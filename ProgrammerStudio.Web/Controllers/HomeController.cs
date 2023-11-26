using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammerStudio.Web.Data;
using ProgrammerStudio.Web.Models;
using System.Diagnostics;

namespace ProgrammerStudio.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var posts = await _context.Posts.Include(x => x.BlogTags).ToListAsync();

        return View(posts);
    }

    public async Task<IActionResult> ShowDetails(Guid id)
    {
        var postSelected = _context.Posts.Include(x => x.BlogTags).FirstOrDefault(x => x.Id == id);

        if (postSelected != null)
        {
            return View(postSelected);
        }

        return View(null);
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
}