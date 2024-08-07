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

function GetUserData($conn, $loginUser)
{
    $sql = "
        SELECT users.*, user_data.*
        FROM users
        JOIN user_data ON users.id = user_data.user_id
        WHERE users.username = ?
    ";

    // Prepare the statement
    $stmt = $conn->prepare($sql);
    if ($stmt === false) {
        echo "Error preparing statement: " . $conn->error;
        return;
    }

    // Bind parameters
    $stmt->bind_param("s", $loginUser);
    $stmt->execute();
    $result = $stmt->get_result();

    // Fetch data
    $userData = $result->fetch_assoc();

    // Check if data is found
    if ($userData) {
        //$json = json_encode($userData);
        //echo $json;
        return $userData;
    } else {
        return ["error" => "User not found"];
    }
}

function UpdateUserDiamonds($conn, $user_id, $diamonds)
{
    $sql = "
        UPDATE user_data
        SET diamonds = ?
        WHERE user_id = ?
    ";

    // Prepare the statement
    $stmt = $conn->prepare($sql);
    if ($stmt === false) {
        echo "Error preparing statement: " . $conn->error;
        return;
    }

    // Bind parameters
    $stmt->bind_param("ii", $diamonds, $user_id);

    // Execute the statement
    if ($stmt->execute() === false) {
        echo "Error executing statement: " . $stmt->error;
        return;
    }

    // Close the statement
    $stmt->close();
}

?>