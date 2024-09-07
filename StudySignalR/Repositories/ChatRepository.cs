using System;
using System.Collections.Generic;
using System.Linq;

namespace StudySignalR.Repositories
{
    public class Chat
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
    }
    
    
    public interface IChatRepository
    {
        List<Chat> GetAllChats();

        bool SaveChat(Chat chat);

        bool RemoveChat(Guid id);
    }
    
    public class ChatRepository : IChatRepository
    {
        private readonly List<Chat> _chatList;

        public ChatRepository()
        {
            _chatList = new List<Chat>();
        }

        public List<Chat> GetAllChats()
        {
            return _chatList;
        }

        public bool SaveChat(Chat chat)
        {
            if (_chatList.Any(x => x.Id == chat.Id))
            {
                return false;
            }

            if (chat.Id != Guid.Empty)
            {
                return false;
            }

            chat.Id = Guid.NewGuid();
            _chatList.Add(chat);

            return true;
        }

        public bool RemoveChat(Guid id)
        {
            var existChat = _chatList.FirstOrDefault(x => x.Id == id);
            if (existChat == null)
            {
                return false;
            }

            _chatList.Remove(existChat);
            return true;
        }
    }
}