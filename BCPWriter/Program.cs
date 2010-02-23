using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream stream = new FileStream(@"D:\krotoff\BCPWriter\BCPCharTest", FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);

            SQLChar bcpChar = new SQLChar("KIKOO", 10);
            bcpChar.ToBCP(writer);

            writer.Close();
        }
    }
}
