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

    public async Task<LoggedUserData> LoginUser(User user)
    {
        try
        {
            var hashedPassword = await PasswordService.Instance.Hash(user.Password);
            var response = await _grpcService.usersClient.LoginUserAsync(new()
            {
                Email = user.Email,
                Password = user.Password
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
    
    public async Task<ICollection<User>> GetAllUsers(User filters)
    {
        try
        {
            var client = _grpcService.usersClient;
            using var call = client.GetAllUsers(filters, _grpcService._metadata);
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

    public User UpdateUser(UserUpdateRequest updateData)
    {
        try
        {
            var response = _grpcService.usersClient.UpdateUser(updateData, _grpcService._metadata);
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

    public User GetUser(User filter)
    {
        try
        {
            var client = _grpcService.usersClient;
            User user = client.GetUser(filter, _grpcService._metadata);
            
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
}
