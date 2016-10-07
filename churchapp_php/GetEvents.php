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
		$query = 'SELECT * FROM Events ORDER BY Event_Date';
		
		$stmt = mysqli_query($conn, $query);
		
		if (!$stmt)
		{
			//Query failed
			echo 'Query failed';
		}
		
		else
		{
			$events = array(); //Create an array to hold all of the contacts
			//Query successful, begin putting each contact into an array of contacts
			
			while ($row = mysqli_fetch_array($stmt,MYSQLI_ASSOC)) //While there are still contacts
			{
				//Create an associative array to hold the current contact
				//the names must match exactly the property names in the contact class in our C# code.
				$currentevent = array("ID" => $row['Event_ID'],
								 "Title" => $row['Event_Title'],
								 "Date" => $row['Event_Date']
								 );
								 
				//Add the contact to the contacts array
				array_push($events, $currentevent);
			}
			
			//Echo out the contacts array in JSON format
			echo json_encode($events);
		}
	}
?>
