using System;
using System.Collections.Generic;

namespace StudySignalR.Repositories
{
    public class Message
    {
        public Guid ChatId { get; set; }
        
        public string From { get; set; }
        
        public string Body { get; set; }
    }
    
    public interface IMessageRepository
    {
        bool SaveMessage(Message message);
    }
    
    public class MessageRepository : IMessageRepository
    {
        private readonly Dictionary<Guid, List<Message>> _messageStorage;

        public MessageRepository()
        {
            _messageStorage = new Dictionary<Guid, List<Message>>();
        }

        public bool SaveMessage(Message message)
        {
            if (!_messageStorage.ContainsKey(message.ChatId))
            {
                _messageStorage.Add(message.ChatId, new List<Message>());
            }
            
            _messageStorage[message.ChatId].Add(message);

            return true;
        }
    }
}