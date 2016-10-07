<?php



	//Get the POST variables
	$mSeriesID = $_POST['SeriesID'];
	
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
		
		
		$query = "SELECT * FROM Sermon WHERE Series_ID=('".$mSeriesID."') ORDER BY Sermon_Date";
		
		$stmt = mysqli_query($conn, $query);
		
		if (!$stmt)
		{
			//Query failed
			echo 'Query failed';
		}
		
		else
		{
			$sermons = array(); //Create an array to hold all of the contacts
			//Query successful, begin putting each contact into an array of contacts
			
			while ($row = mysqli_fetch_array($stmt,MYSQLI_ASSOC)) //While there are still contacts
			{
				//Create an associative array to hold the current contact
				//the names must match exactly the property names in the contact class in our C# code.
				$currentsermon = array("ID" => $row['Sermon_ID'],
								 "Title" => $row['Sermon_Title'],
								 "Image"=> $row['Sermon_ImagePath']
								 );
								 
				//Add the contact to the contacts array
				array_push($sermons, $currentsermon);
			}
			
			//Echo out the contacts array in JSON format
			echo json_encode($sermons);
		}
	}

?>