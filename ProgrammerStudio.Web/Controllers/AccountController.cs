using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammerStudio.Web.Models.ViewModels;

namespace ProgrammerStudio.Web.Controllers;

public class AccountController : Controller
{

    private UserManager<IdentityUser> _userManager;

    public AccountController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(AddAccountModel addAccountModel)
    {
        var identityUser = new IdentityUser
        {
            UserName = addAccountModel.Username,
            Email = addAccountModel.Email
        };

        var response = await _userManager.CreateAsync(identityUser, addAccountModel.Password);

        if (response.Succeeded)
        {
            var role = await _userManager.AddToRoleAsync(identityUser, "user");

            if (role.Succeeded)
            {
                return RedirectToAction("Register");
            }
        }

        return View();
    }
}
