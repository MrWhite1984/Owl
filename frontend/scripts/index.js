let containerAddNewsBtn = document.getElementById("container-add-news-btn");
let revBtn = document.getElementById("rev-btn");

let newsContainer = document.getElementById("news-container");
let createNewsContailner = document.getElementById("create-news-container");

containerAddNewsBtn.addEventListener("click", function(){
    newsContainer.style.display = "none";
    createNewsContailner.style.display = "block";

});

revBtn.addEventListener("click", function(){
    newsContainer.style.display = "block";
    createNewsContailner.style.display = "none";
});