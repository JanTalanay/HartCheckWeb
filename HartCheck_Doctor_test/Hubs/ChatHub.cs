using Microsoft.AspNetCore.SignalR;

namespace HartCheck_Doctor_test.Hubs
{
    public class ChatHub : Hub
    {

        public async Task SendMessage(string user, string message)
        {
            Console.WriteLine("From: " + user + " Message: " + message);
            Clients.All.SendAsync("ReceiveMessage",user,message);
        }
    }
}
