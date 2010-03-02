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
    /// <see>SQLChar</see>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187993.aspx">ntext, text, and image (Transact-SQL)</a><br/>
    /// <br/>
    /// ntext, text, and image data types will be removed in a future version of Microsoft SQL Server.
    /// </remarks>
    [Obsolete]
    public class SQLNText : IBCPSerialization
    {
        //2^30 - 1 (1,073,741,823)
        public const uint MAX_LENGTH = 1073741823;

        public byte[] ToBCP(string text)
        {
            if (text.Length > MAX_LENGTH)
            {
                throw new ArgumentException("text is longer than 2^30 - 1 (1,073,741,823)");
            }

            //uint is 4 bytes long
            //* 2 because we are in unicode, thus 1 char is 2 bytes long
            uint length = (uint)(text.Length * 2);
            byte[] sizeBytes = BitConverter.GetBytes(length);

            //Text should be in unicode
            return Util.ConcatByteArrays(sizeBytes, Encoding.Unicode.GetBytes(text));
        }
    }
}
