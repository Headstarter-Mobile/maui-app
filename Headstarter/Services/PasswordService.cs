using Headstarter.Interfaces;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Headstarter.Services
{
    internal class PasswordService : IPasswordService
    {
        private static readonly PasswordService instance;
        private static readonly int memorySize = 65536; // 1 GB
        private static readonly int degreeOfParallelism = 4;
        private static readonly int iterations = 4;
        private static readonly int saltSize = 16;

        static PasswordService()
        {
            instance = new PasswordService();
        }
        private PasswordService()
        {
        }
        public async Task<string> Hash(string password)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = RandomNumberGenerator.GetBytes(saltSize),
                MemorySize = memorySize,
                DegreeOfParallelism = degreeOfParallelism,
                Iterations = iterations
            };

            var hashBytes = await argon2.GetBytesAsync(32);

            // Combine salt and hash for storage
            byte[] combinedBytes = new byte[argon2.Salt.Length + hashBytes.Length];
            Array.Copy(argon2.Salt, 0, combinedBytes, 0, argon2.Salt.Length);
            Array.Copy(hashBytes, 0, combinedBytes, argon2.Salt.Length, hashBytes.Length);

            return Convert.ToBase64String(combinedBytes);
        }
        public async Task<bool> VerifyPassword(string password, string hashedPassword)
        {
            byte[] combinedBytes = Convert.FromBase64String(hashedPassword);

            // Extract salt and hash
            byte[] salt = new byte[saltSize]; // Assuming 16-byte salt
            byte[] hashBytes = new byte[combinedBytes.Length - salt.Length];

            Array.Copy(combinedBytes, 0, salt, 0, salt.Length);
            Array.Copy(combinedBytes, salt.Length, hashBytes, 0, hashBytes.Length);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = RandomNumberGenerator.GetBytes(saltSize),
                MemorySize = memorySize,
                DegreeOfParallelism = degreeOfParallelism,
                Iterations = iterations
            };

            byte[] newHashBytes = await argon2.GetBytesAsync(hashBytes.Length);

            return CryptographicOperations.FixedTimeEquals(hashBytes, newHashBytes);
        }
    }
}
