<?php 
    session_start();

    if(empty($_SESSION["token"])){
        $token_is_valid = false;
    }
    else{
        $token = $_SESSION["token"];
        $api_url = "http://sibadi-conf-hub:8080/api/token/validate";

        $payload = json_encode([
                "token" => $token
            ]);

        $ch = curl_init($api_url);
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
        curl_setopt($ch, CURLOPT_POST, true);
        curl_setopt($ch, CURLOPT_POSTFIELDS, $payload);
        curl_setopt($ch, CURLOPT_HTTPHEADER, [
            "Content-Type: application/json",
            "Content-Length: " . strlen($payload)
        ]);

        curl_setopt($ch, CURLOPT_TIMEOUT, 5);
        $response = curl_exec($ch);
        $http_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
        curl_close($ch);

        if ($http_code === 200) {
            $token_is_valid = true;
        } else {
            unset($_SESSION["token"]);
            $token_is_valid = false;
        }
    }
    ?>
<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Главная</title>
</head>
<body>
    <?php
        if($token_is_valid){

        }
        else{
            include 'elements/unauthed_header.php';
        }
    ?>
</body>
</html>