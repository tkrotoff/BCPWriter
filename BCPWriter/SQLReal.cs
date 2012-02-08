using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL real.
    /// </summary>
    /// 
    /// <remarks>
    /// <see cref="SQLFloat"/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms173773.aspx">float and real (Transact-SQL)</a><br/>
    /// The ISO synonym for real is float(24).
    /// </remarks>
    public class SQLReal : SQLFloat
    {
        private const ushort REAL_NBBITS = 24;

        public SQLReal()
            : base(REAL_NBBITS)
        {
        }

        public override void Write(BinaryWriter writer, object value)
        {
            Write(writer, (float?)value);
        }

        public static void Write(BinaryWriter writer, float? value)
        {
            Write(writer, value, REAL_NBBITS);
        }
    }
}
