using MessageCase.Data;
using MessageCase.Data.Context;
using MessageCase.Data.Entities;
using MessageCase.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MessageCase.Repository.Concrete
{
    public class MessageHistoryRepository : IMessageHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public MessageHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task Add(MessageHistory messageHistory)
        {
            await _context.MessageHistory.AddAsync(messageHistory);
            await _context.SaveChangesAsync();
        }


        public async Task<List<MessageHistory>> GetMessageHistory(int userId)
        {
            var historyList = await _context.MessageHistory
                .Include(x => x.User)
                .Where(x => x.SenderId == userId || x.ReceiverId == userId)
                .AsNoTracking()
                .OrderByDescending(x => x.MessageDate)
                .ToListAsync();

            return historyList;

        }

        public async Task AddBlockingkUser(BlockingUser blockingUser)
        {
            await _context.BlockingUser.AddAsync(blockingUser);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserBlocking(int userId, int blockingUserId)
        {
            var result = await _context.BlockingUser
                .Include(x => x.User)
                .Where(x => x.UserId == userId && x.BlockingUserId == blockingUserId)
                .AsNoTracking()
                .AnyAsync(x => x.BlockingUserId == blockingUserId);

            if (result)
            {
                return true;
            }

            return false;
        }
    }
}
