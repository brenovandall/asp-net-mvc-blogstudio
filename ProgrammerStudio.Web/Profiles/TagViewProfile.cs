using AutoMapper;
using ProgrammerStudio.Web.Models.Domain;
using ProgrammerStudio.Web.Models.ViewModels;

namespace ProgrammerStudio.Web.Profiles;

public class TagViewProfile : Profile
{
    // auto mapping the tag view model to a blog tag
    public TagViewProfile()
    {
        CreateMap<AddTagModel, BlogTag>();
    }
}
