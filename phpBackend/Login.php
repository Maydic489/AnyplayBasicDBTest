<?php

require 'ConnectionSetting.php';

$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT password FROM users WHERE username = '" . $loginUser . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // output data of each row
    while ($row = $result->fetch_assoc()) {
        if ($row["password"] == $loginPass) {
            echo "login Success.<br>";
            UpdateLoginTimeStamp($conn, $loginUser);
        } else {
            echo "Wrong Credentials.";
        }
    }
} else {
    echo "Username doesn't exist.";
}
$conn->close();


function UpdateLoginTimeStamp($conn, $loginUser)
{
    $sql = "SELECT id FROM users WHERE username = '" . $loginUser . "'";
    $result = $conn->query($sql);
    $row = $result->fetch_assoc();
    $id = $row["id"];

    date_default_timezone_set('Asia/Bangkok');
    $dateTime = date("Y-m-d H:i:s");

    $sql = "INSERT login_history (user_id, login_time) VALUES ('$id', '$dateTime')";
    $conn->query($sql);
}
?>