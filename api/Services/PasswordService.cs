using System.Security.Cryptography;

namespace API.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string password);
    }

    public class PasswordService : IPasswordService
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32; // 256 bit
        private const int Iterations = 10000; // Antal iterationer

        public string HashPassword(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
                       password,
                       SaltSize,
                       Iterations,
                       HashAlgorithmName.SHA256))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                // Format: {iterations}.{salt}.{hash}
                return $"{Iterations}.{salt}.{key}";
            }
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            var parts = hashedPassword.Split('.', 3);

            if (parts.Length != 3)
            {
                return false;
            }

            var iterations = int.Parse(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
                       password,
                       salt,
                       iterations,
                       HashAlgorithmName.SHA256))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);
                return AreEqual(key, keyToCheck);
            }
        }

        private bool AreEqual(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }
    }
}
