using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.API.Security
{
    public class EncryptionHelper
    {
        private static string EncryptionKey = "E8qmhkkBxA1m18mwoxmIM5Rhs1ZKOKlD";
        public static string Encrypt(string text)
        {
            Aes aes = Aes.Create();

            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.KeySize = 256;
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.IV = new byte[16];
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter csEncryptWriter = new StreamWriter(csEncrypt))
                    {
                        csEncryptWriter.Write(text);
                    }
                }
                string result = Convert.ToBase64String(msEncrypt.ToArray());
                return result;
            }
        }
        public static string Decrypt(string text)
        {
            string decryptedText = string.Empty;

            if (!string.IsNullOrEmpty(text))
            {
                byte[] fullCipher = Convert.FromBase64String(text);
                byte[] key = Encoding.UTF8.GetBytes(EncryptionKey);

                Aes aes = Aes.Create();
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.KeySize = 256;
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aes.IV = new byte[16];

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var msDecrypt = new MemoryStream(fullCipher))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader csDecryptReader = new StreamReader(csDecrypt))
                        {
                            decryptedText = csDecryptReader.ReadToEnd();
                        }
                    }
                }
            }
            return decryptedText;
        }
    }
}
