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
		$query = 'SELECT * FROM Series ORDER BY Series_ID';
		
		$stmt = mysqli_query($conn, $query);
		
		if (!$stmt)
		{
			//Query failed
			echo 'Query failed';
		}
		
		else
		{
			$series = array(); //Create an array to hold all of the contacts
			//Query successful, begin putting each contact into an array of contacts
			
			while ($row = mysqli_fetch_array($stmt,MYSQLI_ASSOC)) //While there are still contacts
			{
				//Create an associative array to hold the current contact
				//the names must match exactly the property names in the contact class in our C# code.
				$currentseries = array("ID" => $row['Series_ID'],
								 "Title" => $row['Series_Title'],
								 "ImageBase64" => $row['Series_ImagePath']
								 );
								 
				//Add the contact to the contacts array
				array_push($series, $currentseries);
			}
			
			//Echo out the contacts array in JSON format
			echo json_encode($series);
		}
	}
?>
