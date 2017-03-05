<#
This script creates a LocalDB instance and a CodeCamp database. It also seeds the database with sample values. 
There must be a single default Event in the event table in order for app to run.
If the LocalDB already exists then it does not try to create it. 
If the CodeCamp database already exist then it does not try to create it or seed it. It is up to you to ensure it contains the necessary values.
#>
cls

$localDBInstance = "MSSQLLocalDB"
$dbname = "CodeCamp"

function ExecuteQuery {
    Param ($server, $query, $database, $read = $true)
    $conn = new-object ('System.Data.SqlClient.SqlConnection')
    $connString = "Server=$server;Integrated Security=SSPI;Database=$database"
    $conn.ConnectionString = $connString
    $conn.Open()
    $sqlCmd = New-Object System.Data.SqlClient.SqlCommand
    $sqlCmd.CommandText = $query
    $sqlCmd.Connection = $conn
    if ($read) {
        $Rset = $sqlCmd.ExecuteReader()
    }
    else {
        $Rset = $sqlCmd.ExecuteNonQuery()
    }
    ,$Rset ## The comma is used to create an outer array, which PS strips off automatically when returning the $Rset
}

function QuerySQL {
    Param ($server, $query, $database = "master")
    $data = ExecuteQuery $server $query $database
    while ($data.read() -eq $true) {
        $max = $data.FieldCount -1
        $obj = New-Object Object
        For ($i = 0; $i -le $max; $i++) {
            $name = $data.GetName($i)
            $obj | Add-Member Noteproperty $name -value $data.GetValue($i)
        }

     $obj
    }
}

function CreateDb {
    $result = ExecuteQuery $server "create database $dbname" "master" $false

    if($result -ne -1){
        Write-Host "Failed to create database. Exiting..."
        exit
    }
}

function CreateTables {
    $path ="CC.Data.Database\"
    $file = "create.sql"
    Write-Host "Executing $file"
    $sql = Get-Content "$path$file"
    $result = ExecuteQuery $server $sql $dbname $false

    if ($result -ne -1){
        Write-Host "Failed to create tables without constraints. Exiting..."
        exit
    }
}

function AddConstraints {
    $path ="CC.Data.Database\"
    $file = "AddConstraints.sql"
    Write-Host "Executing $file"
    $sql = Get-Content "$path$file"
    $result = ExecuteQuery $server $sql $dbname $false

    if ($result -ne -1){
        Write-Host "Failed to add constraints. Exiting..."
        exit
    }
}

function Seed {
    $path ="CC.Data.Database\"
    $file = "seed.sql"
    Write-Host "Executing $file"
    $sql = Get-Content "$path$file"
    $result = ExecuteQuery $server $sql $dbname $false

    if ($result -lt 1){
        Write-Host "Failed to seed the database. Exiting..."
        exit
    }
}

function GetLocalInstance {
    $command = "SQLLocalDB i `"$($localDBInstance)`""
    $info = Invoke-Expression $command 
    return $info
}

function CreateLocalInstance {
    $command = "SQLLocalDB create `"$($localDBInstance)`""
    $info = Invoke-Expression $command
    return $info
}

function StartLocalInstance {
    $command = "SQLLocalDB start `"$($localDBInstance)`""
    $info = Invoke-Expression $command
    return $info
}

$instanceInfo = GetLocalInstance

if($instanceInfo.Get(0) -like "*failed*"){
    Write-Host "Could not find $localDBInstance."
    Write-Host "Creating $localDBInstance"

    CreateLocalInstance
}

StartLocalInstance

$pair = GetLocalInstance | where {$_ -like '*pipe*'} 
$len = $pair.Length
$server = $pair.Substring($pair.IndexOf('np:\\.\pipe\LOCALDB'), $len - $pair.IndexOf('np:\\.\pipe\LOCALDB'))
$dbExists = $false

QuerySQL $server "SELECT * FROM master.dbo.sysdatabases" | ForEach-Object {
  if ($_.name -eq $dbname){
        Write-Host "Found $dbname database."
        $dbExists = $true
        break;
     }
  }

if (-not $dbExists) {
    Write-Host "Creating $dbname database"
    CreateDb
    Write-Host "$dbname database created"

    Write-Host "Creating tables that do not need to be seeded."
    CreateTables
    Write-Host "Tables not needing to be seeded created."

    Write-Host "Seeding $dbname with a default sample values and a default Event because it is necessary for site to run."
    Seed
    Write-Host "Seeding compelete."

    Write-Host "Adding constraints to $dbname."
    AddConstraints
    Write-Host "Constraints added."
}
