using Microsoft.AspNetCore.Mvc;

namespace ProgrammerStudio.Web.Controllers;

public class AdminTagsController : Controller
{
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
}
