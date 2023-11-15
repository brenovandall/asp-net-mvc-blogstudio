namespace ProgrammerStudio.Web.Models.Domain;

public class BlogPost
{
    public Guid Id { get; set; }
    public string Heading { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string UrlHandle { get; set; } // url that will get user on the handle blog page
    public DateTime PublishDate { get; set; }
    public string Author { get; set; }
    public bool Visible { get; set; }
    public ICollection<BlogTag> BlogTags { get; set; } // many for many relation with tags
}
