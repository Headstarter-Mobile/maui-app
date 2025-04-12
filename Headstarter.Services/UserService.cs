using Grpc.Core;
using Headstarter.Protos;

namespace Headstarter.Services;

public partial class UserService : IUserService
{
    private readonly GrpcService _grpcService; // For API calls

    public UserService(GrpcService grpcService)
    {
        _grpcService = grpcService;

    }

    public async Task<LoggedUserData> AuthenticateUser(string email, string password)
    {
        try
        {
            var hashedPassword = await PasswordService.Instance.Hash(password);
            var response = await _grpcService.usersClient.LoginUserAsync(new()
            {
                Email = email,
                Password = password
            }, _grpcService._metadata, DateTime.UtcNow.AddSeconds(7));
            return response;
        }
        catch (RpcException ex)
        {
            if (ex.Status.StatusCode.Equals(StatusCode.DeadlineExceeded))
            {
                // connection failed
                return null;
            }
            else if (ex.Status.StatusCode.Equals(StatusCode.NotFound))
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

    public User CreateUser(User user)
    {
        try
        {
            var response = _grpcService.usersClient.CreateUser(user, _grpcService._metadata);
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

    public User DeleteUser(User user)
    {
        try
        {
            var response = _grpcService.usersClient.DeleteUser(user, _grpcService._metadata);
            return user;
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
    public async Task<ICollection<User>> GetAllUsers()
    {
        try
        {
            var client = _grpcService.usersClient;
            using var call = client.GetAllUsers(new Google.Protobuf.WellKnownTypes.Empty(), _grpcService._metadata);
            List<User> users = [];

            while (await call.ResponseStream.MoveNext())
            {
                users.Add(call.ResponseStream.Current);
            }
            return users;
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

    public User UpdateUser(User oldUser, User newUser)
    {
        try
        {
            var response = _grpcService.usersClient.UpdateUser(new()
            {
                OldData = oldUser,
                NewData = newUser
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
