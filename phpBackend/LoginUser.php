<?php

require 'DBConnectionSetting.php';
require 'LoginTimeStamp.php';
require 'UserDataManager.php';

$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Check connection
if ($conn->connect_error) {
    $response["success"] = false;
    $response["message"] = "Connection failed: " . $conn->connect_error;
    echo json_encode($response);
    exit();
}

$sql = "SELECT password FROM users WHERE username = '" . $loginUser . "'";

$result = $conn->query($sql);

$response = array();

if ($result->num_rows > 0) {
    // output data of each row
    while ($row = $result->fetch_assoc()) {
        if ($row["password"] == $loginPass) {
            UpdateLoginTimeStamp($conn, $loginUser);
            $userData = GetUserData($conn, $loginUser);
            $response["success"] = true;
            $response["message"] = "login Success.";
            $response["data"] = $userData;
        } else {
            $response["success"] = false;
            $response["message"] = "Wrong Credentials.";
        }
    }
} else {
    $response["success"] = false;
    $response["message"] = "Username doesn't exist.";
}
$conn->close();

echo json_encode($response);

?>