using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageCase.API.Dto
{
    public class MessageHistoryDto
    {
        public string SenderName { get; set; }

        public string Message { get; set; }

        public DateTime MessageDate { get; set; }

        public string ReceiverName { get; set; }


    }
}
