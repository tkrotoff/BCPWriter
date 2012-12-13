namespace BCPWriter.Tests
{
    using System.IO;

    using NUnit.Framework;

    /// <summary>
    /// Internal utility functions for unit tests.
    /// </summary>
    internal class BCPTests
    {
        public static BinaryWriter CreateBinaryFile(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.Create);
            return new BinaryWriter(stream);
        }

        public static byte[] ReadBinaryFile(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.Open);

            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                    {
                        stream.Close();
                        return ms.ToArray();
                    }
                    ms.Write(buffer, 0, read);
                }
            }
        }

        public static void CheckFile(string myFileName)
        {
            string bcpFileName = "../../bcp_tests/" + myFileName;

            byte[] myFile = BCPTests.ReadBinaryFile(myFileName);
            byte[] bcpFile = BCPTests.ReadBinaryFile(bcpFileName);

            Assert.AreEqual(bcpFile, myFile);
        }
    }
}
