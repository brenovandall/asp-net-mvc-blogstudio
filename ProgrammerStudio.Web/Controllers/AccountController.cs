using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammerStudio.Web.Models.ViewModels;

namespace ProgrammerStudio.Web.Controllers;

public class AccountController : Controller
{

    private UserManager<IdentityUser> _userManager;
    private SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager; // manager user with identity, so i can use methods like create user and stuff
        _signInManager = signInManager; 
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(AddAccountModel addAccountModel)
    {

        // create a new user with identity framework
        var identityUser = new IdentityUser
        {
            UserName = addAccountModel.Username,
            Email = addAccountModel.Email
        };

        var response = await _userManager.CreateAsync(identityUser, addAccountModel.Password);

        if (response.Succeeded)
        {
            var role = await _userManager.AddToRoleAsync(identityUser, "user"); // add to role normal user

            if (role.Succeeded)
            {
                return RedirectToAction("Register");
            }
        }

        return View();
    }

    [HttpGet]
    public IActionResult Login(string ReturnUrl)
    {
        LoginViewModel loginViewModel = new()
        {
            ReturnUrl = ReturnUrl,
        };

        return View(loginViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {

        // its the authentication of the user that is trying to login -- >
        var responseSign = await _signInManager.PasswordSignInAsync(
            loginViewModel.Username, 
            loginViewModel.Password, 
            false, 
            false
            );

        if (responseSign != null && responseSign.Succeeded)
        {
            if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
            {
                return RedirectToPage(loginViewModel.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}
