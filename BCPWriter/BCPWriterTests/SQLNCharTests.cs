﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using NUnit.Framework;

using BCPWriter;

namespace BCPWriter.Tests
{
    [TestFixture]
    class SQLNCharTests
    {
        private void WriteNChar(string text, ushort length, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLNChar sqlNChar = new SQLNChar(length);
            writer.Write(sqlNChar.ToBCP(text));

            writer.Close();
        }

        [Test]
        public void TestNChar2()
        {
            ushort length = 2;

            string myFileName = string.Format("nchar({0}).bcp", length);
            WriteNChar("KI", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNChar10()
        {
            ushort length = 10;

            string myFileName = string.Format("nchar({0}).bcp", length);
            WriteNChar("KIKOO", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNChar1000()
        {
            ushort length = 1000;

            string myFileName = string.Format("nchar({0}).bcp", length);
            WriteNChar("KIKOO", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}