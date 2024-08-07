<?php

require 'ConnectionSetting.php';

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
    } else {
        echo "Error: " . $conn->error . "<br>";
    }
}

// insert new user_data
function NewUserData($conn, $signUpUser)
{

    $sql = "SELECT id FROM users WHERE username = '" . $signUpUser . "'";
    $result = $conn->query($sql);
    $row = $result->fetch_assoc();
    $id = $row["id"];

    $sql = "INSERT user_data (user_id) VALUES ('$id')";
}
?>