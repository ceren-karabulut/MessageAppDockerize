using MessageCase.Repository.Abstract;
using MessageCase.Repository.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MessageCase.Data;
using MessageCase.API.Dto;
using MessageCase.Data.Entities;

namespace MessageCase.API.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IMessageHistoryRepository _messageRepository;
        public MessageController(IUserRepository userRepository, IMessageHistoryRepository messageHistoryRepository)
        {
            _messageRepository = messageHistoryRepository;
            _userRepository = userRepository;
        }

        protected int GetUserClaimId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var senderId = identity.FindFirst("UserId").Value;
            return Convert.ToInt32(senderId);
        }


        [HttpPost]
        [Route("sendmessage")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto messageDto)
        {
            if (!await _userRepository.UserExist(messageDto.Username))
            {
                return BadRequest("Mesaj atmaj istediğiniz kullanici bulunamadi!");
            }

            var receiverId = await _userRepository.GetUserIdByName(messageDto.Username);
             

            if (await _messageRepository.IsUserBlocking(receiverId, GetUserClaimId()))
            {
                return BadRequest("Bu kullaniciya mesaj atamazsiniz!");
            }

            var message = new MessageHistory()
            {
                SenderId = GetUserClaimId(),
                ReceiverId = receiverId,
                Message = messageDto.Message,
                MessageDate = DateTime.Now.Date
            };

            await _messageRepository.Add(message);
            return Ok("Mesaj gonderildi!");
        }

        [HttpGet]
        [Route("messagehistories")]
        public async Task<List<MessageHistoryDto>> GetMessageHistories()
        {

            var userId = GetUserClaimId();

            var messageHistory = await _messageRepository.GetMessageHistory(userId);

            var result = messageHistory.Select(x => new MessageHistoryDto()
            {
                SenderName = _userRepository.GetUsernameById(x.SenderId),
                ReceiverName = _userRepository.GetUsernameById(x.ReceiverId),
                Message = x.Message,
                MessageDate = x.MessageDate
            });

            return result.ToList();
        }

        [HttpPost]
        [Route("blockuser")]
        public async Task<IActionResult> BlockUser([FromBody] BlockUserDto blockUserDto)
        {
            var userId = GetUserClaimId();

            if (!await _userRepository.UserExist(blockUserDto.BlockingUser))
            {
                return BadRequest("Engellemek istediğiniz kullanici bulunamadi!");
            }

            var blockingUser = new BlockingUser()
            {
                UserId = userId,
                BlockingUserId = await _userRepository.GetUserIdByName(blockUserDto.BlockingUser)
            };

            await _messageRepository.AddBlockingkUser(blockingUser);

            return Ok("kullanici engellendi!");
        }

    }
}
