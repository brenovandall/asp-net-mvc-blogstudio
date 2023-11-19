using AutoMapper;
using ProgrammerStudio.Web.Models.Domain;
using ProgrammerStudio.Web.Models.ViewModels;

namespace ProgrammerStudio.Web.Profiles;

public class AddBlogProfile : Profile
{
    public AddBlogProfile()
    {
        CreateMap<AddBlogModel, BlogPost>();
    }
}
