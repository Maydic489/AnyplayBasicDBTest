<?php

require 'DBConnectionSetting.php';
require 'UserDataManager.php';

$signUpUser = $_POST["signUpUser"];
$signUpPass = $_POST["signUpPass"];

$response = array();

// Check connection
if ($conn->connect_error) {
    $response["success"] = false;
    $response["message"] = "Connection failed: " . $conn->connect_error;
    echo json_encode($response);
    exit();
}

$sql = "SELECT username FROM users WHERE username = ?";

// Prepare the statement
$stmt = $conn->prepare($sql);
$stmt->bind_param("s", $signUpUser);
$stmt->execute();
$result = $stmt->get_result();

// Check if username already exists
if ($result->num_rows > 0) {
    $response["success"] = false;
    $response["message"] = "Username already exists";
} else {
    $sql = "INSERT INTO users (username, password) VALUES (?, ?)";

    // Prepare the statement
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("ss", $signUpUser, $signUpPass);

    if ($stmt->execute() === TRUE) {
        $response["success"] = true;
        $response["message"] = "Signup Success";

        // Create new user data
        NewUserData($conn, $signUpUser);

        // Get user data
        $userData = GetUserData($conn, $signUpUser);
        $response["data"] = $userData;
    } else {
        $response["success"] = false;
        $response["message"] = "Error: " . $conn->error;
    }
}

// Close the statement
$stmt->close();

// Output the response as JSON
echo json_encode($response);

?>