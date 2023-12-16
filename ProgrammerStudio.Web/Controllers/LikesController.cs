using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using ProgrammerStudio.Web.Data;
using ProgrammerStudio.Web.Models.Domain;
using ProgrammerStudio.Web.Models.ViewModels;
using ProgrammerStudio.Web.Service;

namespace ProgrammerStudio.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LikesController : ControllerBase
{

    private ApplicationDbContext _context;
    private readonly IGetAllTheLikes _getAllTheLikes;

    public LikesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // here, is the step when the user likes the post
    [HttpPost]
    [Route("AddLikes")]
    public async Task<IActionResult> AddLike([FromBody] AddLikeViewModel addLikeViewModel) // comes from body of requisition
    {
        // so i create a new blog post like, where has the post and user ID, to specify the blog and user that had be liked...  
        var like = new BlogPostLike()
        {
            PostId = addLikeViewModel.BlogId,
            UserId = addLikeViewModel.UserId
        };

        await _context.Likes.AddAsync(like);
        await _context.SaveChangesAsync();

        return Ok();

    }

    [HttpGet]
    [Route("{PostId:Guid}/totallikes")] // route contains the post id, so i can search for the post later on param
    public async Task<IActionResult> GetTotalLikes([FromRoute] Guid postId) // getting the post id from the route i had mentioned
    {
        var totalLikes = await _context.Likes.CountAsync(x => x.PostId == postId); // so it gets the count of lines and returns as number if likes

        return Ok(totalLikes);
    }
}
