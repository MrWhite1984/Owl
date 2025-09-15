<div id="sidebar-container" class="sidebar-container">
    <nav>
        <?php if ($role == null): ?>
            <a class="nav-link" href="../index.php">
                <div class="nav-element">Главная</div>
            </a>
            <a class="nav-link" href="../conditions_for_participation.php">
                <div class="nav-element">Условия участия</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">О конференции</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Место проведения</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Конференции</div>
            </a>
        <?php elseif (in_array($role->title, $full_rights)): ?>
            <a class="nav-link" href="../index.php">
                <div class="nav-element">Главная</div>
            </a>
            <a class="nav-link" href="../conditions_for_participation.php">
                <div class="nav-element">Условия участия</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">О конференции</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Место проведения</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Конференции</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Мои статьи</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Модерация</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Статистика</div>
            </a>
        <?php elseif (in_array($role->title, $moders_rights)): ?>
            <a class="nav-link" href="../index.php">
                <div class="nav-element">Главная</div>
            </a>
            <a class="nav-link" href="../conditions_for_participation.php">
                <div class="nav-element">Условия участия</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">О конференции</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Место проведения</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Конференции</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Мои статьи</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Модерация</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Статистика</div>
            </a>
        <?php elseif (in_array($role->title, $user_rights)): ?>
            <a class="nav-link" href="../index.php">
                <div class="nav-element">Главная</div>
            </a>
            <a class="nav-link" href="../conditions_for_participation.php">
                <div class="nav-element">Условия участия</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">О конференции</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Место проведения</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Конференции</div>
            </a>
            <a class="nav-link" href="">
                <div class="nav-element">Мои статьи</div>
            </a>
        <?php endif; ?>
    </nav>
</div>