using AutoMapper;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Hart_Check_Official.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hart_Check_Official.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;

        private readonly IMapper _mapper;

        public ChatController(IChatRepository chatRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }
        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<Chat>))]
        public IActionResult GetChat()
        {
            var chat = _mapper.Map<List<ChatDto>>(_chatRepository.GetChats());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(chat);
        }
    }
}
