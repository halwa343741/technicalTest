using System.Security.Cryptography;
using System.Text;

namespace VehicleRESTAPI.Repo
{
    public static class helper
    {
        private static readonly string key = "KHf7oKp7FObYX+lFRci/hLLCkWZsEShvBpU8CllzxMw=";
        private static readonly string iv = "PmoanCSeQsY2eRNvZbHCdQ==";

        public static string? Encrypt(this string s) {
            try {
                using Aes aesAlg = Aes.Create();
                aesAlg.Key = Convert.FromBase64String(key);
                aesAlg.IV = Convert.FromBase64String(iv);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msEncrypt = new MemoryStream();
                using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {
                    swEncrypt.Write(s);
                }

                byte[] encryptedBytes = msEncrypt.ToArray();
                string encryptedText = Convert.ToBase64String(encryptedBytes);
                return encryptedText.StringToAscii();
            }
            catch {
                return null;
            }
        }

        public static string? Decrypt(this string s) {
            try {
                var v = s.AsciiToString();
                if (v == null) return v;
                byte[] cipherText = Convert.FromBase64String(v);

                using Aes aesAlg = Aes.Create();
                aesAlg.Key = Convert.FromBase64String(key);
                aesAlg.IV = Convert.FromBase64String(iv);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msDecrypt = new MemoryStream(cipherText);
                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new StreamReader(csDecrypt);

                string plaintext = srDecrypt.ReadToEnd();
                return plaintext;
            }
            catch {
                return null;
            }
        }

        private static string? StringToAscii(this string s) {
            try {
                StringBuilder asciiBuilder = new StringBuilder();

                foreach (char c in s) asciiBuilder.Append((int)c + "-");

                return asciiBuilder.ToString().TrimEnd("-".ToCharArray());
            }
            catch {
                return null;
            }
        }

        private static string? AsciiToString(this string s) {
            try {
                string[] asciiValues = s.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder stringBuilder = new StringBuilder();

                foreach (string asciiValue in asciiValues) {
                    int intValue;
                    if (int.TryParse(asciiValue, out intValue)) {
                        char charValue = (char)intValue;
                        stringBuilder.Append(charValue);
                    }
                }

                return stringBuilder.ToString();
            }
            catch {
                return null;
            }
        }

        public static string getToken(this HttpContext context) {
            try {
                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                if (authHeader == null || !authHeader.StartsWith("Bearer ")) return "";
                return authHeader.Substring("Bearer ".Length).Trim();
            }
            catch {
                return "";
            }
        }

        public static bool isNullOrEmpty(this string s) {
            return string.IsNullOrEmpty(s);
        }
    }
}
