using ConfHub.Core.Application.Common.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ConfHub.Core.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 50000;

        public string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Password cant be null or empty");
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = KeyDerivation.Pbkdf2
                (
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: Iterations,
                    numBytesRequested: KeySize
                );
            byte[] hashBytes = new byte[SaltSize + KeySize];
            Buffer.BlockCopy(salt, 0, hashBytes, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, hashBytes, SaltSize, KeySize);
            return Convert.ToBase64String(hashBytes);
        }

        public bool Verify(string password, string hash)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(hash))
                return false;

            try
            {
                byte[] hashBytes = Convert.FromBase64String(hash);

                if (hashBytes.Length != SaltSize + KeySize)
                    return false;

                byte[] salt = new byte[SaltSize];
                byte[] storedHash = new byte[KeySize];
                Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);
                Buffer.BlockCopy(hashBytes, SaltSize, storedHash, 0, KeySize);

                byte[] computedHash = KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: Iterations,
                    numBytesRequested: KeySize
                );

                return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
            }
            catch
            {
                return false;
            }
        }
    }
}
