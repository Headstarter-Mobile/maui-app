using Grpc.Net.Client;
using Headstarter.Protos;

namespace Headstarter.Services
{
    public sealed class GrpcService
    {
        private readonly GrpcChannel channel;
        public readonly JobSearch.JobSearchClient jobSearchClient;
        public readonly Users.UsersClient usersClient;

        private static readonly GrpcService instance;

        static GrpcService()
        {
            instance = new GrpcService();
        }

        private GrpcService()
        {
            channel = GrpcChannel.ForAddress("http://129.159.196.117:5001");
            jobSearchClient = new JobSearch.JobSearchClient(channel);
            usersClient = new Users.UsersClient(channel);
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
