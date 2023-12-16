using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    [HttpPost]
    [Route("AddLikes")]
    public async Task<IActionResult> AddLike([FromBody] AddLikeViewModel addLikeViewModel)
    {

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
    [Route("{PostId:Guid}/totallikes")]
    public async Task<IActionResult> GetTotalLikes([FromRoute] Guid postId)
    {
        var totalLikes = await _context.Likes.CountAsync(x => x.PostId == postId);

        return Ok(totalLikes);
    }
}
