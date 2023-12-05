using Microsoft.EntityFrameworkCore;
using ProgrammerStudio.Web.Data;

namespace ProgrammerStudio.Web.Service;

public class GetLikes : IGetAllTheLikes
{
    private ApplicationDbContext _context;

    public GetLikes(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetAllTheLikesOfThePost(Guid id)
    {
        return await _context.Likes.CountAsync(x => x.PostId == id);

    }
}
