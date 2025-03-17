using Grpc.Core;
using Grpc.Net.Client;
using Headstarter.Protos;
using System.Reflection.Metadata;

namespace Headstarter.Services
{
    public sealed class GrpcService
    {
        private readonly GrpcChannel channel;
        public readonly Headstarter.Protos.UserService.UserServiceClient usersClient;
        public readonly Headstarter.Protos.PositionService.PositionServiceClient positionClient;
        public readonly Headstarter.Protos.OfficeService.OfficeServiceClient officeClient;
        public readonly Headstarter.Protos.NotificationService.NotificationServiceClient notificationClient;
        public readonly Headstarter.Protos.CompanyService.CompanyServiceClient companyClient;
        public readonly Headstarter.Protos.ApplicationService.ApplicationServiceClient applicationClient;
        public readonly Headstarter.Protos.NotificationService.NotificationServiceClient notificationServiceClient;
        public readonly Metadata _metadata; 


        private static readonly GrpcService instance;

        static GrpcService()
        {
            instance = new GrpcService();
        }

        private GrpcService()
        {
            channel = GrpcChannel.ForAddress("http://129.159.196.117:5001");
            _metadata = new Metadata();
            _metadata.Add("token", "");
            usersClient = new Headstarter.Protos.UserService.UserServiceClient(channel);
            positionClient = new Headstarter.Protos.PositionService.PositionServiceClient(channel);
            officeClient = new Headstarter.Protos.OfficeService.OfficeServiceClient(channel);
            notificationClient = new Headstarter.Protos.NotificationService.NotificationServiceClient(channel);
            companyClient = new Headstarter.Protos.CompanyService.CompanyServiceClient(channel);
            applicationClient = new Headstarter.Protos.ApplicationService.ApplicationServiceClient(channel);
            notificationServiceClient = new Headstarter.Protos.NotificationService.NotificationServiceClient(channel);

        }

        public static GrpcService Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
