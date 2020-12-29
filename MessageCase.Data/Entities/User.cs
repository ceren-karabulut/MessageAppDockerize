using System;
using System.Collections.Generic;
using System.Text;

namespace MessageCase.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordSalt { get; set; }

        public byte[] PasswordHash { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual ICollection<MessageHistory> MessageHistories { get; set; }

        public virtual ICollection<BlockingUser> BlockingUsers { get; set; }
        
    }
}
