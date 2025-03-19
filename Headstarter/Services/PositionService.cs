using Grpc.Core;
using Headstarter.Interfaces;
using Headstarter.Protos;

namespace Headstarter.Services
{
    public class PositionService : IPositionService
    {
        private readonly GrpcService _grpcService; // For API calls

        public PositionService(GrpcService grpcService)
        {
            _grpcService = grpcService;

        }
        public Position CreatePosition(Position position)
        {
            try
            {
                var response = _grpcService.positionClient.CreatePosition(position, _grpcService._metadata);
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

        public Position DeletePosition(Position position)
        {
            try
            {
                var response = _grpcService.positionClient.DeletePosition(position, _grpcService._metadata);
                return position;
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

        public async Task<ICollection<Position>> GetAllPositions()
        {
            try
            {
                var client = _grpcService.positionClient;
                using var call = client.GetAllPositions(new Google.Protobuf.WellKnownTypes.Empty(), _grpcService._metadata);
                List<Position> positions = [];

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

        public Position GetPosition(Position position)
        {
            try
            {
                var response = _grpcService.positionClient.GetPosition(position

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

        public Position UpdatePosition(Position oldPosition, Position newPosition)
        {
            try
            {

                var response = _grpcService.positionClient.UpdatePosition(new Headstarter.Protos.PositionUpdateRequest()
                {
                    OldData = oldPosition,
                    NewData = newPosition
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
