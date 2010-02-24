﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL int.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
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
        int _value;

        public static readonly int MIN_VALUE = (int) Math.Pow(-2, 31);
        public static readonly int MAX_VALUE = (int) Math.Pow(2, 31) - 1;

        public SQLInt(int value)
        {
            //Can be a value from -2^31 through -2^31-1
            //FIXME useless asserts since int cannot be less/more than MIN_VALUE/MAX_VALUE
            //asserts here for the beauty of it...
            System.Diagnostics.Trace.Assert(value >= MIN_VALUE);
            System.Diagnostics.Trace.Assert(value <= MAX_VALUE);

            _value = value;
        }

        public void ToBCP(BinaryWriter writer)
        {
            //byte is 1 byte long :)
            byte size = 4;
            writer.Write(size);

            //int is 4 bytes long
            writer.Write(_value);
        }
    }
}
