using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammerStudio.Web.Services;
using System.Net;

namespace ProgrammerStudio.Web.Controllers;
/*
   ************************************************************************* 
    THIS IS THE API CONTROLLER (CLOUDINARY PROVIDER) FOR UPLOAD IMAGE FILES
   ************************************************************************* 
*/
[Route("api/[controller]")]
[ApiController]
public class ImageApiController : ControllerBase
{
    private CloudImagesProvider _cloudImagesProvider; // this is the provider service

    public ImageApiController(CloudImagesProvider cloudImagesProvider)
    {
        _cloudImagesProvider = cloudImagesProvider;
    }


    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile imageFile) // ***IMPORTANT*** every file that will be uploaded, needs to contain "imageFile" as key name 
    {

        var image = await _cloudImagesProvider.UploadImageToCloud(imageFile); // calling the upload method

        if (image == null)
        {
            return BadRequest();
        }

        return new JsonResult(new { Link = image }); // returns a json that has the url handle for the image

    }
}
