<#
This script creates a LocalDB instance and a CodeCamp database. It also seeds the database with sample values. 
There must be a single default Event in the event table in order for app to run.
If the LocalDB already exists then it does not try to create it. 
If the CodeCamp database already exist then it does not try to create it or seed it. It is up to you to ensure it contains the necessary values.
If the '-recreate' command line argument is provided, it will drop the existing database if it exists and then create a fresh one from the setup scripts.
#>

Param(
	[switch]$recreate
)

cls

$localDBInstance = "MSSQLLocalDB"
$dbname = "CodeCamp"

function OpenConnection {
    Param ($server, $database)
	
	Write-Host "Opening connection to '$database'"
    $conn = new-object ('System.Data.SqlClient.SqlConnection')
    $connString = "Server=$server;Integrated Security=SSPI;Database=$database"
    $conn.ConnectionString = $connString
    $conn.open()
	
	return $conn
}

function ExecuteScalar {
    Param ($conn, $query)
	
    $sqlCmd = New-Object System.Data.SqlClient.SqlCommand
    $sqlCmd.CommandText = $query
    $sqlCmd.Connection = $conn
	
	return $sqlCmd.ExecuteScalar()
}

function ExecuteNonQuery {
    Param ($conn, $query)

    $sqlCmd = New-Object System.Data.SqlClient.SqlCommand
    $sqlCmd.CommandText = "SET NOCOUNT ON; $query"
    $sqlCmd.Connection = $conn

	# Just setting the result here but not using it.
	# This is to just prevent '-1' from being printed out
	# each time this is called.
	$result = $sqlCmd.ExecuteNonQuery()
}

function DbExists{
	Param ($conn)

	try
	{
		$exists = ExecuteScalar $conn "SELECT TOP 1 * FROM master.dbo.sysdatabases WHERE name = '$dbname'"
		
		# did we get Null?  If so, the database doesn't exist.
		if($exists)
		{
			return $true
		}
		else
		{
			return $false
		}
	}
	catch 
	{
		Write-Host "Failed checking for database. Exiting..."
		$conn.Dispose()
		exit
    }	
}

function CreateDb {
	Param ($conn)
	
	try 
	{
		ExecuteNonQuery $conn "create database $dbname"
	}
	catch 
	{
		Write-Host "Failed to create database. Exiting..."
		$conn.Dispose()
		exit
    }
}

function DropDb {
	Param ($conn)
	
	try 
	{
		ExecuteNonQuery $conn "alter database $dbname set single_user with rollback immediate; drop database $dbname"
	}
	catch
	{ 
		Write-Host "Failed to drop database. Exiting..."
		$conn.Dispose()
		exit
    }
}

function RunSetupScript {
    Param ($conn, $file, $errorMessage)
	
	try 
	{
		$path = "CC.Data.Database\SetupScripts\"
		Write-Host "Executing $file"
		$sql = Get-Content "$path$file"
		ExecuteNonQuery $conn $sql
	}
	catch
	{
        Write-Host "$errorMessage Exiting..."
		$conn.Dispose()
		exit
	}
}

function CreateTables {
	RunSetupScript $conn "create.sql" "Failed to create tables without constraints."
}

function AddConstraints {
	RunSetupScript $conn "AddConstraints.sql" "Failed to add constraints."
}

function Seed {
	RunSetupScript $conn "seed.sql" "Failed to seed the database."
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

# Open a connection to the master database to see if the CodeCamp database exists.
$conn = OpenConnection $server "master"
$dbExists = DbExists $conn

Write-Host "Database Exists: $dbExists"
Write-Host "Recreate: $recreate"

if ($dbExists -and $recreate) {
	Write-Host "Dropping '$dbname' database"
	DropDb $conn
    Write-Host "'$dbname' database dropped"
}

if (-not $dbExists -or $recreate) {
	# it doesn't exist, so create it, then change connection to 
	# to CodeCamp database and start populating it.

    Write-Host "Creating '$dbname' database"
    CreateDb $conn
    Write-Host "'$dbname' database created"
	
	# Switch connections
	Write-Host "Closing connection to 'master'"
	$conn.close()
	$conn = OpenConnection $server $dbname

    Write-Host "Creating tables that do not need to be seeded."
    CreateTables $conn
    Write-Host "Tables not needing to be seeded created."

    Write-Host "Seeding '$dbname' with a default sample values and a default Event because it is necessary for site to run."
    Seed $conn
    Write-Host "Seeding compelete."

    Write-Host "Adding constraints to '$dbname'."
    AddConstraints $conn
    Write-Host "Constraints added."
	
	# we're done, so dispose of our connection.
	Write-Host "Closing connection to '$dbname'"
	$conn.close()
}
else
{
	# If we are here, then the database exists, and 
	# we aren't recreating it.  At this point,
	# we still have a connection to 'master', so close it.
	Write-Host "Closing connection to 'master'"
	$conn.close()
}
