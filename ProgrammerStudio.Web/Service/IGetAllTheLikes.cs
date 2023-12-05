namespace ProgrammerStudio.Web.Service;

public interface IGetAllTheLikes
{
    Task<int> GetAllTheLikesOfThePost(Guid id);
}
