namespace UnitTests.Mocks
{
    public class ApplicationService : Headstarter.Interfaces.IApplicationService
    {
        public Headstarter.Protos.Application CreateApplication(Headstarter.Protos.Application application)
        {
            throw new NotImplementedException();
        }

        public Headstarter.Protos.Application DeleteApplication(Headstarter.Protos.Application application)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Headstarter.Protos.Application>> GetAllPositions(int userId, int positionId, string status, string createdAtStart, string createdAtEnd, string updatedAtStart, string updatedAtEnd)
        {
            throw new NotImplementedException();
        }

        public Headstarter.Protos.Application GetApplication(Headstarter.Protos.Application application)
        {
            throw new NotImplementedException();
        }

        public Headstarter.Protos.Application UpdateApplication(Headstarter.Protos.Application oldApplication, Headstarter.Protos.Application newApplication)
        {
            throw new NotImplementedException();
        }
    }
}
