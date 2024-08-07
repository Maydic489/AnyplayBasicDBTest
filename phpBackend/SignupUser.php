<?php

require 'DBConnectionSetting.php';
require 'UserDataManager.php';

$signUpUser = $_POST["signUpUser"];
$signUpPass = $_POST["signUpPass"];

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT username FROM users WHERE username = '" . $signUpUser . "'";

// check if username already exists
$result = $conn->query($sql);
if ($result->num_rows > 0) {
    echo "Username already exists";
} else {
    $sql = "INSERT users (username, password) VALUES ('$signUpUser', '$signUpPass')";

    if ($conn->query($sql) === TRUE) {
        echo "Signup Success";
        NewUserData($conn, $signUpUser);
        GetUserData($conn, $signUpUser);
    } else {
        echo "Error: " . $conn->error . "<br>";
    }
}

?>