

let B_startChat = document.getElementById("b_startChat");
let nick_name = document.getElementById("nick_name");
let password = document.getElementById("password");

if(localStorage.getItem("NickName") != null) {
    nick_name.value = localStorage.getItem("NickName");
}
B_startChat.onclick = async function(){
    let nick = nick_name.value;
    let user_password = password.value;
    let  result;     

    result = await send("Login",[ nick, user_password]) ;

    if(result == "OK"){
        localStorage.setItem("NickName", nick);
        location.href = "/website/pages/Chat.html";

    }
    else{
        alert("פרטי המשתמש או סיסמא אינה תקינים");
    }

   
 

}

