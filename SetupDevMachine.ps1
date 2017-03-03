cls

$localDBInstance = "MSSQLLocalDB231"
$dbname = "CodeCamp"

function ExecuteQuery {
    Param ($server, $query, $database, $read = $true)
    $conn = new-object ('System.Data.SqlClient.SqlConnection')
    $connString = "Server=$server;Integrated Security=SSPI;Database=$database"
    $conn.ConnectionString = $connString
    Write-Host $conn.ConnectionString
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
    ExecuteQuery $server "create database $dbname" "master" $false
}

function CreateTables {
#    $path ="CC.Data.Database\dbo\Tables\"
    $path ="CC.Data.Database\"
#    $files = "EventAttendees.sql","EventAttendeeRatings.sql","EventAttendeeSessionRatings.sql","Tasks.sql","PersonTasks.sql","SessionAttendees.sql","Sponsors.sql"
    $files = "create.sql"
    foreach ($file in $files) {
        Write-Host "Executing $file"
        $sql = Get-Content "$path$file"
        ExecuteQuery $server $sql $dbname $false
    }
}

function Seed {
    $path ="CC.Data.Database\"
    $file = "seed.sql"
    Write-Host "Executing $file"
    $sql = Get-Content "$path$file"
    ExecuteQuery $server $sql $dbname $false

<#    $sql = "Insert Into Events(Name, StartTime, EndTime, Address1, Address2, City, State, Zip, IsDefault, IsSponsorRegistrationOpen, IsSpeakerRegistrationOpen, IsAttendeeRegistrationOpen, IsVolunteerRegistrationOpen) Values ('Seattle Code Camp 2011', '03/21/2011', '03/21/2011', 'Seminole State College', '100 College Dr', 'Sanford', 'FL', '32746', 1, 0, 0, 0, 0)"
    ExecuteQuery $server $sql $dbname $false
#>
}

function GetLocalInstance {
    $command = "SQLLocalDB i `"$($localDBInstance)`""
    $info = Invoke-Expression $command 
    $info
}

function CreateLocalInstance {
    $command = "SQLLocalDB create `"$($localDBInstance)`""
    $info = Invoke-Expression $command
    $info
}

function StartLocalInstance {
    $command = "SQLLocalDB start `"$($localDBInstance)`""
    $info = Invoke-Expression $command
    $info
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
    Write-Host "Creating $dbname"
    CreateDb

    Write-Host "Creating tables that do not need to be seeded."
    CreateTables

    Write-Host "Seeding $dbname with a default sample values and a default Event because it is necessary for site to run."
#    Seed

}
