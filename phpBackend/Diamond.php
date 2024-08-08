<?php

require 'DBConnectionSetting.php';
require 'UserDataManager.php';

$updateUserID = $_POST["updateUserID"];
$diamondAmout = $_POST["diamondAmout"];

// Check connection
if ($conn->connect_error) {
    $response["success"] = false;
    $response["message"] = "Connection failed: " . $conn->connect_error;
    echo json_encode($response);
    exit();
}

// get current diamonds in db
$sql = "SELECT diamonds FROM user_data WHERE user_id = '" . $updateUserID . "'";
$result = $conn->query($sql);
$row = $result->fetch_assoc();
$currentDiamonds = $row["diamonds"];

// add/deduct diamonds
$newDiamonds = $currentDiamonds + $diamondAmout;

$response = array();

UpdateUserDiamonds($conn, $updateUserID, $newDiamonds);

$response["success"] = true;
$response["message"] = "You got " . $diamondAmout . " diamonds!";
$response["diamonds"] = $newDiamonds;

$conn->close();

echo json_encode($response);

?>