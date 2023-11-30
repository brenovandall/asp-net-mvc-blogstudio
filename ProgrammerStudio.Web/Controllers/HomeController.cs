using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammerStudio.Web.Data;
using ProgrammerStudio.Web.Models;
using ProgrammerStudio.Web.Models.ViewModels;
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
        // right here, i can use the view model that a have created, so its been so easy to display these two list of items on the index 

        var posts = await _context.Posts.Include(x => x.BlogTags).ToListAsync();

        var tags = await _context.Tags.ToListAsync();

        var modelWithTwiceResult = new HomeViewModel()
        {
            BlogPosts = posts, // sooo, the list that i stored at "posts" variable, i can return for BlogPosts collection...
            BlogTags = tags // same thing right here, just getting the tags stored items, to display on index razor page.
        };

        return View(modelWithTwiceResult);
    }

    public async Task<IActionResult> ShowDetails(string handle)
    {
        // get the url handle and compares with some data stored --- >>>>
        var postSelected = _context.Posts.Include(x => x.BlogTags).FirstOrDefault(x => x.UrlHandle == handle);

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