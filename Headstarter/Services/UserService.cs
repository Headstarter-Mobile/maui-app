using Grpc.Core;
using Headstarter.Interfaces;
using Headstarter.Protos;
using System.Threading.Channels;

namespace Headstarter.Services;

public partial class UserService : IUserService
{
    private readonly GrpcService _grpcService; // For API calls

    public UserService(GrpcService grpcService)
    {
        _grpcService = grpcService;
       
    }

    public User AuthenticateUser(string username, string password, UserRole userType)
    {      
        try
        {
            var response = _grpcService.usersClient.GetUser(new User()
            {
                Name = username,
                Password = password,
                Type = userType
            },_grpcService._metadata);
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

    public User CreateUser(User user)
    {
        try
        {
            var response = _grpcService.usersClient.CreateUser(new User()
            {
                Name = user.Name,
                Password = user.Password,
                Type = user.Type
            },_grpcService._metadata)
            ;
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
            var response = _grpcService.usersClient.DeleteUser(user,_grpcService._metadata);
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

    public async Task <ICollection<User>> GetAllUsers()
    {
        try
        {
            var client = _grpcService.usersClient;
            using var call = client.GetAllUsers(new Google.Protobuf.WellKnownTypes.Empty(), _grpcService._metadata);
            List<User> users = new List<User>();

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

            var response = _grpcService.usersClient.UpdateUser(new Headstarter.Protos.UserUpdateRequest()
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

    // ... other service methods
}
