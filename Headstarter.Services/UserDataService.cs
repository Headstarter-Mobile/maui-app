using Headstarter.Protos;

namespace Headstarter.Services
{
    /// <summary>
    /// Keeps track of persistant user data after the login
    /// </summary>
    public class UserDataService
    {
        public static UserDataService Instance { get; } = new UserDataService();
        public User MyUser { get; private set; }
        private UserDataService()
        {
        }

        public void SetUser(User user)
        {
            MyUser = user;
        }
    }
}
