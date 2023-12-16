namespace ProgrammerStudio.Web.Models.Domain;

public class Comment
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CommentDate { get; set; }
}
