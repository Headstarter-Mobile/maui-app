namespace Headstarter.Services
{
    public interface IApplicationService
    {
        Protos.Application GetApplication(Protos.Application application);
        Task<ICollection<Protos.Application>> GetAllApplications(int userId, int positionId, string status, string createdAtStart, string createdAtEnd, string updatedAtStart, string updatedAtEnd);
        Protos.Application CreateApplication(Protos.Application application);
        Protos.Application UpdateApplication(Protos.Application oldApplication, Protos.Application newApplication);
        Protos.Application DeleteApplication(Protos.Application application);

    }
}
