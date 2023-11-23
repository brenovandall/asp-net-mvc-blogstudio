using AutoMapper;
using ProgrammerStudio.Web.Models.Domain;
using ProgrammerStudio.Web.Models.ViewModels;

namespace ProgrammerStudio.Web.Profiles;

public class EditBlogPostProfile : Profile
{
    public EditBlogPostProfile()
    {
        CreateMap<EditBlogModel, BlogPost>();
    }
}
