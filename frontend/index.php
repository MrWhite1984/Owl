<?php 
    session_start();
    
    $role=null;

    $full_rights = ["ADMIN"];
    $moders_rights = ["ADMIN", "MODER"];
    $user_rights = ["ADMIN", "MODER", "USER"];

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


        $api_url = "http://sibadi-conf-hub:8080/api/people/getPeopleLight";
    $payload = json_encode([
        "token" => $token
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
    }    
    ?>
<!DOCTYPE html>
<html lang="ru">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Главная</title>
    <link rel="stylesheet" href="styles/index.css">
    <link rel="stylesheet" href="styles/nav.css">
    <link rel="stylesheet" href="styles/header.css">
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
        <div id="create-news-container" class="create-news-container">
            <button id="rev-btn" class="rev-btn btn">&lsaquo; Назад</button>
            <h2>Создание новости</h2>
            <form action="" method="post">
                <input type="hidden" name="active-method" class="active-method" value="create-news">
                <label for="created-news-title">Заголовок</label>
                <input type="text" name="created-news-title" class="created-news-title">
                <label for="created-news-data">Текст</label>
                <textarea name="created-news-data" class="created-news-data"></textarea>
                <button class="add-news-btn btn">Создать статью</button>
            </form>
        </div>
        <div id="news-container" class="news-container">
            <?php
                if($token_is_valid){
                    $hour = (int)date('H');
                    if ($hour >= 6 && $hour < 12) {
                        $greeting = "Доброе утро, " . htmlspecialchars($peopleData->surname). " ". htmlspecialchars($peopleData->name);
                    } elseif ($hour >= 12 && $hour < 18) {
                        $greeting = "Добрый день, " . htmlspecialchars($peopleData->surname). " ". htmlspecialchars($peopleData->name);
                    } elseif ($hour >= 18 && $hour < 23) {
                        $greeting = "Добрый вечер, " . htmlspecialchars($peopleData->surname). " ". htmlspecialchars($peopleData->name);
                    } else {
                        $greeting = "Доброй ночи, " . htmlspecialchars($peopleData->surname). " ". htmlspecialchars($peopleData->name);
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
            <?php 
                if($role != null){
                    if(in_array($role->title, $moders_rights))
                        echo "<button id=\"container-add-news-btn\" class=\"add-news-btn btn\">Создать новость</button>";
                }
            ?>
            <div class="news-block"></div>
        </div>

    </main>
    <?php include 'elements/footer.php';?>
    <script src="scripts/header.js"></script>
    <script src="scripts/index.js"></script>
</body>

</html>