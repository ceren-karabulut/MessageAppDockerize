using MessageCase.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageCase.Data
{
   public class MessageHistory
    {
        public int Id { get; set; }

        public int ReceiverId { get; set; }

        public string Message { get; set; }

        public DateTime MessageDate { get; set; }

        public virtual User User { get; set; }
        public int SenderId { get; set; }
    }
}
