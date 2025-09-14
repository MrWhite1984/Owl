<?php
    $api_url = "http://sibadi-conf-hub:8080/api/people/getPeopleLight";
        $payload = json_encode([
            "token" => $_SESSION["token"]
        ]);
        $ch = curl_init($api_url);
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
        curl_setopt($ch, CURLOPT_POST, true);
        curl_setopt($ch, CURLOPT_POSTFIELDS, $payload);
        curl_setopt($ch, CURLOPT_HTTPHEADER, [
            "Content-Type: application/json",
            "Accept: application/json"
        ]);

        curl_setopt($ch, CURLOPT_TIMEOUT, 5);
        $response = curl_exec($ch);
        $http_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
        curl_close($ch);
        if($http_code !== 200){
            if($http_code === 401){
                header('Location: /auth.php');
                exit;
            }
        }
        else{
            $peopleData = json_decode($response);
            echo "<div>$peopleData->surname $peopleData->name". ($peopleData->isVerified ? "V" : ""). "</div>";
        } 
?>