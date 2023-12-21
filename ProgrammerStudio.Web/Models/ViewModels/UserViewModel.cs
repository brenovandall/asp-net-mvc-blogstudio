using Microsoft.AspNetCore.Components.Web;

namespace ProgrammerStudio.Web.Models.ViewModels;

public class UserViewModel
{
    public List<User> Users { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsRole { get; set; }
}
