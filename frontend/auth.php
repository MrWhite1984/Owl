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
</head>

<body>
    <main>
        <h2>Вход</h2>
        <form action="" method="post">
            <label for="email">Почта</label>
            <input type="email" name="email" id="email" required>
            <label for="pass">Пароль</label>
            <input type="password" name="pass" id="pass" required>
            <button type="submit" id="login-btn">Вход</button>
        </form>
        <a href="reg.php">Регистрация</a>
        <a href="">Восстановить пароль</a>
    </main>
</body>

</html>