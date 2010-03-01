using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    class BCPWriter
    {
        public BCPWriter()
        {
            string myFileName = "test.bcp";

            FileStream stream = new FileStream(myFileName, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);

            SQLChar firstName = new SQLChar(10);
            SQLChar lastName = new SQLChar(10);
            SQLInt age = new SQLInt();
            SQLInt gender = new SQLInt();

            writer.Write(firstName.ToBCP("Tanguy"));
            writer.Write(lastName.ToBCP("Krotoff"));
            writer.Write(age.ToBCP(10));
            writer.Write(gender.ToBCP(1));

            writer.Write(firstName.ToBCP("Renaud"));
            writer.Write(lastName.ToBCP("Larzilliere"));
            writer.Write(age.ToBCP(15));
            writer.Write(gender.ToBCP(1));

            writer.Write(firstName.ToBCP("Nicolas"));
            writer.Write(lastName.ToBCP("Thal"));
            writer.Write(age.ToBCP(7));
            writer.Write(gender.ToBCP(1));

            writer.Close();
        }

    }
}
