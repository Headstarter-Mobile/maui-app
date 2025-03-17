using Headstarter.Protos;
using System.IO;

namespace Headstarter.Interfaces;

public interface IUserService
{
    User AuthenticateUser(string username, string password, UserRole userType);
    Task <ICollection<User>> GetAllUsers();
    User CreateUser(User user);
    User UpdateUser(User oldUser,User newUser);
    User DeleteUser(User user);
}