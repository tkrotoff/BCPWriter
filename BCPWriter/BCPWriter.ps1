# PowerShell script that uses BCPWriter assembly and bcp

[Reflection.Assembly]::LoadFrom("BCPWriter\bin\Release\BCPWriter.dll");

$bcpFileName = "data.bcp";
$bcp = new-object BCPWriter.BCPWriter($bcpFileName);

$firstName = new-object BCPWriter.SQLNVarChar([BCPWriter.SQLNVarChar]::MAX);
$bcp.AddColumn($firstName);

$lastName = new-object BCPWriter.SQLNVarChar([BCPWriter.SQLNVarChar]::MAX);
$bcp.AddColumn($lastName);

$birth = new-object BCPWriter.SQLInt;
$bcp.AddColumn($birth);

$death = new-object BCPWriter.SQLInt;
$bcp.AddColumn($death);

$gender = new-object BCPWriter.SQLInt;
$bcp.AddColumn($gender);

$rows =
    "Frédéric François", "Chopin", 1810, 1849, 1,
    "Franz", "Liszt", 1811, 1886, 1,
    "George", "Sand", 1804, 1876, 0;

$bcp.WriteRows($rows);


$table = "[BCPTest].[dbo].[BCPTest]";
$servername = "localhost";
$username = "sa";
$password = "Password01";

# Calls bcp and load the data inside the table
bcp $table in $bcpFileName -n -S $servername -U $username -P $password
