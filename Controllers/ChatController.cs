using System;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using perpark_api.DevelopmentClasses;
using perpark_api.Models;
using perpark_api.Models.Entities;
using static QRCoder.PayloadGenerator.SwissQrCode;

namespace perpark_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ChatController> _logger;
    Models.Context _db = new Models.Context();

    public ChatController(ILogger<ChatController> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage([FromBody] ChatMessages message)
    {
        try
        {
            message.Date = DateTime.Now;
            string roomId = "";
            if (message.SenderId > message.ReceiverId)
            {
                roomId = message.SenderId.ToString() +"&"+ message.ReceiverId.ToString();
            }
            else
            {
                roomId = message.ReceiverId.ToString() +"&"+ message.SenderId.ToString();
            }
            message.RoomId = roomId;
            _db.ChatMessages.Add(message);
            await _db.SaveChangesAsync();
            return Ok(message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
       
    }

    [HttpGet("GetHistory")]
    public IActionResult GetHistory(int mainId, int anotherId)
    {
        try
        {
            MessageVM vm = new MessageVM();
            var users = _db.Users.Where(x => x.UserId == mainId || x.UserId == anotherId).ToList();
            vm.Users = users;

            DateTime lastseven = DateTime.Now.AddDays(-15);
            var messages = _db.ChatMessages
            .Where(m => ((m.SenderId == mainId && m.ReceiverId == anotherId) ||
                         (m.SenderId == anotherId && m.ReceiverId == mainId)) &&
                         m.Date > lastseven && m.Date != null).ToList();
            vm.Messages = messages;

            return Ok(vm);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
    [HttpGet("GetMessageList")]
    public IActionResult GetMessageList(string userId)
    {
        try
        {
            /*   var result = _db.ChatMessages
       .Where(cm => cm.RoomId.StartsWith(userId + "&") || cm.RoomId.EndsWith("&" + userId))
       .GroupBy(cm => cm.RoomId)
       .Select(g => new {
           RoomId = g.Key,
           LastMessage = g.OrderByDescending(cm => cm.Date).FirstOrDefault().Message,
           Datee = g.Max(cm => cm.Date),
           SenderId = g.OrderByDescending(cm => cm.Date).FirstOrDefault().SenderId,
           ReceiverId = g.OrderByDescending(cm => cm.Date).FirstOrDefault().ReceiverId
       })
       .ToList(); */


            var messages = _db.ChatMessages
    .Where(cm => cm.RoomId.StartsWith(userId + "&") || cm.RoomId.EndsWith("&" + userId))
    .GroupBy(cm => cm.RoomId)
    .Select(g => new {
        RoomId = g.Key,
        LastMessage = g.OrderByDescending(cm => cm.Date).FirstOrDefault()
    })
    .ToList();

            var result = messages.Select(m => new {
                RoomId = m.RoomId,
                LastMessage = m.LastMessage.Message,
                Datee = m.LastMessage.Date,
                SenderId = m.LastMessage.SenderId,
                ReceiverId = m.LastMessage.ReceiverId,
                Sender = _db.Users.FirstOrDefault(u => u.UserId == m.LastMessage.SenderId),
                Receiver = _db.Users.FirstOrDefault(u => u.UserId == m.LastMessage.ReceiverId)
            }).Select(m => new {
                m.RoomId,
                m.LastMessage,
                m.Datee,
                m.SenderId,
                m.ReceiverId,
                SenderUsername = m.Sender.Username,
                SenderUserPP = m.Sender.ProfilePictureUrl,
                ReceiverUsername = m.Receiver.Username,
                ReceiverUserPP = m.Receiver.ProfilePictureUrl
            }).ToList();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        

    }
    

}