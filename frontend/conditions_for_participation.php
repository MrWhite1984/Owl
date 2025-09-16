<?php
session_start();

$role = null;
$news = null;
$token = null;
$peopleData = null;
$pageNumber = 0;

$full_rights = ["ADMIN"];
$moders_rights = ["ADMIN", "MODER"];
$user_rights = ["ADMIN", "MODER", "USER"];

if (empty($_SESSION["token"]) || empty($_SESSION["selected_role"])) {
    $token_is_valid = false;
} else {
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
if ($token_is_valid) {
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


    curl_setopt($ch, CURLOPT_TIMEOUT, 5);
    $response = curl_exec($ch);
    $http_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
    curl_close($ch);
    if ($http_code !== 200) {
    } else {
        $peopleData = json_decode($response);
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
    if ($http_code !== 200) {
    } else {
        $role = json_decode($response);
    }
}

if (!empty($_POST["active-method"])) {
    if ($_POST["active-method"]) {
        $api_url = "http://sibadi-conf-hub:8080/api/news/create";

        $payload = json_encode([
            "title" => $_POST["created-news-title"],
            "data" => $_POST["created-news-data"],
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

        curl_setopt($ch, CURLOPT_TIMEOUT, 5);
        $response = curl_exec($ch);
        $http_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
        curl_close($ch);
        if ($http_code !== 200) {
        } else {
            header('Location: /index.php');
            exit;
        }
    }
}

if (!empty($_POST["function"])) {
    if ($_POST["function"] == "save-updates") {
        $newData = preg_split('/\r\n|\r|\n/', $_POST["container-data"]);
        file_put_contents('text_data/conditions_for_participation.json', json_encode($newData, JSON_UNESCAPED_UNICODE | JSON_PRETTY_PRINT));
    }
}
?>

<!DOCTYPE html>
<html lang="ru">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Условия участия</title>
    <link rel="stylesheet" href="styles/conditions_for_participation.css">
    <link rel="stylesheet" href="styles/nav.css">
    <link rel="stylesheet" href="styles/header.css">
    <link rel="stylesheet" href="styles/footer.css">
</head>

<body>
    <?php
    if ($token_is_valid) {
        include 'elements/authed_header.php';
    } else {
        include 'elements/unauthed_header.php';
    }
    ?>
    <?php include 'elements/nav.php'; ?>
    <main>
        <div id="update-container" class="update-container">
            <button id="rev-btn" class="rev-btn btn">&lsaquo; Назад</button>
            <h2>Редактивароние условий участия</h2>
            <form action="" method="post">
                <input type="hidden" name="function" value="save-updates">
                <div>
                    <label for="container-data">Текст</label>
                </div>
                <div>
                    <textarea name="container-data" class="container-data" required><?php
                                                                                    if (!file_exists('text_data/conditions_for_participation.json')) {
                                                                                        $defaultData = [];
                                                                                        file_put_contents('text_data/conditions_for_participation.json', json_encode($defaultData, JSON_UNESCAPED_UNICODE | JSON_PRETTY_PRINT));
                                                                                    }
                                                                                    $fileData = file_get_contents('text_data/conditions_for_participation.json');
                                                                                    $data = json_decode($fileData, true);
                                                                                    foreach ($data as $item) {
                                                                                        echo $item . "\n";
                                                                                    }
                                                                                    ?></textarea>
                </div>
                <button type="submit" id="update-container-data-btn" class="update-container-data-btn btn">Изменить</button>
            </form>
        </div>
        <div id="container" class="container">
            <h2>Условия участия</h2>
            <?php
            if ($role != null) {
                if (in_array($role->title, $full_rights))
                    echo "<button id=\"change-inform-btn\" class=\"change-inform-btn btn\">Изменить текст</button>";
            }
            ?>
            <div>
                <?php
                if (!file_exists('text_data/conditions_for_participation.json')) {
                    $defaultData = [];
                    file_put_contents('text_data/conditions_for_participation.json', json_encode($defaultData, JSON_UNESCAPED_UNICODE | JSON_PRETTY_PRINT));
                }
                $fileData = file_get_contents('text_data/conditions_for_participation.json');
                $data = json_decode($fileData, true);
                foreach ($data as $item): ?>
                    <p><?php echo htmlspecialchars($item); ?></p>
                <?php endforeach;
                ?>
            </div>
        </div>
    </main>
    <?php include 'elements/footer.php'; ?>

    <script src="scripts/header.js"></script>
    <script src="scripts/conditions_for_participation.js"></script>
</body>

</html>