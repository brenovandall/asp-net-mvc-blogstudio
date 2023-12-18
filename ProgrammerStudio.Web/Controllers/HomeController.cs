using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProgrammerStudio.Web.Data;
using ProgrammerStudio.Web.Models;
using ProgrammerStudio.Web.Models.Domain;
using ProgrammerStudio.Web.Models.ViewModels;
using ProgrammerStudio.Web.Service;
using System.Diagnostics;

namespace ProgrammerStudio.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ApplicationDbContext _context;
    private GetLikes _getlikes;
    private SignInManager<IdentityUser> _signInManager;
    private UserManager<IdentityUser> _userManager;
    private readonly AuthDbContext _authcontext;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, GetLikes getlikes, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, AuthDbContext authcontext)
    {
        _logger = logger;
        _context = context;
        _getlikes = getlikes;
        _signInManager = signInManager;
        _userManager = userManager;
        _authcontext = authcontext;
    }

    public async Task<IActionResult> Index()
    {
        // right here, i can use the view model that a have created, so its been so easy to display these two list of items on the index 

        var posts = await _context.Posts.Include(x => x.BlogTags).ToListAsync();

        var tags = await _context.Tags.ToListAsync();

        var modelWithTwiceResult = new HomeViewModel()
        {
            BlogPosts = posts, // sooo, the list that i stored at "posts" variable, i can return for BlogPosts collection...
            BlogTags = tags // same thing right here, just getting the tags stored items, to display on index razor page.
        };

        return View(modelWithTwiceResult);
    }

    [HttpGet]
    public async Task<IActionResult> ShowDetails(string handle)
    {
        var likedOrNot = false;
        // get the url handle and compares with some data stored --- >>>>
        var postSelected = _context.Posts.Include(x => x.BlogTags).FirstOrDefault(x => x.UrlHandle == handle);

        var userid = _userManager.GetUserId(User);

        var comme = _context.Comments;

        //var commentsOfThePost = await _context.Comments.Where(x => x.PostId == postSelected.Id).ToList();

        var listOfComments = new List<CommentViewModel>();

        foreach(var comment in comme.Where(x => x.PostId == postSelected.Id))
        {
            var com = new CommentViewModel()
            {
                Content = comment.Content,
                CommentAdded = comment.CommentDate,
                UserName = _authcontext.Users.FirstOrDefault(x => x.Id == comment.UserId.ToString()).UserName
            };

            listOfComments.Add(com);
        }

        if (_signInManager.IsSignedIn(User))
        {
            var likesOfPost = await _context.Likes.Where(x => x.PostId == postSelected.Id && x.UserId == Guid.Parse(userid)).ToListAsync();

            if (userid != null)
            {
                if (likesOfPost.Any())
                {
                    likedOrNot = true;
                }
            }
        }

        if (postSelected != null)
        {
            var likes = _getlikes.GetAllTheLikesOfThePost(postSelected.Id);

            var newModel = new ShowDetailsViewModel()
            {
                Id = postSelected.Id,
                Heading = postSelected.Heading,
                Title = postSelected.Title,
                Content = postSelected.Content,
                Description = postSelected.Description,
                ImageUrl = postSelected.ImageUrl,
                UrlHandle = postSelected.UrlHandle,
                PublishDate = postSelected.PublishDate,
                Author = postSelected.Author,
                Visible = postSelected.Visible,
                BlogTags = postSelected.BlogTags,
                Likes = await likes,
                Liked = likedOrNot,
                Comments = listOfComments
            };

            return View(newModel);
        }

        return View(null);
    }

    [HttpPost]
    public async Task<IActionResult> ShowDetails(ShowDetailsViewModel showDetailsViewModel)
    {
        if (_signInManager.IsSignedIn(User))
        {
            var userid = Guid.Parse(_userManager.GetUserId(User));

            var comment = new Comment()
            {
                Content = showDetailsViewModel.CommentContent,
                PostId = showDetailsViewModel.Id,
                UserId = userid,
                CommentDate = DateTime.Now
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("ShowDetails", "Home", new { handle = showDetailsViewModel.UrlHandle });
        }
        return Forbid();

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}