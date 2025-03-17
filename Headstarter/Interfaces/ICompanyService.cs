using Headstarter.Protos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Headstarter.Interfaces
{
    public interface ICompanyService
    {
        Company GetCompany(Company company);
        Task<ICollection<Company>> GetAllCompanys();
        Company CreateCompany(Company company);
        Company UpdateCompany(Company oldCompany, Company newCompany);
        Company DeleteCompany(Company company);
       
    }
}
