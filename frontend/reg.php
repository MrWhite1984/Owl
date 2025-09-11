<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <main>
        <h2>Регистрация</h2>
        <h4>Поля, отмеченные знаком <span class="rq">*</span>, являются обязательными</h4>
        <form action="" method="post">
            <label for="surname">Фамилия<span class="rq">*</span></label>
            <input type="text" name="surname" id="surname" required>
            <label for="name">Имя<span class="rq">*</span></label>
            <input type="text" name="name" id="name" required>
            <label for="patronymic">Отчество</label>
            <input type="text" name="patronymic" id="patronymic">
            <label for="educational-institution">Учебное заведение<span class="rq">*</span></label>
            <input type="text" name="educational-institution" id="educational-institution" required>
            <label for="job-title">Должность (допустимо: Студент)<span class="rq">*</span></label>
            <input type="text" name="job-title" id="job-title" required>
            <label for="city">Город<span class="rq">*</span></label>
            <input type="text" name="city" id="city" required>
            <label for="phone">Номер телефона<span class="rq">*</span></label>
            <input type="text" name="phone" id="phone" required>
            <label for="e-lib-link">Ссылка на eLibrary</label>
            <input type="text" name="e-lib-link" id="e-lib-link">
            <label for="email">Эл. почта<span class="rq">*</span></label>
            <input type="email" name="email" id="email" required>
            <label for="pass">Пароль<span class="rq">*</span></label>
            <input type="password" name="pass" id="pass" required>
            <label for="rep-pas">Повторите пароль<span class="rq">*</span></label>
            <input type="password" name="rep-pas" id="rep-pas" required>
            <button type="submit" id="reg-btn">Зарегистрироваться</button>
        </form>
    </main>
    <script src="scripts/reg.js"></script>
    <?php
        if($_POST){
            $api_url = "http://sibadi-conf-hub:8080/api/people/registration";

            $payload = json_encode([
                    "surname" => $_POST["surname"],
                    "name" => $_POST["name"],
                    "patronymic" => empty($_POST) ? "" : $_POST["patronymic"],
                    "educationalInstitution" => $_POST["educational-institution"],
                    "jobTitle" => $_POST["job-title"],
                    "city" => $_POST["city"] 
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
        }
    ?>
</body>
</html>