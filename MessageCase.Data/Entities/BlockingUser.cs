using System;
using System.Collections.Generic;
using System.Text;

namespace MessageCase.Data.Entities
{
    public class BlockingUser
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int BlockingUserId { get; set; }
    }
}
