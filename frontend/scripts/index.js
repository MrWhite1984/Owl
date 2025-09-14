let navBtn = document.getElementById("nav-btn");
let sidebarContainer = document.getElementById("sidebar-container");

navBtn.addEventListener("click", function(){
    if(sidebarContainer.style.display == "none"){
        sidebarContainer.style.display = "block";    
    }
    else{
        sidebarContainer.style.display = "none";
    }
});
