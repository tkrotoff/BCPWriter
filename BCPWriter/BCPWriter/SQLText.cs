using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// Obsolete! SQL text.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187993.aspx">ntext, text, and image (Transact-SQL)</a><br/>
    /// <br/>
    /// ntext, text, and image data types will be removed in a future version of Microsoft SQL Server.
    /// </remarks>
    [Obsolete]
    public class SQLText : IBCPSerialization
    {
        /// <summary>
        /// Maximum length allowed for SQL text.
        /// 2^31-1 = 2,147,483,647
        /// </summary>
        public const uint MAX_LENGTH = SQLInt.MAX_VALUE;

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

            if (text.Length > MAX_LENGTH)
            {
                throw new ArgumentException("text is longer than 2^31-1 (2,147,483,647)");
            }

            //uint is 4 bytes long
            writer.Write((uint)(text.Length));

            writer.Write(Util.EncodeToOEMCodePage(text));
        }
    }
}
