let B_send = document.getElementById("B_send");
let txt_message = document.getElementById("txt_message");
let message_list = document.getElementById("message_list");
let messageCounter = -1;
let users_list = document.getElementById("users_list");
let b_exit = document.getElementById("b_exit");

setTimeout(getMessages, 1000);
setTimeout(getUsers, 1000);

B_send.onclick = async function(){
    let message = txt_message.value;
    let Nickname = localStorage.getItem("NickName");

    txt_message.value = "";
    txt_message.focus();

    
    

     await send("NewMessage",[message,Nickname]) ;
    
}

txt_message.onkeydown = function(event){
    if (event.key === 'Enter') {
        B_send.click();
    }
}


async function getMessages(){
    let messages;
    let Divmessagee;

    
    messages = await send("Getmessages", messageCounter);
    


    for(i=0; i< messages.length; i++){
        
        
        Divmessagee = document.createElement("div");
        Divmessagee.style.display = "block";
        Divmessagee.className = "oneMessage"
        Divmessagee.innerText = messages[i].NickName  + "- "  +   messages[i].MessageContent;

        if(messages[i].NickName == ""){
            Divmessagee.style.color="red";
        }

        messageCounter  =  messages[i].id;
        message_list.appendChild(Divmessagee);
        
    }

    setTimeout(getMessages, 1000);
}


async function getUsers(){
    let Users;
    let DivUser;

    //console.log('messageCounter', messageCounter)
   Users = await send("GetUsers"," ");
    //console.log(messages);

    users_list.innerHTML  = '';
    for(i=0; i< Users.length; i++){
        
        
        DivUser = document.createElement("div");
        DivUser.style.display = "block"
        DivUser.innerText = Users[i].NickName;
        
       users_list.appendChild(DivUser);

        
    }

    setTimeout(getUsers, 1000);
}

b_exit.onclick = async function(){

    console.log("ok");
    let User = localStorage.getItem("NickName");

    result = await send("exit", User) ;

    if(result == "ok"){
        alert("להתראות");
        location.href = "/website/pages/Login.html";

    }



}








