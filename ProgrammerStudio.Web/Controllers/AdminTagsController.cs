using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProgrammerStudio.Web.Data;
using ProgrammerStudio.Web.Models.Domain;
using ProgrammerStudio.Web.Models.ViewModels;

namespace ProgrammerStudio.Web.Controllers;

public class AdminTagsController : Controller
{
    // mapper and context attributes -- > 
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

        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult List()
    {
        var tags = _context.Tags.ToList(); // list all the tags

        return View(tags);
    }

    [HttpGet]
    public IActionResult Edit(Guid id)
    {
        var tag = _context.Tags.FirstOrDefault(x => x.Id == id); // take the same tag ig

        if (tag != null)
        {
            // create a  new View Model of tags -- >
            EditTagModel model = new EditTagModel
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayName = tag.DisplayName
            };

            return View(model); // return the info on the edit fields
        }

        return View(null); // else, return null
    }

    [HttpPost]
    public IActionResult Edit(EditTagModel editTagModel)
    {
        // create a new tag with the new info, using manual mapper rn
        BlogTag tag = new()
        {
            Id = editTagModel.Id,
            Name = editTagModel.Name,
            DisplayName = editTagModel.DisplayName
        };

        var oldTag = _context.Tags.FirstOrDefault(x => x.Id == tag.Id); // the old tag that exists on database

        if (oldTag != null)
        {
            oldTag.Name = tag.Name;
            oldTag.DisplayName = tag.DisplayName;

            _context.SaveChanges();
            return RedirectToAction("List"); // redirect to the list of tags
        }

        return RedirectToAction("Edit", new { id = editTagModel.Id }); // else, will be in the same page, the readonly id will be the editTagModel ID
    }
}
