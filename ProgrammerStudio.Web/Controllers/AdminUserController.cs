using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammerStudio.Web.Data;
using ProgrammerStudio.Web.Models.ViewModels;

namespace ProgrammerStudio.Web.Controllers;

[Authorize(Roles = "superAdmin")]
public class AdminUserController : Controller
{
    private readonly AuthDbContext _authcontext;
    private UserManager<IdentityUser> _userManager;
    private SignInManager<IdentityUser> _signInManager;

    public AdminUserController(AuthDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _authcontext = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
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

    [HttpPost]
    public async Task<IActionResult> List(UserViewModel userViewModel)
    {
        if (userViewModel != null)
        {
            var user = new IdentityUser()
            {
                UserName = userViewModel.Username,
                Email = userViewModel.Email,
                NormalizedEmail = userViewModel.Email.ToUpper(),
                NormalizedUserName = userViewModel.Username.ToUpper()
            };

            var response = await _userManager.CreateAsync(user, userViewModel.Password);

            if (response.Succeeded)
            {
                if (userViewModel.IsRole == true)
                {
                    var role = await _userManager.AddToRoleAsync(user, "admin");
                } else
                {
                    var role = await _userManager.AddToRoleAsync(user, "user");
                }

                return RedirectToAction("List");
            }

        }

        return View(null);
    }

}
