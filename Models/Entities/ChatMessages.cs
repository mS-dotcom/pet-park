using System;
using System.ComponentModel.DataAnnotations;
namespace perpark_api.Models.Entities

{
	public class ChatMessages
	{
		[Key]
		public int MessageId { get; set; }
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public string Message { get; set; }
		public string? RoomId { get; set; }
		public DateTime? Date { get; set; }
	}
}

