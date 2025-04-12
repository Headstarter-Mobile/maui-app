using Headstarter.Protos;
using Grpc.Core;


namespace Headstarter.Services;

public class NotificationService : INotificationService
{
    private readonly GrpcService _grpcService; // For API calls

    public NotificationService(GrpcService grpcService)
    {
        _grpcService = grpcService;

    }
    public async Task<ICollection<Notification>> GetUnseenMessagesAfter(int Id)
    {
        try
        {
            var client = _grpcService.notificationClient;
            using var call = client.GetUnseenMessages(new GetUnseenMessagesRequest()
            {
                Id = Id
            }, _grpcService._metadata);
            List<Notification> notifications = [];

            while (await call.ResponseStream.MoveNext())
            {
                notifications.Add(call.ResponseStream.Current);
            }
            return notifications;
        }
        catch (RpcException ex)
        {
            if (ex.Status.StatusCode.Equals(StatusCode.NotFound))
            {
                // user not found
                return Array.Empty<Notification>();
            }
            else if (ex.Status.StatusCode.Equals(StatusCode.PermissionDenied))
            {
                // permission denied
                return Array.Empty<Notification>();
            }
            else
            {
                // other error
                return Array.Empty<Notification>();
            }
        }
    }

    public async Task<bool> MarkAsRead(int Id)
    {
        try
        {
            var client = _grpcService.notificationClient;
            var response = await client.MarkAsReadAsync(new MarkAsReadRequest()
            {
                Id = Id
            }, _grpcService._metadata);
            return true;
        }
        catch (RpcException ex)
        {
            if (ex.Status.StatusCode.Equals(StatusCode.NotFound))
            {
                // user not found
                return false;
            }
            else if (ex.Status.StatusCode.Equals(StatusCode.PermissionDenied))
            {
                // permission denied
                return false;
            }
            else
            {
                // other error
                return false;
            }
        }
    }
}
