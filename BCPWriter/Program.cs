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

            BCPChar bcpChar = new BCPChar("KIKOO", 10);
            bcpChar.ToBCPFormat(writer);

            writer.Close();
        }
    }
}
