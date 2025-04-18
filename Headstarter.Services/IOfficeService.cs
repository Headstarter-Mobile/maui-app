﻿using Headstarter.Protos;

namespace Headstarter.Services
{
    public interface IOfficeService
    {
        Office GetOffice(Office office);
        Task<ICollection<Office>> GetAllOffices(Office filters);
        Office CreateOffice(Office office);
        Office UpdateOffice(Office oldOffice, Office newOffice);
        Office DeleteOffice(Office office);
    }
}
