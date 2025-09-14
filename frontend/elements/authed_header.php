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

    $peopleData = null;

    curl_setopt($ch, CURLOPT_TIMEOUT, 5);
    $response = curl_exec($ch);
    $http_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
    curl_close($ch);
    if($http_code !== 200){
    }
    else{
        $peopleData = json_decode($response);
    } 

    ob_start();
    include __DIR__ . '/usr_elements/usr_light_card.php';
    $cardContent = ob_get_clean();
?>

<header class="row">
    <div class="nav-btn-div col-1"><button id="nav-btn">&#9776;</button></div>
    <div class="space-div col-7"></div>
    <div class="notify-btn-block"><button class="ntfy-btn"><span class="ntfy-bell">&#128276;</span></button></div>
    <?= $cardContent?>    
</header>