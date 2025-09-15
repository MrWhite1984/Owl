<?php
session_start();

if ($_POST) {
    $_SESSION["selected_role"] = $_POST["role-select"];
    header('Location: /index.php');
    exit;
}

if (empty($_SESSION["token"]))
    header('Location: /index.php');
else {
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
    if ($http_code !== 200) {
        if ($http_code === 401) {
            header('Location: /auth.php');
            exit;
        }
    } else {
        $roles = json_decode($response);
        if (count($roles) === 1) {
            $_SESSION["selected_role"] = $roles[0]->id;
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
    <title>Выберите роль</title>
</head>

<body>
    <form action="" method="post">
        <label for="role-select">Выберите роль:</label>
        <select name="role-select" id="role-select">
            <option value="" disabled selected>— Выберите роль —</option>
            <?php
            foreach ($roles as $element) {
                if ($element->title === "ADMIN")
                    echo "<option value=\"$element->id\">Администратор</option>";
                else if ($element->title === "MODER")
                    echo "<option value=\"$element->id\">Модератор</option>";
                else if ($element->title === "USER")
                    echo "<option value=\"$element->id\">Пользователь</option>";
            }
            ?>
        </select>
        <button type="submit">Войти</button>
    </form>

    <script src="scripts/select_role.js"></script>
</body>

</html>