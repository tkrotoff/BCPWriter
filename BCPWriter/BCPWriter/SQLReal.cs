﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL real.
    /// </summary>
    /// 
    /// <remarks>
    /// <see>SQLFloat</see>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms173773.aspx">float and real (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLReal : SQLFloat
    {
        public SQLReal()
            : base(24)
        {
        }

        public new byte[] ToBCP(float value)
        {
            return base.ToBCP(value);
        }
    }
}