using ProgrammerStudio.Web.Models.Domain;

namespace ProgrammerStudio.Web.Models.ViewModels;

public class HomeViewModel
{
    // i created this view model just because i need to show the blog posts and all the tags that i created on the index home page...
    // for this, i cant actually return twice
    // the resolution for this, is just creating a new view model, that includes a enumerable collection of blog posts and blog tags! 
    public IEnumerable<BlogPost> BlogPosts { get; set; }
    public IEnumerable<BlogTag> BlogTags { get; set; }
}
