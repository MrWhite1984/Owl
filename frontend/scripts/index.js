let containerAddNewsBtn = document.getElementById("container-add-news-btn");
let revBtn = document.getElementById("rev-btn");
let createNewContainerSubmitBtn = document.getElementById("create-new-container-submit-btn");

let newsContainer = document.getElementById("news-container");
let createNewsContainer = document.getElementById("create-news-container");

containerAddNewsBtn.addEventListener("click", function(){
    newsContainer.style.display = "none";
    createNewsContainer.style.display = "block";

});

revBtn.addEventListener("click", function(){
    newsContainer.style.display = "block";
    createNewsContainer.style.display = "none";
});

createNewContainerSubmitBtn.addEventListener("click", function(){
    newsContainer.style.display = "block";
    createNewsContainer.style.display = "none";
});