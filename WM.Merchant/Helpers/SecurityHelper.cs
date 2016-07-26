using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace WM.Merchant.Helpers
{
    public class SecurityHelper
    {
        /// <summary>
        /// Default string encoding 
        /// </summary>
        private static readonly Encoding encoding = Encoding.UTF8;

        /// <summary>
        /// Encrypts message with HMAC, using HSA1 algorithm 
        /// </summary>
        /// <param name="message">Message needs to be encoded</param>
        /// <param name="secretKey">Secret key</param>
        /// <returns>Encrypted string</returns>
        public static string HMACHSA1(string message, string secretKey)
        {
            string result;
            var keyByte = encoding.GetBytes(secretKey);

            using (var hmac = new HMACSHA1(keyByte))
            {
                hmac.ComputeHash(encoding.GetBytes(message));
                result = StringHelper.ByteToString(hmac.Hash);
            }

            return result;
        }

        /// <summary>
        /// Compares hash string with plain-string message
        /// </summary>
        /// <param name="hash">Hash string need to be compared</param>
        /// <param name="message">Plain message need to be encrypted</param>
        /// <param name="secretKey">Secret key</param>
        /// <returns>Boolean if hash is equal with encrypted message</returns>
        public static bool CompareHMACHSA1(string hash, string message, string secretKey)
        {
            string comparedHash = SecurityHelper.HMACHSA1(message, secretKey);

            return hash == comparedHash;
        }
    }
}
