using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    /// <summary>
    /// Provides encryption and decryption for strings
    /// </summary>
    static class Encryptor
    {
        // Set the password
        static readonly byte[] KEY = new byte[32] { 118, 123, 23, 17, 161, 152, 35, 68, 126, 213, 16, 115, 68, 217, 58, 108, 56, 218, 5, 78, 28, 128, 113, 208, 61, 56, 10, 87, 187, 162, 233, 38 };

        // Set the IV (initialization vector) value
        static readonly byte[] IV = new byte[16] { 33, 241, 14, 16, 103, 18, 14, 248, 4, 54, 18, 5, 60, 76, 16, 191 };

        static public byte[] Encrypt(string rawInput)
        {
            // Will contain the encrypted value later
            byte[] encrypted;

            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                // Create an encryptor to perform the stream transform
                rijAlg.Padding = PaddingMode.PKCS7;
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(KEY, IV);
                // Create the streams used for encryption
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream
                            swEncrypt.Write(rawInput);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        public static string Decrypt(string inputString)
        {
            // Will contain the decrypted value later
            string decrypted = null;
            byte[] inputBytes = Convert.FromBase64String(inputString);
            // Gets the input string and converts it to a byte array
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Padding = PaddingMode.PKCS7;
                // Create a decryptor to perform the stream transform
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(KEY, IV);

                // Create the streams used for decryption
                using (MemoryStream msDecrypt = new MemoryStream(inputBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream and place them in a string
                            decrypted = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return decrypted;
        }
    }
}
