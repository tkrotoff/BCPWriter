using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL xml.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187339.aspx">xml (Transact-SQL)</a><br/>
    /// <br/>
    /// From SQL Server 2008 Books Online:<br/>
    /// <br/>
    /// CONTENT<br/>
    /// Restricts the xml instance to be a well-formed XML fragment.<br/>
    /// The XML data can contain multiple zero or more elements at the top level.<br/>
    /// Text nodes are also allowed at the top level.<br/>
    /// This is the default behavior.<br/>
    /// <br/>
    /// DOCUMENT<br/>
    /// Restricts the xml instance to be a well-formed XML document.<br/>
    /// The XML data must have one and only one root element.<br/>
    /// Text nodes are not allowed at the top level.<br/>
    /// </remarks>
    public class SQLXML : SQLNVarChar
    {
        /// <summary>
        /// Constructs a SQL xml.
        /// </summary>
        public SQLXML()
            : base(SQLNVarChar.MAX)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml">XML as a string</param>
        /// <returns></returns>
        public new byte[] ToBCP(string xml)
        {
            return base.ToBCP(xml);
        }
    }
}
