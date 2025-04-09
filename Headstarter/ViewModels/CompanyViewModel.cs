using Grpc.Core;
using Headstarter.Protos;
using Headstarter.Services;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Headstarter.ViewModels
{
    public class CompanyViewModel
    {
        public Company Company { get; set; }
        public BindingList<Office> Offices { get; set; }
        public BindingList<Position> Positions { get; set; }
        public CompanyViewModel()
        {
            if (UserDataService.Instance.MyUser != null && UserDataService.Instance.MyUser.Type == Protos.UserRole.Recruiter)
            {
                Company = GrpcService.Instance.companyClient.GetCompany(new Company()
                {
                    Id = UserDataService.Instance.MyUser.CompanyId
                }, GrpcService.Instance._metadata);
                Offices = new BindingList<Office>();
                Positions = new BindingList<Position>();
                //Offices = new BindingList<Office>(GrpcService.Instance.officeClient.GetAllOffices(new Company()
                //{
                //    Id = UserDataService.Instance.MyUser.CompanyId
                //}, GrpcService.Instance._metadata));
            }
            else
            {
                Company = new Company()
                {
                    Id = 0,
                    Name = "No company",
                    Description = "No company",
                    Logo = "No company",
                    Website = "No company",
                    CreatedAt = DateTime.UtcNow.ToString(),
                    UpdatedAt = DateTime.UtcNow.ToString(),
                };
                Offices = new BindingList<Office>()
                {
                    new Office()
                    {
                        Id = 0,
                        Name = "No office",
                        Address = "No office",
                        CompanyId = 0,
                        Description = "No office",
                        Location = "No office",
                        CreatedAt = DateTime.UtcNow.ToString(),
                        UpdatedAt = DateTime.UtcNow.ToString(),
                    }
                };
                Position DummyPosition = new Position()
                {
                    Id = 0,
                    Title = "No position",
                    Description = "No position",
                    CompanyId = 0,
                    ExternalApplicationLink = "",
                    ExpiresAt = DateTime.UtcNow.ToString(),
                    PublishedAt = DateTime.UtcNow.ToString(),
                    Status = PositionStatus.Draft,
                    CreatedAt = DateTime.UtcNow.ToString(),
                    UpdatedAt = DateTime.UtcNow.ToString(),
                    Level = PositionLevel.EntryLevel,
                    YearsRequired = new PositionYearsRequired()
                    {
                        From = 0,
                        To = 2
                    },
                };
                DummyPosition.Offices.Add(new Office()
                {
                    Id = 0,
                    Name = "No office",
                    Address = "No office",
                    CompanyId = 0,
                    Description = "No office",
                    Location = "No office",
                    CreatedAt = DateTime.UtcNow.ToString(),
                    UpdatedAt = DateTime.UtcNow.ToString(),
                });
                Positions = new BindingList<Position>()
                {
                    DummyPosition
                };
                //throw new Exception("User is not a recruiter");
            }
        }
    }
}
