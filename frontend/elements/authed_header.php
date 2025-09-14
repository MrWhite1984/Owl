<?php
    ob_start();
    include __DIR__ . '/usr_elements/usr_light_card.php';
    $cardContent = ob_get_clean();
?>

<header class="row">
    <div class="nav-btn-div col-1"><button id="nav-btn">&#9776;</button></div>
    <div class="space-div col-7"></div>
    <div class="notify-btn-block"><button class="ntfy-btn"><span class="ntfy-bell">&#128276;</span></button></div>
    <div class="header-usr-card-div"><?= $cardContent?> </div>   
</header>