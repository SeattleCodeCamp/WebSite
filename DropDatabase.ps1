<#
This script checks if the CodeCamp database exists, and if so, drops it.
This is mostly for cleanup if you need to remove the database.
#>

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

if ($dbExists) {
	Write-Host "Dropping '$dbname' database"
	DropDb $conn
    Write-Host "'$dbname' database dropped"
}

Write-Host "Closing connection to 'master'"
$conn.close()
