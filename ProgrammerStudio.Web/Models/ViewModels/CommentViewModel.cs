using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ProgrammerStudio.Web.Models.ViewModels;

public class CommentViewModel
{
    public string Content { get; set; }
    public DateTime CommentAdded { get; set; }
    public string UserName { get; set; }
}
