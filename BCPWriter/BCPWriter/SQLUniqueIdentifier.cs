using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL uniqueidentifier.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187942.aspx">uniqueidentifier (Transact-SQL)</a><br/>
    /// <br/>
    /// From SQL Server 2008 Books Online:<br/>
    /// <br/>
    /// A column or local variable of uniqueidentifier data type can be initialized to a value in the following ways:<br/>
    /// * By using the NEWID function.<br/>
    /// * By converting from a string constant in the form xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx,
    ///   in which each x is a hexadecimal digit in the range 0-9 or a-f.
    ///   For example, 6F9619FF-8B86-D011-B42D-00C04FC964FF is a valid uniqueidentifier value.<br/>
    /// </remarks>
    public class SQLUniqueIdentifier : IBCPSerialization
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uuid">
        /// Globally Unique Identifier,
        /// see http://en.wikipedia.org/wiki/Globally_Unique_Identifier
        /// </param>
        /// <returns></returns>
        public byte[] ToBCP(Guid guid)
        {
            if (string.IsNullOrEmpty(guid.ToString()))
            {
                throw new ArgumentNullException("Empty guid");
            }

            //byte is 1 byte long :)
            //Guid is always of length 16
            byte size = 16;
            byte[] sizeBytes = { size };

            //int is 4 bytes long
            byte[] valueBytes = guid.ToByteArray();

            return Util.ConcatByteArrays(sizeBytes, valueBytes);
        }
    }
}
