using System.Security.Cryptography;

namespace Ferreteria.Services
{
    public class PasswordService
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 600000;

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                HashSize);

            return Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            try
            {
                string[] partes = passwordHash.Split(':');

                if (partes.Length != 2)
                    return false;

                byte[] salt = Convert.FromBase64String(partes[0]);
                byte[] hashOriginal = Convert.FromBase64String(partes[1]);

                if (salt.Length != SaltSize || hashOriginal.Length != HashSize)
                    return false;

                byte[] hashCalculado = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    Iterations,
                    HashAlgorithmName.SHA256,
                    HashSize);

                return CryptographicOperations.FixedTimeEquals(
                    hashOriginal,
                    hashCalculado);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}