using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL float.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms173773.aspx">float and real (Transact-SQL)</a><br/>
    /// <br/>
    /// From SQL Server 2008 Books Online:<br/>
    /// <br/>
    /// Data type      | Range 	                                                     | Storage<br/>
    /// float          | - 1.79E+308 to -2.23E-308, 0 and 2.23E-308 to 1.79E+308     | Depends on the value of n<br/>
    /// float n: 1-24  | Precision: 7 digits                                         | 4 bytes
    /// float n: 25-53 | Precision: 15 digits                                        | 8 bytes
    /// real           | - 3.40E + 38 to -1.18E - 38, 0 and 1.18E - 38 to 3.40E + 38 | 4 Bytes<br/>
    /// </remarks>
    public class SQLFloat : IBCPSerialization
    {
        /// <summary>
        /// Minimum NbBits value allowed for SQL float.
        /// </summary>
        public const ushort MIN_NBBITS = 1;

        /// <summary>
        /// Maximum NbBits value allowed for SQL float.
        /// </summary>
        public const ushort MAX_NBBITS = 53;

        /// <summary>
        /// Minimum NbBits value allowed for SQL float.
        /// </summary>
        public const ushort MIN_DOUBLE_NBBITS = 25;

        /// <summary>
        /// Maximum NbBits value allowed for SQL float.
        /// </summary>
        public const ushort MAX_FLOAT_NBBITS = 24;

        /// <summary>
        /// Creates a SQL float(n).
        /// </summary>
        /// <remarks>
        /// SQL Server treats n as one of two possible values.<br/>
        /// If <![CDATA[1<=n<=24]]>, n is treated as 24. If <![CDATA[25<=n<=53]]>, n is treated as 53.
        /// </remarks>
        /// <param name="nbBits">
        /// n is the number of bits that are used to store the mantissa of the float number
        /// in scientific notation and, therefore, dictates the precision and storage size
        /// </param>
        public SQLFloat(ushort nbBits)
        {
            NbBits = nbBits;
        }

        /// <summary>
        /// Creates a float with the default value of n (53).
        /// </summary>
        public SQLFloat()
            : this(MAX_NBBITS)
        {
        }

        /// <summary>
        /// SQL float number of bits.
        /// </summary>
        public ushort NbBits
        {
            get;
            private set;
        }

        public virtual void Write(BinaryWriter writer, object value)
        {
            if (value is float?)
            {
                Write(writer, (float?)value, NbBits);
            }
            else
            {
                //This fix a bug:
                //SQLFloat sql = new SQLFloat(SQLFloat.MAX_FLOAT_NBBITS);
                //float? valueFloat = null;
                //sql.Writer(writer, valueFloat);
                //Is this case one might think value is of type float?
                //It is not, C# confuses float? and double? when the value == null
                //Thus we have to check NbBits and determine if value is float? or double?
                double? valueDouble = (double?)value;
                if (!valueDouble.HasValue && NbBits <= MAX_FLOAT_NBBITS)
                {
                    Write(writer, (float?)value, NbBits);
                }
                else
                {
                    Write(writer, valueDouble, NbBits);
                }
            }
        }

        public static void Write(BinaryWriter writer, double? value, ushort nbBits)
        {
            if (nbBits < MIN_DOUBLE_NBBITS || nbBits > MAX_NBBITS)
            {
                throw new ArgumentException("nbBits should be between 25-53");
            }

            if (!value.HasValue)
            {
                //1 byte long
                byte[] nullBytes = { 255 };
                writer.Write(nullBytes);
                return;
            }

            //byte is 1 byte long :)
            byte size = 4 * 2;
            writer.Write(size);

            //double is 8 bytes long
            writer.Write(value.Value);
        }

        public static void Write(BinaryWriter writer, float? value, ushort nbBits)
        {
            if (nbBits < MIN_NBBITS || nbBits > MAX_FLOAT_NBBITS)
            {
                throw new ArgumentException("NbBits should be between 1-24");
            }

            if (!value.HasValue)
            {
                //1 byte long
                byte[] nullBytes = { 255 };
                writer.Write(nullBytes);
                return;
            }

            //byte is 1 byte long :)
            byte size = 4;
            writer.Write(size);

            //float is 4 bytes long
            writer.Write(value.Value);
        }
    }
}
