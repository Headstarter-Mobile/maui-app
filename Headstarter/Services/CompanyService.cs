using Grpc.Core;
using Headstarter.Interfaces;
using Headstarter.Protos;

namespace Headstarter.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly GrpcService _grpcService; // For API calls

        public CompanyService(GrpcService grpcService)
        {
            _grpcService = grpcService;

        }
        public Company GetCompany(Company company)
        {
            try
            {
                var response = _grpcService.companyClient.GetCompany(company, _grpcService._metadata);
                return response;
            }
            catch (RpcException ex)
            {
                if (ex.Status.StatusCode.Equals(StatusCode.NotFound))
                {
                    // user not found
                    return null;
                }
                else if (ex.Status.StatusCode.Equals(StatusCode.PermissionDenied))
                {
                    // permission denied
                    return null;
                }
                else
                {
                    // other error
                    return null;
                }
            }
        }
        public async Task<ICollection<Company>> GetAllCompanys()
        {
            try
            {
                var client = _grpcService.companyClient;
                using var call = client.GetAllCompanies(new Google.Protobuf.WellKnownTypes.Empty(), _grpcService._metadata);
                List<Company> positions = [];

                while (await call.ResponseStream.MoveNext())
                {
                    positions.Add(call.ResponseStream.Current);
                }
                return positions;
            }
            catch (RpcException ex)
            {
                if (ex.Status.StatusCode.Equals(StatusCode.NotFound))
                {
                    // user not found
                    return null;
                }
                else if (ex.Status.StatusCode.Equals(StatusCode.PermissionDenied))
                {
                    // permission denied
                    return null;
                }
                else
                {
                    // other error
                    return null;
                }
            }
        }
        public Company CreateCompany(Company company)
        {
            try
            {
                var response = _grpcService.companyClient.CreateCompany(company, _grpcService._metadata);
                return response;
            }
            catch (RpcException ex)
            {
                if (ex.Status.StatusCode.Equals(StatusCode.NotFound))
                {
                    // user not found
                    return null;
                }
                else if (ex.Status.StatusCode.Equals(StatusCode.PermissionDenied))
                {
                    // permission denied
                    return null;
                }
                else
                {
                    // other error
                    return null;
                }
            }
        }
        public Company UpdateCompany(Company oldCompany, Company newCompany)
        {
            try
            {
                var response = _grpcService.companyClient.UpdateCompany(new Headstarter.Protos.CompanyRequest()
                {
                    OldData = oldCompany,
                    NewData = newCompany
                }, _grpcService._metadata);
                return response;
            }
            catch (RpcException ex)
            {
                if (ex.Status.StatusCode.Equals(StatusCode.NotFound))
                {
                    // user not found
                    return null;
                }
                else if (ex.Status.StatusCode.Equals(StatusCode.PermissionDenied))
                {
                    // permission denied
                    return null;
                }
                else
                {
                    // other error
                    return null;
                }
            }
        }
        public Company DeleteCompany(Company company)
        {
            try
            {
                var response = _grpcService.companyClient.DeleteCompany(company, _grpcService._metadata);
                return company;
            }
            catch (RpcException ex)
            {
                if (ex.Status.StatusCode.Equals(StatusCode.NotFound))
                {
                    // user not found
                    return null;
                }
                else if (ex.Status.StatusCode.Equals(StatusCode.PermissionDenied))
                {
                    // permission denied
                    return null;
                }
                else
                {
                    // other error
                    return null;
                }
            }
        }
    }

}
