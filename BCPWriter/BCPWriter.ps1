# PowerShell script that uses BCPWriter assembly and bcp

[Reflection.Assembly]::LoadFrom("BCPWriter\bin\Release\BCPWriter.dll");

$bcpFileName = "data.bcp";
$bcp = new-object BCPWriter.BCPWriterStatic($bcpFileName);

$bcp.WriteNVarChar("Frédéric François", [BCPWriter.SQLNVarChar]::MAX);
$bcp.WriteNVarChar("Chopin", [BCPWriter.SQLNVarChar]::MAX);
$bcp.WriteInt(1810);
$bcp.WriteInt(1849);

$bcp.WriteNVarChar("Franz Liszt", [BCPWriter.SQLNVarChar]::MAX);
$bcp.WriteNVarChar("Chopin", [BCPWriter.SQLNVarChar]::MAX);
$bcp.WriteInt(1811);
$bcp.WriteInt(1886);

$bcp.WriteNVarChar("George", [BCPWriter.SQLNVarChar]::MAX);
$bcp.WriteNVarChar("Sand", [BCPWriter.SQLNVarChar]::MAX);
$bcp.WriteInt(1804);
$bcp.WriteInt(1876);

$bcp.Close();

#$table = "[BCPTest].[dbo].[BCPTest]";
#$servername = "localhost";
#$username = "sa";
#$password = "Password01";

# Calls bcp and loads the data inside the table
#bcp $table in $bcpFileName -n -S $servername -U $username -P $password
