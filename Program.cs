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


    while (true)
    {
      (var request, var response) = server.WaitForRequest();

      Console.WriteLine($"Recieved a request with the path: {request.Path}");

      if (File.Exists(request.Path))
      {
        var file = new File(request.Path);
        response.Send(file);
      }
      else if (request.ExpectsHtml())
      {
        var file = new File("website/pages/404.html");
        response.SetStatusCode(404);
        response.Send(file);
      }
      else
      {
        try
        {
          if (request.Path == "NewUser") {
            string NickName = request.GetBody<string>();
            User usr = new  User();
            usr.NickName = NickName;

            usersList.Add(usr);
            response.Send("OK");

            
          }
          else if(request.Path  == "NewMessage"){
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
            response.Send(usersList);


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


