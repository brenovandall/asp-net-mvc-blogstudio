using System.ComponentModel.DataAnnotations;

namespace ProgrammerStudio.Web.Models.ViewModels;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    public string? ReturnUrl { get; set; }
}
