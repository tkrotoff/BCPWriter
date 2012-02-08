using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL int.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187745.aspx">int, bigint, smallint, and tinyint (Transact-SQL)</a><br/>
    /// <br/>
    /// From SQL Server 2008 Books Online:<br/>
    /// <br/>
    /// Data type | Range 	                                                                 | Storage<br/>
    /// bigint    | -2^63 (-9,223,372,036,854,775,808) to 2^63-1 (9,223,372,036,854,775,807) | 8 Bytes<br/>
    /// int       | -2^31 (-2,147,483,648) to 2^31-1 (2,147,483,647)                         | 4 Bytes<br/>
    /// smallint  | -2^15 (-32,768) to 2^15-1 (32,767)                                       | 2 Bytes<br/>
    /// tinyint   | 0 to 255                                                                 | 1 Byte<br/>
    /// </remarks>
    public class SQLInt : IBCPSerialization
    {
        /// <summary>
        /// Minimum value allowed for SQL int.
        /// Math.Pow(-2, 31) = -2147483648
        /// </summary>
        public const int MIN_VALUE = -2147483648;

        /// <summary>
        /// Maximum value allowed for SQL int.
        /// Math.Pow(2, 31) - 1 = 2147483647
        /// </summary>
        public const int MAX_VALUE = 2147483647;

        public void Write(BinaryWriter writer, object value)
        {
            Write(writer, (int?)value);
        }

        public static void Write(BinaryWriter writer, int? value)
        {
            if (!value.HasValue)
            {
                //1 byte long
                byte[] nullBytes = { 255 };
                writer.Write(nullBytes);
                return;
            }

            //value must be from -2^31 through -2^31-1
            //value is an int so it is already the case

            //byte is 1 byte long :)
            const byte size = 4;
            writer.Write(size);

            //int is 4 bytes long
            writer.Write(value.Value);
        }
    }
}
