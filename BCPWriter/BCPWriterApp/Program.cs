using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using BCPWriter;

namespace BCPWriterWriterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream stream = new FileStream(@"D:\krotoff\BCPWriter\BCPCharTest", FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);

            /*SQLChar sqlChar = new SQLChar("KIKOO", 10);
            sqlChar.ToBCP(writer);

            SQLInt sqlInt = new SQLInt(10);
            sqlInt.ToBCP(writer);

            sqlChar = new SQLChar("LOL", 5);
            sqlChar.ToBCP(writer);*/

            SQLNChar sqlNChar = new SQLNChar("KIKOO", 10);
            sqlNChar.ToBCP(writer);

            writer.Close();
        }
    }
}
