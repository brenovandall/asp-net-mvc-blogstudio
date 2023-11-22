using Microsoft.AspNetCore.Mvc.Rendering;
using ProgrammerStudio.Web.Models.Domain;

namespace ProgrammerStudio.Web.Models.ViewModels;

public class EditBlogModel
{
    public Guid Id { get; set; }
    public string Heading { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string UrlHandle { get; set; }
    public DateTime PublishDate { get; set; }
    public string Author { get; set; }
    public bool Visible { get; set; }
    public ICollection<BlogTag> BlogTags { get; set; }
    public IEnumerable<SelectListItem>? Tags { get; set; }
    public string[] SelectedTags { get; set; } = Array.Empty<string>();
}

