window.addEventListener("DOMContentLoaded", function () {
    const token = localStorage.getItem("token");
    const role = localStorage.getItem("role");
    const authHeader = document.getElementById("auth-header");
    const unauthHeader = document.getElementById("unauth-header");

    const navBar = document.getElementById("nav-bar");

    if (token === null) {
        authHeader.style.display = "none";
    }
    else {
        unauthHeader.style.display = "none";
        if (role === "USER") {
            
        }
    }


});

let navBtn = document.getElementById("nav-btn");
let sidebarContainer = document.getElementById("sidebar-container");

navBtn.addEventListener("click", function () {
    let computedStyle = window.getComputedStyle(sidebarContainer);
    if (computedStyle.display === "none") {
        sidebarContainer.style.display = "block";
    } else {
        sidebarContainer.style.display = "none";
    }
});



let containerAddNewsBtn = document.getElementById("container-add-news-btn");
let revBtn = document.getElementById("rev-btn");
let createNewContainerSubmitBtn = document.getElementById("create-new-container-submit-btn");

let newsContainer = document.getElementById("news-container");
let createNewsContainer = document.getElementById("create-news-container");

containerAddNewsBtn.addEventListener("click", function () {
    newsContainer.style.display = "none";
    createNewsContainer.style.display = "block";

});

revBtn.addEventListener("click", function () {
    newsContainer.style.display = "block";
    createNewsContainer.style.display = "none";
});

createNewContainerSubmitBtn.addEventListener("click", function () {
    newsContainer.style.display = "block";
    createNewsContainer.style.display = "none";
});