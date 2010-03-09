using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// Obsolete! SQL ntext.
    /// </summary>
    /// 
    /// <remarks>
    /// <see cref="SQLChar"/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187993.aspx">ntext, text, and image (Transact-SQL)</a><br/>
    /// <br/>
    /// ntext, text, and image data types will be removed in a future version of Microsoft SQL Server.
    /// </remarks>
    [Obsolete]
    public class SQLNText : IBCPSerialization
    {
        /// <summary>
        /// Maximum length allowed for SQL ntext.
        /// 2^30 - 1 = 1,073,741,823
        /// </summary>
        public const uint MAX_LENGTH = 1073741823;

        public void Write(BinaryWriter writer, object value)
        {
            Write(writer, (string)value);
        }

        public void Write(BinaryWriter writer, string text)
        {
            if (text == null)
            {
                //4 bytes long
                byte[] nullBytes = { 255, 255, 255, 255 };
                writer.Write(nullBytes);
                return;
            }

            //This cannot be tested anyway since OutOfMemory exception is
            //thrown before
            /*if (text.Length > MAX_LENGTH)
            {
                throw new ArgumentException("text is longer than 2^30 - 1 (1,073,741,823)");
            }*/

            //uint is 4 bytes long
            //* 2 because we are in UTF-16, thus 1 char is 2 bytes long
            uint length = (uint)(text.Length * 2);
            writer.Write(length);

            //Text should be in unicode UTF-16
            writer.Write(Encoding.Unicode.GetBytes(text));
        }
    }
}
