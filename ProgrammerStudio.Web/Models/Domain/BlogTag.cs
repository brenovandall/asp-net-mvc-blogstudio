namespace ProgrammerStudio.Web.Models.Domain;

public class BlogTag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public ICollection<BlogPost> BlogPosts { get; set; } // many for many relation with posts
}
