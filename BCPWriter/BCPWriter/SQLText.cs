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
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187993.aspx">ntext, text, and image (Transact-SQL)</a><br/>
    /// <br/>
    /// ntext, text, and image data types will be removed in a future version of Microsoft SQL Server.
    /// </remarks>
    [Obsolete]
    public class SQLText : IBCPSerialization
    {
        //2^31-1 (2,147,483,647)
        public const uint MAX_LENGTH = SQLInt.MAX_VALUE;

        public byte[] ToBCP(string text)
        {
            if (text.Length > MAX_LENGTH)
            {
                throw new ArgumentException("text is longer than 2^31-1 (2,147,483,647)");
            }

            //uint is 4 bytes long
            byte[] sizeBytes = BitConverter.GetBytes((uint)(text.Length));

            return Util.ConcatByteArrays(sizeBytes, Util.EncodeToOEMCodePage(text));
        }
    }
}
