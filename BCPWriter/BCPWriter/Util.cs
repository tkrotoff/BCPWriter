using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCPWriter
{
    /// <summary>
    /// Internal utility class.
    /// </summary>
    public class Util
    {
        /// <summary>
        /// Encode text using OEM code page, see http://en.wikipedia.org/wiki/Windows_code_page
        /// </summary>
        /// <remarks>
        /// When we use SQLChar and SQLVarChar unicode is not used and text is encoded using OEM code page.
        /// </remarks>
        /// <param name="text">text to encode</param>
        /// <returns>text encoded using OEM code page</returns>
        public static byte[] EncodeToOEMCodePage(string text)
        {
            Encoding enc = Encoding.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
            return enc.GetBytes(text);
        }

        /// <summary>
        /// Converts a byte[] to hexadecimal.
        /// </summary>
        /// <remarks>
        /// See http://stackoverflow.com/questions/623104/c-byte-to-hex-string
        /// </remarks>
        /// <param name="data">data to convert</param>
        /// <returns>string containing hexadecimal</returns>
        public static string ToHexString(byte[] data)
        {
            /*StringBuilder hex = new StringBuilder();
            foreach (byte b in data)
            {
                hex.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "{0:x2}", b);
            }
            return hex.ToString();*/

            return BitConverter.ToString(data).Replace("-", string.Empty);
        }

        /// <summary>
        /// Converts a string to a byte[]
        /// </summary>
        /// <param name="text">text to convert</param>
        /// <returns>byte[]</returns>
        public static byte[] StringToByteArray(string text)
        {
            return Encoding.Default.GetBytes(text);
        }

        /// <summary>
        /// See <a href="http://stackoverflow.com/questions/311165/how-do-you-convert-byte-array-to-hexadecimal-string-and-vice-versa-in-c">How do you convert Byte Array to Hexadecimal String, and vice versa, in C#?</a>
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexToByteArray(string hex)
        {
            int nbChars = hex.Length;
            byte[] bytes = new byte[nbChars / 2];
            for (int i = 0; i < nbChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
