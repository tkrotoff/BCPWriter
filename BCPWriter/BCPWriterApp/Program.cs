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
            FileStream stream = new FileStream("BCPWriterApp.bcp", FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Close();
        }
    }
}
