let pass_field = document.getElementById("pass");
let rep_pass_field = document.getElementById("rep-pas");

let reg_btn = document.getElementById("reg-btn");

rep_pass_field.addEventListener("input", ()=>{
    if(pass_field.value != rep_pass_field.value){
        rep_pass_field.style.backgroundColor = "pink";
        rep_pass_field.style.borderColor = "red";
    }
    else{
        rep_pass_field.style.backgroundColor = "white";
        rep_pass_field.style.borderColor = "black";
    }        
});

rep_pass_field.addEventListener("change", () => {
    if(pass_field.value != rep_pass_field.value)
        rep_pass_field.setCustomValidity("Пароли не совпадают");
});