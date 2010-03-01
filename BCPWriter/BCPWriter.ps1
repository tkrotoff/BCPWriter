# PowerShell script that uses BCPWriter assembly

[Reflection.Assembly]::LoadFrom("BCPWriter.dll");

$bcp = new-object BCPWriter.BCPWriter("data.bcp");

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
    "Frédéric François", "Chopin", 1810, 1849, 0,
    "Franz", "Liszt", 1811, 1886, 1,
    "George", "Sand", 1804, 1876, 1;

$bcp.WriteRows($rows);
