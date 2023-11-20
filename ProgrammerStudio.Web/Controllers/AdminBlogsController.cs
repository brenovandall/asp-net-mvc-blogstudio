using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammerStudio.Web.Data;
using ProgrammerStudio.Web.Models.Domain;
using ProgrammerStudio.Web.Models.ViewModels;

namespace ProgrammerStudio.Web.Controllers;

public class AdminBlogsController : Controller
{
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public AdminBlogsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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

    [HttpPost]
    public async Task<IActionResult> Add(AddBlogModel addBlogModel)
    {
        // mapping the addBlogModel view-model to a blog post
        var newBlogPost = _mapper.Map<BlogPost>(addBlogModel);

        List<BlogTag> listOfTags = new(); // this is the list that will have all the tags selected
        foreach (var item in addBlogModel.SelectedTags) // loop on the selected tags
        {
            var selectedTag = Guid.Parse(item); // the id of addBlogModel selected tag
            var tagFromDatabase = await _context.Tags.FirstOrDefaultAsync(x => x.Id == selectedTag); // getting the the tag that has the same selectedTag id

            if (tagFromDatabase != null)
            {
                listOfTags.Add(tagFromDatabase); // add to the list of tags the existing tag from database
            }
        }

        newBlogPost.BlogTags = listOfTags; // taking the mapped blog, the collection of tags will be the tag list "listOfTags"

        await _context.Posts.AddAsync(newBlogPost);
        await _context.SaveChangesAsync();

        return RedirectToAction("Add");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var posts = _context.Posts.Include(x => x.BlogTags).ToListAsync();

        return View(posts);
    }
}
