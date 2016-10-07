<?php



	//Get the POST variables
	$mVideoID = $_POST['VideoID'];
	
$servername = "localhost";
$username = "cotykova_COTY";
$password = "Scrapmonkey722!";
$dbname = "cotykova_church_test";

// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);
// Check connection

if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}
	
	else
	{
		//Create query to retrieve all contacts
		
		
		$query = "SELECT * FROM Video WHERE Video_ID=('".$mVideoID."')";
		
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
				$currentvideo = array("Title" => $row['Video_Title'],
								 "Author" => $row['Video_Author'],
								 "Date" => $row['Video_Date'],
								 "Image"=> $row['Video_ImagePath'],
								 "Description"=> $row['Video_Description'],
								 "Link"=> $row['Video_Link']
								 );
								 
				//Add the contact to the contacts array
			
			
			
			//Echo out the contacts array in JSON format
			echo json_encode($currentvideo);
			mysqli_close($conn);
		}
	}

?>