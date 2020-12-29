using MessageCase.Data;
using MessageCase.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageCase.Repository.Abstract
{
    public interface IMessageHistoryRepository 
    {
        Task Add(MessageHistory messageHistory);

        Task<List<MessageHistory>> GetMessageHistory(int userId);

        Task  AddBlockingkUser(BlockingUser blockingUser);

        Task<bool> IsUserBlocking(int userId, int blockingUserId);
    }
}
