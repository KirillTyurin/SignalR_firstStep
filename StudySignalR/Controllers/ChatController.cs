using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StudySignalR.Hubs;
using StudySignalR.Pages;
using StudySignalR.Repositories;

namespace StudySignalR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IChatRepository chatRepository, IHubContext<ChatHub> hubContext)
        {
            _chatRepository = chatRepository;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Chat chat)
        {
            if (_chatRepository.SaveChat(chat))
            {
                await _hubContext.Clients.All.SendAsync("addchat", chat);
            }

            return Ok();
        }
    }
}