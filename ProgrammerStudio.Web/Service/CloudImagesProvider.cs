using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ProgrammerStudio.Web.Services;

public class CloudImagesProvider
{
    private IConfiguration _configuration;
    private Account _account;

    public CloudImagesProvider(IConfiguration configuration)
    {

        _configuration = configuration;
        _account = new Account(
            //  ****** getting the information from appsettings *******
                _configuration.GetSection("Cloudinary")["CloudName"],
                _configuration.GetSection("Cloudinary")["ApiKey"],
                _configuration.GetSection("Cloudinary")["ApiSecrets"]
            );
    }

    public async Task<string> UploadImageToCloud(IFormFile imageFile)
    {
        var cloudinary = new Cloudinary(_account);

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
            DisplayName = imageFile.FileName
        };
        var uploadResult = await cloudinary.UploadAsync(uploadParams);

        if (uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
        {
            return uploadResult.SecureUri.ToString();
        }

        return null;
    }
}
