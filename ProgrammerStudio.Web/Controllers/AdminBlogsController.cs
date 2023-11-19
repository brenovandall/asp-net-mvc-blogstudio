using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammerStudio.Web.Data;
using ProgrammerStudio.Web.Models.ViewModels;

namespace ProgrammerStudio.Web.Controllers;

public class AdminBlogsController : Controller
{
    private ApplicationDbContext _context;

    public AdminBlogsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var tags = await _context.Tags.ToListAsync(); // returns a list of tags

        AddBlogModel blog = new()
        {
            Tags = tags.Select(x => new SelectListItem{ Text = x.DisplayName, Value = x.Id.ToString() }) // using this to list the tags on the field
        };

        return View(blog);
    }
}
