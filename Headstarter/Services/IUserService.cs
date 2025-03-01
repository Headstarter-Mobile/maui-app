using Headstarter.Models;

namespace Headstarter.Services;

public interface IUserService
{
    Task<User> AuthenticateUser(string username, string password, UserType userType);
    // ... other user-related methods
}