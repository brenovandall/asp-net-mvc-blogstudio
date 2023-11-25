using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammerStudio.Web.Services;

namespace ProgrammerStudio.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageApiController : ControllerBase
{
    private CloudImagesProvider _cloudImagesProvider;

    public ImageApiController(CloudImagesProvider cloudImagesProvider)
    {
        _cloudImagesProvider = cloudImagesProvider;
    }


    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile imageFile)
    {

        var image = await _cloudImagesProvider.UploadImageToCloud(imageFile);

        if (image == null)
        {
            return BadRequest();
        }

        return new JsonResult(new { Link = image });
    }
}
