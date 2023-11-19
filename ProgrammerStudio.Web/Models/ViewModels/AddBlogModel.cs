using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProgrammerStudio.Web.Models.ViewModels;

public class AddBlogModel
{
    public string Heading { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string UrlHandle { get; set; }
    public DateTime PublishDate { get; set; }
    public string Author { get; set; }
    public bool Visible { get; set; }
    public IEnumerable<SelectListItem>? Tags { get; set; } // list of tags
    public string[] SelectedTags { get; set; } = Array.Empty<string>();
}
