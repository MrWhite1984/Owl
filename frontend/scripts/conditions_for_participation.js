let changeInformBtn = document.getElementById("change-inform-btn");
let updateContainerDataBtn = document.getElementById("update-container-data-btn");
let revBtn = document.getElementById("rev-btn");

let container = document.getElementById("container");
let updateContainer = document.getElementById("update-container");

revBtn.addEventListener("click", function () {
    container.style.display = "block";
    updateContainer.style.display = "none";
});

changeInformBtn.addEventListener("click", function(){
    container.style.display = "none";
    updateContainer.style.display = "block";
});

updateContainerDataBtn.addEventListener("click", function(){
    container.style.display = "block";
    updateContainer.style.display = "none";
});

