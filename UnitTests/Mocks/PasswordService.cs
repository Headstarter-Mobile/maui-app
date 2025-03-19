using Headstarter.Interfaces;

namespace UnitTests.Mocks
{
    internal class PasswordService : IPasswordService
    {
        public Task<string> Hash(string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyPassword(string password, string hashedPassword)
        {
            throw new NotImplementedException();
        }
    }
}