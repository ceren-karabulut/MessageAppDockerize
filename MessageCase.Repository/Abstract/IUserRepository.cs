using MessageCase.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageCase.Repository.Abstract
{
   public interface IUserRepository
    {
        Task<bool> UserExist(string username);

        Task<User> Login(string username, string password);

        Task<User> Register(User user, string password);

        Task<int> GetUserIdByName(string username);

        string GetUsernameById(int userId);
        
    }
}
