<?php
function NewUserData($conn, $signUpUser)
{
    $sql = "SELECT id FROM users WHERE username = '" . $signUpUser . "'";
    $result = $conn->query($sql);
    $row = $result->fetch_assoc();
    $id = $row["id"];

    $sql = "INSERT user_data (user_id) VALUES ('$id')";

    if ($conn->query($sql) === TRUE) {
    } else {
        echo "Error: " . $conn->error . "<br>";
    }
}



?>