<?php
    if($_SESSION["selected_role"] == "ADMIN"){
        ob_start();
        include 'usr_elements/usr_light_card.php';
        $cardContent = ob_get_clean();
        echo "<header><button id=\"nav-btn\"><img src=\"\" alt=\"nav\"></button>".$cardContent."</header>";
    }
?>