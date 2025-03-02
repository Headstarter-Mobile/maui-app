using Headstarter.Protos;

namespace Headstarter.Services;

public interface IUserService
{
    Task<UserData> AuthenticateUser(string username, string password, UserType userType);
    // ... other user-related methods
}