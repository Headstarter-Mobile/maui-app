using Grpc.Core;

namespace Headstarter.Services;

public class ApplicationService : IApplicationService
{
    private readonly GrpcService _grpcService; // For API calls

    public ApplicationService(GrpcService grpcService)
    {
        _grpcService = grpcService;

    }
    public Protos.Application CreateApplication(Protos.Application application)
    {
        try
        {
            var response = _grpcService.applicationClient.CreateApplication(application, _grpcService._metadata);
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

    public Protos.Application DeleteApplication(Protos.Application application)
    {
        try
        {
            var response = _grpcService.applicationClient.DeleteApplication(application, _grpcService._metadata);
            return application;
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

    public async Task<ICollection<Protos.Application>> GetAllPositions(int userId, int positionId, string status, string createdAtStart, string createdAtEnd, string updatedAtStart, string updatedAtEnd)
    {
        try
        {
            var client = _grpcService.applicationClient;
            using var call = client.GetAllApplications(new()
            {
                UserId = userId,
                PositionId = positionId,
                Status = status,
                CreatedAtStart = createdAtStart,
                CreatedAtEnd = createdAtEnd,
                UpdatedAtStart = updatedAtStart,
                UpdatedAtEnd = updatedAtEnd
            }, _grpcService._metadata);

            List<Protos.Application> applications = [];

            while (await call.ResponseStream.MoveNext())
            {
                applications.Add(call.ResponseStream.Current);
            }
            return applications;
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

    public Protos.Application GetApplication(Protos.Application application)
    {
        try
        {
            var response = _grpcService.applicationClient.GetApplication(application, _grpcService._metadata);
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

    public Protos.Application UpdateApplication(Protos.Application oldApplication, Protos.Application newApplication)
    {
        try
        {
            var response = _grpcService.applicationClient.UpdateApplication(new()
            {
                OldData = oldApplication,
                NewData = newApplication
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
