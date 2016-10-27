using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.Shared.Extentions
{
    public static class StringExtentions
    {
        private static readonly byte[] Salt = Encoding.ASCII.GetBytes("a;@Xdtd6h>0bZ");
        private const string PublicKey = "P+1X0g*XB_c[#";

        private const int PREFIXED_IV_LENGTH = 16;

        /// <summary>
        /// Removes "+" sign from provided sring
        /// </summary>
        /// <param name="str">string to update</param>
        /// <returns></returns>
        public static string RemovePlusSign(this string str)
        {
            return str.Replace("+", String.Empty);
        }
        /// <summary>
        /// Removes spaces from formatted number
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSpace(this string str)
        {
            return str.Replace(" ", String.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64EncodedString"></param>
        /// <param name="decryptionKey"></param>
        /// <returns></returns>
        public static string DecryptStringFromBase64Aes(this string base64EncodedString, string decryptionKey)
        {
            if (string.IsNullOrEmpty(base64EncodedString))
                throw new ArgumentNullException("Base64EncodedString", "The string to be decrypted is null or empty");

            var decodedBytes = Convert.FromBase64String(base64EncodedString);

            var decodedText = new byte[decodedBytes.Length - PREFIXED_IV_LENGTH];
            var IV = new byte[PREFIXED_IV_LENGTH];

            Buffer.BlockCopy(decodedBytes, 16, decodedText, 0, decodedText.Length);
            Buffer.BlockCopy(decodedBytes, 0, IV, 0, IV.Length);

            var myRijndael = new RijndaelManaged();

            var key = Convert.FromBase64String(decryptionKey);

            return DecryptStringFromBytesAes(decodedText, key, IV);

        }

        /// <summary>
        /// Decrypts using AES 128 algorithm
        /// </summary>
        /// <param name="cipherText">cipherText</param>
        /// <param name="Key">Key</param>
        /// <param name="IV">IV</param>
        /// <returns>plainText</returns>
        public static string DecryptStringFromBytesAes(this byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // TDeclare the streams used
            // to decrypt to an in memory
            // array of bytes.
            MemoryStream msDecrypt = null;
            CryptoStream csDecrypt = null;
            StreamReader srDecrypt = null;

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try
            {
                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                msDecrypt = new MemoryStream(cipherText);
                csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                srDecrypt = new StreamReader(csDecrypt);

                // Read the decrypted bytes from the decrypting stream
                // and place them in a string.
                plaintext = srDecrypt.ReadToEnd();
            }
            finally
            {
                // Clean things up.

                // Close the streams.
                if (srDecrypt != null)
                    srDecrypt.Close();
                if (csDecrypt != null)
                    csDecrypt.Close();
                if (msDecrypt != null)
                    msDecrypt.Close();

                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;

        }

        /// <summary>
        /// Encode the given string using Rijndael AES.  The string can be decode using 
        /// ChildCareMgmtStringEncode().  The publicKey parameters must match.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="publicKey">A password used to generate a key for encryption.</param>
        public static string ChildCareMgmtStringEncode(this string plainText)
        {
            string outStr;                       // Encrypted string to return
            RijndaelManaged aesAlg = null;       // RijndaelManaged object used to encrypt the data.

            try
            {
                // generate the key from the shared password and the salt
                var key = new Rfc2898DeriveBytes(PublicKey, Salt);

                // Create a RijndaelManaged object
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Create a decrytor to perform the stream transform.
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    // prepend the IV
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText.Compress());
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return outStr;
        }

        /// <summary>
        /// Decode the given string.  Assumes the string was encode using 
        /// ChildCareMgmtStringDecode(), using an identical publicKey.
        /// </summary>
        /// <param name="cipherText">The text to decrypt.</param>
        /// <param name="publicKey">A password used to generate a key for decryption.</param>
        public static string ChildCareMgmtStringDecode(this string cipherText)
        {
            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold the decrypted text.
            string plaintext;

            try
            {
                // generate the key from the shared password and the salt
                var key = new Rfc2898DeriveBytes(PublicKey, Salt);
                //qc29054 - sit environment
                cipherText = cipherText.Replace('-', '+');
                cipherText = cipherText.Replace('_', '/');
                // Create the streams used for decryption.                
                byte[] bytes = Convert.FromBase64String(cipherText);
                using (var msDecrypt = new MemoryStream(bytes))
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    // Get the initialization vector from the encrypted stream
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    // Create a decrytor to perform the stream transform.
                    var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            var compressedPlaintext = srDecrypt.ReadToEnd();
                            plaintext = compressedPlaintext.Decompress();
                        }
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }

        private static byte[] ReadByteArray(Stream s)
        {
            var rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            var buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }


        public static string Compress(this string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);

            memoryStream.Close();
            memoryStream.Dispose();

            return Convert.ToBase64String(gZipBuffer);
        }

        public static string Decompress(this string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }
    }
}

