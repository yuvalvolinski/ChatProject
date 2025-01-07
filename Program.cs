using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

class Message
{
  public int id;
  public string MessageContent;
  public string NickName;
}

class User{
  public string NickName;
  public string Password;

  public bool Active;
}



class Program
{
  
  

  static void Main()
  {
    int port = 5000;
     List<User> usersList = new  List<User>();
     List<Message>  messageList  = new List<Message>();

    var server = new Server(port);

    Console.WriteLine("The server is running");
    Console.WriteLine($"Main Page: http://localhost:{port}/website/pages/login.html");
    string basePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\website");


    while (true)
    {
      (var request, var response) = server.WaitForRequest();

      string relativePath = request.Path;
      if(relativePath.IndexOf("website/")  > -1)
      {
          relativePath = relativePath.Replace("website/", "");
      }

  
    string requestedFilePath = Path.Combine(basePath, relativePath);

    
    requestedFilePath = Path.GetFullPath(requestedFilePath);

    Console.WriteLine($"Requested File Path: {requestedFilePath}");
      if (File.Exists(requestedFilePath))
      {
        var file = new File(requestedFilePath);
        response.Send(file);
      }
      else if (request.ExpectsHtml())
      {
        var file = new File(Path.Combine(basePath, "pages/404.html"));
        //var file = new File("website/pages/404.html");
        response.SetStatusCode(404);
        response.Send(file);
      }
      else
      {
        try
        {
          
           if(request.Path  == "NewMessage"){
            (string message, string NickName) = request.GetBody<(string, string)>();
            //Console.WriteLine("New message");
            Message msg = new  Message();
            msg.MessageContent  = message;
            msg.NickName = NickName;
            msg.id = messageList.Count;
            messageList.Add(msg);
            //Console.WriteLine("count: " + messageList.Count);

          }
          else if(request.Path == "Getmessages"){
            int messageCounter = request.GetBody<int>();
            messageCounter++;
            List<Message> NewMessageList = new List<Message>();
            for(int i = messageCounter; i < messageList.Count ;  i++){
              NewMessageList.Add(messageList[i]);
            }

            response.Send(NewMessageList);
          
          }
          else if(request.Path == "GetUsers"){
            List<User> ActiveUsers = new List<User>();
            for(int i = 0; i<usersList.Count;i++){
              if(usersList[i].Active == true){
                ActiveUsers.Add(usersList[i]);
              }
            }
            response.Send(ActiveUsers);


          }
          else if(request.Path == "SignIn"){
            (string NickName, string Password) = request.GetBody<(string, string)>();
            bool is_exists =  false;

            for(int i = 0; i<usersList.Count; i++){
              if(usersList[i].NickName == NickName){
                is_exists = true;
                break;

              }
            }

            if(is_exists == true){
              response.Send("exists");
             
            }
            else if(Password.Length<5){
              response.Send("invalid");
             
            }
            
            else
            {
              User usr = new User();
              usr.NickName = NickName;
              usr.Password = Password;
              usr.Active = false;

              usersList.Add(usr);
              response.Send("OK");


            }

            
          }
          else if(request.Path == "Login"){
            (string NickName, string Password) = request.GetBody<(string, string)>();

            bool is_ok = false;

            for(int i = 0; i<usersList.Count; i++){
              if(usersList[i].NickName == NickName && usersList[i].Password == Password){
               is_ok = true; 
               usersList[i].Active = true;
              }
            }

            if(is_ok == true){
              Message msg = new  Message();
              msg.MessageContent  = NickName + " join to chat";
              msg.NickName = "";
              msg.id = messageList.Count;
              messageList.Add(msg);
    
              response.Send("OK");
            }
            else{
              response.Send("error");
            }
          
            

          }
          else if(request.Path == "exit"){
            string NickName = request.GetBody<string>();
            Console.WriteLine("ok");

            for(int i = 0; i<usersList.Count; i++){
              if(usersList[i].NickName == NickName){

                usersList[i].Active = false;
                response.Send("ok");
                break;

            
              }
            }




          }
          else {
            response.SetStatusCode(405);
          }
        }
        catch (Exception exception)
        {
          Log.WriteException(exception);
        }
      }

      response.Close();
    }
  }
}


