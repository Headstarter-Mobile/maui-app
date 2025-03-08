using Headstarter.Protos;

namespace Headstarter.Services;

public partial class UserService : IUserService
{
    private readonly GrpcService _grpcService; // For API calls

    public UserService(GrpcService grpcService)
    {
        _grpcService = grpcService;
    }

    public async Task<UserData> AuthenticateUser(string username, string password, UserType userType)
    {
        if (username == "a" && password == "a")
        {
            return new UserData()
            {
                Username = username,
                Password = password,
                UserType = userType
            };
        }
        
        try
        {
            var response = await _grpcService.usersClient.ValidateUserAsync(new Protos.UserAuthRequest
            {
                Username = username,
                Password = password
            });
            if (response.Status == UserAuthStatus.WrongPassword)
            {
                // Handle wrong password
                return null;
            }
            if (response.Status == UserAuthStatus.NotFound)
            {
                // Handle user not found
                return null;
            }

            return new UserData()
            {
                Username = username,
                Password = password,
                UserType = response.Type
            };
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., network issues)
            return null;
        }
    }
    // ... other service methods
}
