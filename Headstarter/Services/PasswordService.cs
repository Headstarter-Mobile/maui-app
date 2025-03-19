using Headstarter.Interfaces;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Headstarter.Services
{
    public class PasswordService : IPasswordService
    {
        private static readonly PasswordService instance;
        public static PasswordService Instance
        {
            get
            {
                return instance;
            }
        }
        private static readonly int memorySize = 65536; // 1 GB
        private static readonly int degreeOfParallelism = 4;
        private static readonly int iterations = 4;
        private static readonly int saltSize = 16;
        private static readonly int hashSize = 32;

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

            var hashBytes = await argon2.GetBytesAsync(hashSize);

            // Combine salt and hash for storage
            byte[] combinedBytes = new byte[saltSize + hashSize];
            Array.Copy(argon2.Salt, 0, combinedBytes, 0, saltSize);
            Array.Copy(hashBytes, 0, combinedBytes, saltSize, hashSize);

            return Convert.ToBase64String(combinedBytes);
        }
        public async Task<bool> VerifyPassword(string password, string hashedPassword)
        {
            byte[] combinedBytes = Convert.FromBase64String(hashedPassword);

            // Extract salt and hash
            byte[] salt = new byte[saltSize];
            byte[] hashBytes = new byte[hashSize];

            Array.Copy(combinedBytes, 0, salt, 0, salt.Length);
            Array.Copy(combinedBytes, salt.Length, hashBytes, 0, hashBytes.Length);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                MemorySize = memorySize,
                DegreeOfParallelism = degreeOfParallelism,
                Iterations = iterations
            };

            byte[] newHashBytes = await argon2.GetBytesAsync(hashSize);

            return CryptographicOperations.FixedTimeEquals(hashBytes, newHashBytes);
        }
    }
}
