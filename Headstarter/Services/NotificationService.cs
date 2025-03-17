using Headstarter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Headstarter.Protos;
using Grpc.Core;
using Microsoft.Maui.Platform;


namespace Headstarter.Services
{
    public class NotificationService : INotificationService
    {
        private readonly GrpcService _grpcService; // For API calls

        public NotificationService(GrpcService grpcService)
        {
            _grpcService = grpcService;

        }
        public async Task<IList<Notification>> GetUnseenMessagesAfter(int Id)
        {
            try
            {
                var client = _grpcService.notificationClient;
                using var call = client.GetUnseenMessages(new GetUnseenMessagesRequest()
                {
                    Id = Id
                }, _grpcService._metadata);
                List<Notification> notifications = new List<Notification>();

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

        public bool MarkAsRead(int Id)
        {
            try
            {
                var client = _grpcService.notificationClient;
                var response = client.MarkAsRead(new MarkAsReadRequest()
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
}
