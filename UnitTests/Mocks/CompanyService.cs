using Headstarter.Interfaces;
using Headstarter.Protos;

namespace UnitTests.Mocks
{
    public class CompanyService : ICompanyService
    {
        public Company CreateCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public Company DeleteCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Company>> GetAllCompanys()
        {
            throw new NotImplementedException();
        }

        public Company GetCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public Company UpdateCompany(Company oldCompany, Company newCompany)
        {
            throw new NotImplementedException();
        }
    }
}
