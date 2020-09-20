using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PatientManagement.API.Security;

namespace PatientManagement.API.Services
{
    public static class Helper
    {
        public static int DecryptInt(string value)
        {
            int result = 0;

            if (!string.IsNullOrEmpty(value))
            {
                result = int.Parse(EncryptionHelper.Decrypt(value));
            }
            return result;
        }

        public static string EncyptString(int value)
        {
            return EncryptionHelper.Encrypt(value.ToString());
        }

        public static string GenerateRandomCode()
        {
            char[] chars = new char[62];
            chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[8];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(8);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
