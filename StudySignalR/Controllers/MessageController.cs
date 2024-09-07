using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StudySignalR.Hubs;
using StudySignalR.Repositories;

namespace StudySignalR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageController(IMessageRepository messageRepository, IHubContext<ChatHub> hubContext)
        {
            _messageRepository = messageRepository;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Message message)
        {
            if (_messageRepository.SaveMessage(message))
            {
                await _hubContext.Clients.Group($"chat_{message.ChatId}").SendAsync("GetMessage", message);
            }

            return Ok();
        }
    }
}