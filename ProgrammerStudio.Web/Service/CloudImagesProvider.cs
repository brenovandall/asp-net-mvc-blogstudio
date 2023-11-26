using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ProgrammerStudio.Web.Services;
/*
   **********************************************
    THIS IS THE PROVIDER SERVICE FROM CLOUDINARY
   ********************************************** 
*/
public class CloudImagesProvider
{
    private IConfiguration _configuration; // configuration to get the paramethers from the appsettings
    private Cloudinary _cloudinary; // cloudinary needs an account with cloudname, the api key and the api secret

    public CloudImagesProvider(IConfiguration configuration)
    {

        _configuration = configuration;
        Account account = new Account(
            //  ****** getting the information from appsettings *******
                _configuration.GetSection("Cloudinary")["CloudName"],
                _configuration.GetSection("Cloudinary")["ApiKey"],
                _configuration.GetSection("Cloudinary")["ApiSecrets"]
            );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageToCloud(IFormFile imageFile)
    {
        // var cloudinary = new Cloudinary(_account); // new account that cloudinary needs

//    *********** CLOUDINARY UPLOAD PARAMETHERS ***********
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
            DisplayName = imageFile.FileName
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams); // upload result that will be returned for the task

        if (uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
        {
            return uploadResult.SecureUri.ToString(); // returning the upload result at string format
        }

        return null;
    }
}
