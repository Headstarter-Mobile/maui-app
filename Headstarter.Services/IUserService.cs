using Headstarter.Protos;

namespace Headstarter.Services;

public interface IUserService
{
    User AuthenticateUser(string username, string password);
    Task<ICollection<User>> GetAllUsers();
    User CreateUser(User user);
    User UpdateUser(User oldUser, User newUser);
    User DeleteUser(User user);
}