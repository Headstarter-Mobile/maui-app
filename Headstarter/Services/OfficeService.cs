using Grpc.Core;
using Headstarter.Interfaces;
using Headstarter.Protos;

namespace Headstarter.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly GrpcService _grpcService; // For API calls

        public OfficeService(GrpcService grpcService)
        {
            _grpcService = grpcService;

        }

        public Office CreateOffice(Office office)
        {
            try
            {
                var response = _grpcService.officeClient.CreateOffice(office, _grpcService._metadata);
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

        public Office DeleteOffice(Office office)
        {
            try
            {
                var response = _grpcService.officeClient.DeleteOffice(office, _grpcService._metadata);
                return office;
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

        public async Task<ICollection<Office>> GetAllOffices()
        {
            try
            {
                var client = _grpcService.officeClient;
                using var call = client.GetAllOffices(new Google.Protobuf.WellKnownTypes.Empty(), _grpcService._metadata);
                List<Office> offices = [];

                while (await call.ResponseStream.MoveNext())
                {
                    offices.Add(call.ResponseStream.Current);
                }
                return offices;
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

        public Office GetOffice(Office office)
        {
            try
            {
                var response = _grpcService.officeClient.GetOffice(office

                , _grpcService._metadata);
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

        public Office UpdateOffice(Office oldOffice, Office newOffice)
        {
            try
            {

                var response = _grpcService.officeClient.UpdateOffice(new Headstarter.Protos.OfficeUpdateRequest()
                {
                    OldData = oldOffice,
                    NewData = newOffice
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
    }
}
