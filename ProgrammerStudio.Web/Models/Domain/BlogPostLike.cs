namespace ProgrammerStudio.Web.Models.Domain;

public class BlogPostLike
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
}
