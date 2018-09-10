using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WeddingCD.Common.Encryption
{
    /// <summary>
    /// Class to achieve AES encryption (Rijndael)
    /// </summary>
    public class Aes128Encryption
    {
        /// <summary>
        /// Encrypts the specified plain bytes.
        /// </summary>
        /// <param name="plainBytes">The plain bytes.</param>
        /// <param name="secretKeyBytes">The secret key as bytes.</param>
        /// <returns>
        /// The encrypted bytes
        /// </returns>
        public static byte[] Encrypt(byte[] plainBytes, byte[] secretKeyBytes)
        {
            var keyBytes = new byte[16];
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;

                // Resets the IV
                aesAlg.IV = new byte[aesAlg.IV.Length];

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (var encrypt = new MemoryStream())
                {
                    using (var stream = new CryptoStream(encrypt, encryptor, CryptoStreamMode.Write))
                    {
                        stream.Write(plainBytes, 0, plainBytes.Length);
                        return encrypt.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts the specified encrypted data.
        /// </summary>
        /// <param name="encryptedData">The encrypted data.</param>
        /// <param name="secretKeyBytes">The secret key as bytes.</param>
        /// <returns>
        /// The plain bytes
        /// </returns>
        public static byte[] Decrypt(byte[] encryptedData, byte[] secretKeyBytes)
        {
            var keyBytes = new byte[16];
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;

                // Resets the IV
                aesAlg.IV = new byte[aesAlg.IV.Length];

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (var decrypt = new MemoryStream(encryptedData))
                {
                    using (var stream = new CryptoStream(decrypt, decryptor, CryptoStreamMode.Read))
                    {
                        var res = new byte[encryptedData.Length];
                        stream.Read(res, 0, res.Length);
                        return res;
                    }
                }
            }
        }
    }
}