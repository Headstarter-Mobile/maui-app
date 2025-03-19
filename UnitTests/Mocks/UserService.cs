using Headstarter.Interfaces;
using Headstarter.Protos;

namespace UnitTests.Mocks;

public class UserService : IUserService
{
    public User AuthenticateUser(string username, string password)
    {
        throw new NotImplementedException();
    }

    Task<ICollection<User>> IUserService.GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public User CreateUser(User user)
    {
        throw new NotImplementedException();
    }

    public User UpdateUser(User oldUser, User newUser)
    {
        throw new NotImplementedException();
    }

    public User DeleteUser(User user)
    {
        throw new NotImplementedException();
    }
}