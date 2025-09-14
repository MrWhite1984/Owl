let navBtn = document.getElementById("nav-btn");
let sidebarContainer = document.getElementById("sidebar-container");

navBtn.addEventListener("click", function(){
    let computedStyle = window.getComputedStyle(sidebarContainer);
    if (computedStyle.display === "none") {
        sidebarContainer.style.display = "block";
    } else {
        sidebarContainer.style.display = "none";
    }
});
