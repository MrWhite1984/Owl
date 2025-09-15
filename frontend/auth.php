<?php
session_start();
if ($_POST) {
    $api_url = "http://sibadi-conf-hub:8080/api/people/login";
    $payload = json_encode([
        "email" => $_POST["email"],
        "password" => $_POST["pass"]
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
    if ($http_code !== 200) {
        if ($http_code === 401) {
            echo "<p>Ошибка логина или пароля</p>";
        }
    } else {
        $response_data = json_decode($response, true);
        $token = $response_data["sessionToken"];

        $_SESSION["token"] = $token;

        header('Location: /select_role.php');
        exit;
    }
}
?>

<!DOCTYPE html>
<html lang="ru">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Вход</title>
    <link rel="stylesheet" href="styles/auth.css">
</head>

<body>
    <main>
        <div class="top-div"></div>
        <div class="mid-div">
            <div class="mid-div-left"></div>
            <div class="mid-div-mid">
                <h2>Вход</h2>
                <form action="" method="post">
                    <div>
                        <div>
                            <label for="email">Почта</label>
                        </div>
                        <div>
                            <input type="email" name="email" id="email" required>
                        </div>
                    </div>
                    <div>
                        <div>
                            <label for="pass">Пароль</label>
                        </div>
                        <div>
                            <input type="password" name="pass" id="pass" required>
                        </div>
                    </div>
                    <button type="submit" class="btn" id="login-btn">Вход</button>
                </form>
                <div>
                    <a href="reg.php">Регистрация</a>
                </div>
                <div>
                    <a href="">Восстановить пароль</a>
                </div>
            </div>
            <div class="mid-div-right"></div>
        </div>

    </main>
</body>

</html>