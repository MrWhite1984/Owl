<?php 
    session_start();
    
    $role=null;

    if(empty($_SESSION["token"]) || empty($_SESSION["selected_role"])){
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


        $api_url = "http://sibadi-conf-hub:8080/api/roles/getRole";
        $payload = json_encode([
            "id" => $_SESSION["selected_role"]
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

        }
        else{
            $role = json_decode($response);
        }

        $full_rights = ["ADMIN"];
        $moders_rights = ["ADMIN", "MODER"];
        $user_rights = ["ADMIN", "MODER", "USER"];
    }
    ?>
<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Главная</title>
    <link rel="stylesheet" href="styles/index.css">
    <!-- <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"> -->
</head>
<body>
    <?php
        if($token_is_valid){
            include 'elements/authed_header.php';
        }
        else{
            include 'elements/unauthed_header.php';
        }
    ?>
    <?php include 'elements/nav.php';?>
    <main>
        
        <div class="container">
            <?php
                if($token_is_valid){
                    $hour = (int)date('H');
                    if ($hour >= 6 && $hour < 12) {
                        $greeting = "Доброе утро";
                    } elseif ($hour >= 12 && $hour < 18) {
                        $greeting = "Добрый день";
                    } elseif ($hour >= 18 && $hour < 23) {
                        $greeting = "Добрый вечер";
                    } else {
                        $greeting = "Доброй ночи";
                    }   

                    echo "<h1>".$greeting."</h1>";
                }
                else{
                    $hour = (int)date('H');
                    if ($hour >= 6 && $hour < 12) {
                        $greeting = "Доброе утро";
                    } elseif ($hour >= 12 && $hour < 18) {
                        $greeting = "Добрый день";
                    } elseif ($hour >= 18 && $hour < 23) {
                        $greeting = "Добрый вечер";
                    } else {
                        $greeting = "Доброй ночи";
                    }   
                    echo "<h1>".$greeting."</h1>";
                }
            ?>
            <h2>
                Новости
            </h2>
            <div class="news-block"></div>
        </div>        
        
    </main>
    <?php include 'elements/footer.php';?>
    <script src="scripts/index.js"></script>
</body>
</html>