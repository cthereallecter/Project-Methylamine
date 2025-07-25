using System.Security.Cryptography;

namespace ProjectMethylamine.Source.Utility.Cryptography
{
    public static class BitShiftCrypto
    {
        private const int AES_BLOCK_SIZE = 16; // 128-bit
        private const byte DEFAULT_SHIFT = 3;

        public static byte[] Encrypt(byte[] data, string? key = null, byte shift = DEFAULT_SHIFT)
        {
            if (data == null || data.Length == 0)
                return data ?? Array.Empty<byte>();

            key ??= "ProjectMethylamine";

            byte[] keyBytes = DeriveKeyFromString(key);
            byte[] iv = RandomNumberGenerator.GetBytes(AES_BLOCK_SIZE);

            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            ms.Write(iv, 0, iv.Length); // prepend IV
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(data, 0, data.Length);
            }

            return ms.ToArray();
        }

        public static byte[] Decrypt(byte[] encryptedData, string? key = null, byte shift = DEFAULT_SHIFT)
        {
            if (encryptedData == null || encryptedData.Length <= AES_BLOCK_SIZE)
                return Array.Empty<byte>();

            key ??= "ProjectMethylamine";

            byte[] keyBytes = DeriveKeyFromString(key);
            byte[] iv = encryptedData.Take(AES_BLOCK_SIZE).ToArray();
            byte[] cipher = encryptedData.Skip(AES_BLOCK_SIZE).ToArray();

            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(cipher);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var result = new MemoryStream();
            cs.CopyTo(result);

            return result.ToArray();
        }

        private static byte[] DeriveKeyFromString(string key)
        {
            using var sha = SHA256.Create();
            return sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
        }

        public static async Task<bool> EncryptFileAsync(string inputPath, string outputPath, string? key = null, byte shift = DEFAULT_SHIFT)
        {
            try
            {
                if (!File.Exists(inputPath))
                    return false;

                byte[] data = await File.ReadAllBytesAsync(inputPath);
                byte[] encrypted = Encrypt(data, key, shift);

                string? directory = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(directory))
                    Directory.CreateDirectory(directory);

                await File.WriteAllBytesAsync(outputPath, encrypted);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> DecryptFileAsync(string inputPath, string outputPath, string? key = null, byte shift = DEFAULT_SHIFT)
        {
            try
            {
                if (!File.Exists(inputPath))
                    return false;

                byte[] data = await File.ReadAllBytesAsync(inputPath);
                byte[] decrypted = Decrypt(data, key, shift);

                string? directory = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(directory))
                    Directory.CreateDirectory(directory);

                await File.WriteAllBytesAsync(outputPath, decrypted);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}