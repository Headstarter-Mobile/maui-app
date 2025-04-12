using Headstarter.Protos;

namespace Headstarter.Services
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
