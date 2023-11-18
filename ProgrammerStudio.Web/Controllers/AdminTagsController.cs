using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProgrammerStudio.Web.Data;
using ProgrammerStudio.Web.Models.Domain;
using ProgrammerStudio.Web.Models.ViewModels;

namespace ProgrammerStudio.Web.Controllers;

public class AdminTagsController : Controller
{
    // mapper and context atributes -- > 
    private IMapper _mapper;
    private ApplicationDbContext _context;

    public AdminTagsController(IMapper mapper, ApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(AddTagModel addTagModel)
    {
        BlogTag tag = _mapper.Map<BlogTag>(addTagModel); // automap a tag view model to a tag
        _context.Tags.Add(tag); // add to tags on the db context
        _context.SaveChanges(); // save db changes

        return View("");
    }
}
