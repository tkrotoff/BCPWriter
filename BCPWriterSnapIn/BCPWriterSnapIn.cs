namespace BCPWriter
{
    using System.ComponentModel;
    using System.Management.Automation;

    /// <summary>
    /// Creates a PowerShell snap-in for BCPWriterSimple.
    /// </summary>
    ///
    /// <remarks>
    /// How to install the snap-in:<br/>
    /// Run this command inside a command prompt:
    /// <code>C:\Windows\Microsoft.NET\Framework64\v2.0.50727\InstallUtil.exe BCPWriterSnapIn.dll</code>
    /// <br/>
    /// How to use the snap-in:
    /// Open PowerShell and run:
    /// <code>
    /// # Install BCPWriterSnapIn inside current PowerShell session
    /// Add-PSSnapin -Name BCPWriterSnapIn
    ///
    /// # Gets all PowerShell commands that contains the word "bcp"
    /// Get-Command *bcp* -CommandType cmdlet
    ///
    /// # Let's instanciate our object BCPWriterSimple
    /// $bcp = Get-BCPWriter -BCPFileName "test.bcp"
    ///
    /// # Now we can use BCPWriterSimple API
    /// $bcp.WriteNVarChar("Frédéric François", [BCPWriter.SQLNVarChar]::MAX);
    /// $bcp.WriteNVarChar("Chopin", [BCPWriter.SQLNVarChar]::MAX);
    /// $bcp.WriteInt(1810);
    /// $bcp.WriteInt(1849);
    ///
    /// $bcp.WriteNVarChar("Franz", [BCPWriter.SQLNVarChar]::MAX);
    /// $bcp.WriteNVarChar("Liszt", [BCPWriter.SQLNVarChar]::MAX);
    /// $bcp.WriteInt(1811);
    /// $bcp.WriteInt(1886);
    ///
    /// $bcp.WriteNVarChar("George", [BCPWriter.SQLNVarChar]::MAX);
    /// $bcp.WriteNVarChar("Sand", [BCPWriter.SQLNVarChar]::MAX);
    /// $bcp.WriteInt(1804);
    /// $bcp.WriteInt(1876);
    ///
    /// $bcp.Close();
    /// </code>
    /// </remarks>
    [RunInstaller(true)]
    public class BCPWriterSnapIn : PSSnapIn
    {
        public override string Description
        {
            get { return "BCPWriter SnapIn"; }
        }

        public override string Name
        {
            get { return "BCPWriterSnapIn"; }
        }

        public override string Vendor
        {
            get { return "BCPWriter"; }
        }

        [Cmdlet(VerbsCommon.Get, "BCPWriter")]
        public class GetBCPWriter : Cmdlet
        {
            [Parameter()]
            public string BCPFileName
            {
                get;
                set;
            }

            protected override void ProcessRecord()
            {
                WriteObject(new BCPWriterSimple(BCPFileName));
            }
        }
    }
}
