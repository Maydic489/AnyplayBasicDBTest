<?php
function UpdateLoginTimeStamp($conn, $loginUser)
{
    $sql = "SELECT id FROM users WHERE username = '" . $loginUser . "'";
    $result = $conn->query($sql);
    $row = $result->fetch_assoc();
    $id = $row["id"];

    date_default_timezone_set('Asia/Bangkok');
    $dateTime = date("Y-m-d H:i:s");

    $sql = "INSERT INTO login_history (user_id, login_time) VALUES ('$id', '$dateTime')";
    $conn->query($sql);
}
?>