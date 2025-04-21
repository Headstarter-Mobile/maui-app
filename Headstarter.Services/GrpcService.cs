using Grpc.Core;
using Grpc.Net.Client;

namespace Headstarter.Services;

public sealed class GrpcService
{
    private readonly GrpcChannel channel;
    public readonly Protos.UserService.UserServiceClient usersClient;
    public readonly Protos.PositionService.PositionServiceClient positionClient;
    public readonly Protos.OfficeService.OfficeServiceClient officeClient;
    public readonly Protos.NotificationService.NotificationServiceClient notificationClient;
    public readonly Protos.CompanyService.CompanyServiceClient companyClient;
    public readonly Protos.ApplicationService.ApplicationServiceClient applicationClient;
    public readonly Protos.NotificationService.NotificationServiceClient notificationServiceClient;
    public readonly Metadata _metadata;

    public GrpcService()
    {
#if DEBUG
        channel = GrpcChannel.ForAddress("http://localhost:5001");
        //channel = GrpcChannel.ForAddress("http://129.159.196.117:5001");
#else
        channel = GrpcChannel.ForAddress("http://129.159.196.117:5001");
#endif
        _metadata = new Metadata
        {
            { "token", "" }
        };
        usersClient = new Protos.UserService.UserServiceClient(channel);
        positionClient = new Protos.PositionService.PositionServiceClient(channel);
        officeClient = new Protos.OfficeService.OfficeServiceClient(channel);
        notificationClient = new Protos.NotificationService.NotificationServiceClient(channel);
        companyClient = new Protos.CompanyService.CompanyServiceClient(channel);
        applicationClient = new Protos.ApplicationService.ApplicationServiceClient(channel);
        notificationServiceClient = new Protos.NotificationService.NotificationServiceClient(channel);
    }
}
