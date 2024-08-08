<?php

require 'DBConnectionSetting.php';
require 'UserDataManager.php';

$updateUserID = $_POST["updateUserID"];
$heartAmout = $_POST["heartAmout"];

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// get current hearts in db
$sql = "SELECT hearts FROM user_data WHERE user_id = '" . $updateUserID . "'";
$result = $conn->query($sql);
$row = $result->fetch_assoc();
$currentHearts = $row["hearts"];

// add/deduct hearts
$newHearts = $currentHearts + $heartAmout;

$response = array();

UpdateUserHearts($conn, $updateUserID, $newHearts);

$response["success"] = true;
$response["message"] = "You heart changed by " . $heartAmout . " point!";
$response["hearts"] = $newHearts;

$conn->close();

echo json_encode($response);

?>