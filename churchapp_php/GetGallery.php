<?php
	
$servername = "localhost";
$username = "cotykova_COTY";
$password = "Scrapmonkey722!";
$dbname = "cotykova_church_test";
	
	$conn = mysqli_connect($servername, $username, $password, $dbname);
	
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}
	
	else
	{
		//Create query to retrieve all contacts
		$query = 'SELECT * FROM Gallery ORDER BY Gallery_Date';
		
		$stmt = mysqli_query($conn, $query);
		
		if (!$stmt)
		{
			//Query failed
			echo 'Query failed';
		}
		
		else
		{
			$gallery = array(); //Create an array to hold all of the contacts
			//Query successful, begin putting each contact into an array of contacts
			
			while ($row = mysqli_fetch_array($stmt,MYSQLI_ASSOC)) //While there are still contacts
			{
				//Create an associative array to hold the current contact
				//the names must match exactly the property names in the contact class in our C# code.
				$currentGallery = array("ID" => $row['Gallery_ID'],
								 "Title" => $row['Gallery_Title']
								 );
								 
				//Add the contact to the contacts array
				array_push($gallery, $currentGallery);
			}
			
			//Echo out the contacts array in JSON format
			echo json_encode($gallery);
		}
	}
?>