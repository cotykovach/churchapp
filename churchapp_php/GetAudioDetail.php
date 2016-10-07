<?php



	//Get the POST variables
	$mAudioID = $_POST['AudioID'];
	
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
		
		
		$query = "SELECT * FROM Audio WHERE Audio_ID=('".$mAudioID."')";
		
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
				$currentaudio = array("Title" => $row['Audio_Title'],
								 "Author" => $row['Audio_Author'],
								 "Date" => $row['Audio_Date'],
								 "Image"=> $row['Audio_ImagePath'],
								 "Description"=> $row['Audio_Description'],
								 "Link"=> $row['Audio_Link']
								 );
								 
				//Add the contact to the contacts array
				
			
			
			//Echo out the contacts array in JSON format
			echo json_encode($currentaudio);
			mysqli_close($conn);
		}
	}

?>
