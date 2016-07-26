using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Merchant.Helpers
{
    public class StringHelper
    {
        /// <summary>
        /// Convert byte array to string
        /// </summary>
        /// <param name="buffer">Array byte buffer</param>
        /// <returns>Converted string</returns>
        public static string ByteToString(byte[] buffer)
        {
            string result = "";

            for (int i = 0; i < buffer.Length; i++)
            {
                result += buffer[i].ToString("X2"); /* hex format */
            }
            
            return result;
        }
    }
}
