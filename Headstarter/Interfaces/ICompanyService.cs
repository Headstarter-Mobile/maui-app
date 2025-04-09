using Headstarter.Protos;

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
