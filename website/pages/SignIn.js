

let b_SignIn = document.getElementById("b_SignIn");
let nick_name = document.getElementById("nick_name");
let password = document.getElementById("password");

b_SignIn.onclick = async function(){
    let nick = nick_name.value;
    let user_password = password.value;
    let  result;

    result = await send("SignIn",[ nick, user_password]) ;

    if(result == "OK") {
        alert("!נרשמת בהצלחה");
        location.href = "/website/pages/Login.html";

    }
    else if(result == "exists"){
        alert("משתמש זה קיים, אנא  הזן משתמש אחר")

    }
    else if(result == "invalid"){
        alert("אנא הזן סיסמה יותר בטוחה, עם לפחות 5 תווים")
    }
 
}
