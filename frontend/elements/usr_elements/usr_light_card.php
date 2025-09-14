<div class="header-usr-card">
    <span class="usr-name">
        <div class="usr-img-div"></div>
        <?= htmlspecialchars($peopleData->surname)?>
        <?= htmlspecialchars($peopleData->name)?>
        <?= $peopleData->verified ? "&#10004" : '' ?>
    </span>
</div>