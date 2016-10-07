<?php



	//Get the POST variables
	$mSermonID = $_POST['SermonID'];
	
$servername = "localhost";
$username = "";
$password = "";
$dbname = "";

// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);
// Check connection

if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}
	
	else
	{
		//Create query to retrieve all contacts
		
		
		$query = "SELECT * FROM Sermon WHERE Sermon_ID=('".$mSermonID."')";
		
		$stmt = mysqli_query($conn, $query);
		
		if (!$stmt)
		{
			//Query failed
			echo 'Query failed';
		}
		
		else
		{
			 //Create an array to hold all of the contacts
			//Query successful, begin putting each contact into an array of contacts
			
			$row = mysqli_fetch_array($stmt,MYSQLI_ASSOC); //While there are still contacts
			
				//Create an associative array to hold the current contact
				//the names must match exactly the property names in the contact class in our C# code.
				$currentsermon = array("Title" => $row['Sermon_Title'],
								 "Date" => $row['Sermon_Date'],
								 "Image"=> $row['Sermon_ImagePath'],
								 "Text"=> $row['Sermon_TextPath']
								 );
								 
				//Add the contact to the contacts array
				
			
			
			//Echo out the contacts array in JSON format
			echo json_encode($currentsermon);
			mysqli_close($conn);
		}
	}

?>
