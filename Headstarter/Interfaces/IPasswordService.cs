namespace Headstarter.Interfaces
{
    internal interface IPasswordService
    {
        Task<string> Hash(string password);
        Task<bool> VerifyPassword(string password, string hashedPassword);    
    }
}