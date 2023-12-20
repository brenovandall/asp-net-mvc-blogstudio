using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammerStudio.Web.Data;
using ProgrammerStudio.Web.Models.ViewModels;

namespace ProgrammerStudio.Web.Controllers;

[Authorize(Roles = "superAdmin")]
public class AdminUserController : Controller
{
    private readonly AuthDbContext _authcontext;
    private UserManager<IdentityUser> _userManager;

    public AdminUserController(AuthDbContext context)
    {
        _authcontext = context;
    }

    public async Task<IActionResult> List()
    {
        var users = _authcontext.Users.ToList();

        var userAdminLogged = "37fcd00d-418a-453f-a4cb-e8b271c22d9b";

        foreach(var user in users)
        {
            if(user.Id == userAdminLogged)
            {
                users.Remove(user);
            }
        }

        var userViewModel = new UserViewModel();
        userViewModel.Users = new List<User>();

        foreach (var user in users)
        {
            userViewModel.Users.Add(new User
            {
                Id = Guid.Parse(user.Id),
                Username = user.UserName,
                Email = user.Email
            });
        }

        return View(userViewModel);
    }
}
