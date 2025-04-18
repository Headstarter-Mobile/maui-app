using Headstarter.Protos;
using System.IO;

namespace Headstarter.Services;

public interface IUserService
{
    Task<LoggedUserData> LoginUser(User user);
    User GetUser(User filter);
    User CreateUser(User user);
    User UpdateUser(UserUpdateRequest updateData);
    User DeleteUser(User filters);
}