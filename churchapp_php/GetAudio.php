<?php
	
$servername = "localhost";
$username = "";
$password = "";
$dbname = "";
	
	$conn = mysqli_connect($servername, $username, $password, $dbname);
	
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}
	
	else
	{
		//Create query to retrieve all contacts
		$query = 'SELECT * FROM Audio ORDER BY Audio_Date DESC';
		
		$stmt = mysqli_query($conn, $query);
		
		if (!$stmt)
		{
			//Query failed
			echo 'Query failed';
		}
		
		else
		{
			$audio = array(); //Create an array to hold all of the contacts
			//Query successful, begin putting each contact into an array of contacts
			
			while ($row = mysqli_fetch_array($stmt,MYSQLI_ASSOC)) //While there are still contacts
			{
				//Create an associative array to hold the current contact
				//the names must match exactly the property names in the contact class in our C# code.
				$currentAudio = array("ID" => $row['Audio_ID'],
								 "Title" => $row['Audio_Title']
								 );
								 
				//Add the contact to the contacts array
				array_push($audio, $currentAudio);
			}
			
			//Echo out the contacts array in JSON format
			echo json_encode($audio);
		}
	}
?>
