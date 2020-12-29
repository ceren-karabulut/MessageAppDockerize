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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
            {
                return null;
            }

            if (RepositoryHelper.VerifyPassword(password, user.PasswordHash, user.PasswordSalt) != true)
            {
                return null;
            }

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            RepositoryHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;

        }

        public async Task<bool> UserExist(string username)
        {
            var result = await _context.User.AnyAsync(x => x.Username == username);
            if (result)
            {
                return true;
            }

            return false;
        }

        public async Task<int> GetUserIdByName(string username)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Username == username);

            return user.Id;
        }

        public string GetUsernameById(int userId)
        {
            var user = _context.User.FirstOrDefault(x => x.Id == userId);

            return user.Username;
        }

       
    }
}
