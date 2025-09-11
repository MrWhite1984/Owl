<?php
    session_start();
    if(empty($_SESSION["token"]))
        header('Location: /index.php');
    else{
        $api_url = "http://sibadi-conf-hub:8080/api/people/getRoles";
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

        }
        else{
            $roles = json_decode($response);
            if(count($roles) === 1){
                header('Location: /index.php');
                exit;
            }
        } 
    }
?>

<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <?php
        $buttons = "";
        foreach($roles as $element){
            if ($element->title == "ADMIN")
                $buttons = $buttons."<button id=\"adm-button\">Администратор</button>";
            else if ($element->title == "MODER")
                $buttons = $buttons."<button id=\"mdr-button\">Модератор</button>";
            else if ($element->title == "USER")
                $buttons = $buttons."<button id=\"usr-button\">Пользователь</button>";
        }
        echo $buttons;
    ?>
</body>
</html>