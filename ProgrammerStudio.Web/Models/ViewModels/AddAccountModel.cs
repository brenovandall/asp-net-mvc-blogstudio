using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ProgrammerStudio.Web.Models.ViewModels;

public class AddAccountModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "Password has to be at least 2 characters")]
    public string Password { get; set; }
}
