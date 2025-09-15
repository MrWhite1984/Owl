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
    if ($token_is_valid) {
        include 'elements/authed_header.php';
    } else {
        include 'elements/unauthed_header.php';
    }
    ?>
    <?php include 'elements/nav.php'; ?>
    <main>
        <div id="create-news-container" class="create-news-container">
            <button id="rev-btn" class="rev-btn btn">&lsaquo; Назад</button>
            <h2>Создание новости</h2>
            <form action="" method="post">
                <input type="hidden" name="active-method" class="active-method" value="create-news">
                <label for="created-news-title">Заголовок</label>
                <input type="text" name="created-news-title" class="created-news-title" required>
                <label for="created-news-data">Текст</label>
                <textarea name="created-news-data" class="created-news-data" required></textarea>
                <button type="submit" id="create-new-container-submit-btn" class="add-news-btn btn">Создать новость</button>
            </form>
        </div>
        <div id="news-container" class="news-container">
            <?php
            if ($token_is_valid) {
                $hour = (int)date('H');
                if ($hour >= 6 && $hour < 12) {
                    $greeting = "Доброе утро, " . htmlspecialchars($peopleData->surname) . " " . htmlspecialchars($peopleData->name);
                } elseif ($hour >= 12 && $hour < 18) {
                    $greeting = "Добрый день, " . htmlspecialchars($peopleData->surname) . " " . htmlspecialchars($peopleData->name);
                } elseif ($hour >= 18 && $hour < 23) {
                    $greeting = "Добрый вечер, " . htmlspecialchars($peopleData->surname) . " " . htmlspecialchars($peopleData->name);
                } else {
                    $greeting = "Доброй ночи, " . htmlspecialchars($peopleData->surname) . " " . htmlspecialchars($peopleData->name);
                }

                echo "<h1>" . $greeting . "</h1>";
            } else {
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
                echo "<h1>" . $greeting . "</h1>";
            }
            ?>
            <h2>
                Новости
            </h2>
            <?php
            if ($role != null) {
                if (in_array($role->title, $moders_rights))
                    echo "<button id=\"container-add-news-btn\" class=\"add-news-btn btn\">Создать новость</button>";
            }
            ?>
            <div class="news-block">
                <?php
                $newsList = "";

                $api_url = "http://sibadi-conf-hub:8080/api/news/getPage";

                $payload = json_encode([
                    "pageNumber" => $pageNumber,
                    "pageSize" => 10
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
                    $news = json_decode($response);
                    foreach ($news as $element) {
                        $date = new DateTime($element->dateTime);
                        $formateDate = $date->format('d.m.Y, H:i');
                        $newsList = $newsList . "<div class=\"news-item\"><div class=\"usr-card-news\">".
                        "<div class=\"usr-card-news-photo\"></div>"
                            . htmlspecialchars($element->author->surname) . " "
                            . htmlspecialchars($element->author->name) . " "
                            . ($element->author->verified ? "&#10004" : '') . "<br>" .
                            "</div><h3>" . htmlspecialchars($element->title) .
                            "</h3><br><p>" . htmlspecialchars($element->data) . "</p><br><p>" .
                            $formateDate . "</p></div>";
                    }
                    echo $newsList;
                }
                ?>
            </div>
        </div>

    </main>
    <?php include 'elements/footer.php'; ?>
    <script src="scripts/header.js"></script>
    <script src="scripts/index.js"></script>
</body>

</html>