using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using StudySignalR.Repositories;

namespace StudySignalR.Hubs
{
    public class ChatConnection
    {
        public Guid ChatId { get; set; }
    }
    
    public class ChatHub : Hub
    {
        public void JoinToChat(ChatConnection connection)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{connection.ChatId}");
        }

        public async Task CreateChat(string chat)
        {
            var id = Guid.NewGuid();
            await Clients.All.SendAsync("addchat", id);
        }
    }
}