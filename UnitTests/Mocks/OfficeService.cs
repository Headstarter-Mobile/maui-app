using Headstarter.Interfaces;
using Headstarter.Protos;

namespace UnitTests.Mocks
{
    internal class OfficeService : IOfficeService
    {
        public Office CreateOffice(Office office)
        {
            throw new NotImplementedException();
        }

        public Office DeleteOffice(Office office)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Office>> GetAllOffices()
        {
            throw new NotImplementedException();
        }

        public Office GetOffice(Office office)
        {
            throw new NotImplementedException();
        }

        public Office UpdateOffice(Office oldOffice, Office newOffice)
        {
            throw new NotImplementedException();
        }
    }
}
