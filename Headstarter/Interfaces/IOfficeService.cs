using Headstarter.Protos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Headstarter.Interfaces
{
    internal interface IOfficeService
    {
        Office GetOffice(Office office);
        Task<ICollection<Office>> GetAllOffices();
        Office CreateOffice(Office office);
        Office UpdateOffice(Office oldOffice, Office newOffice);
        Office DeleteOffice(Office office);
    }
}
