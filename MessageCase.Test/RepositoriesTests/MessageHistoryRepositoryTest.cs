using MessageCase.Data;
using MessageCase.Data.Context;
using MessageCase.Data.Entities;
using MessageCase.Repository.Concrete;
using MessageCase.Test.Helpers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MessageCase.Test.RepositoriesTests
{
    public class MessageHistoryRepositoryTest
    {
        [Fact]
        public void GetMessageHistoriesNotNull()
        {
            //act
            var userId = 3;
            var messageList = new List<MessageHistory>()
            {
                new MessageHistory()
                {
                    Id =1,
                    ReceiverId =2,
                    SenderId =userId,
                    Message ="Hello",
                    MessageDate = DateTime.Now
                }
            };

            
            var dbsetMoq = TestHelper.GetDbSetMoq(messageList);

            var contextMoq = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            contextMoq.Setup(x => x.MessageHistory)
                .Returns(dbsetMoq.Object);

            var repository = new MessageHistoryRepository(contextMoq.Object);

            //act
            var messages = repository.GetMessageHistory(userId);

            //assert
            Assert.NotNull(messages);
        }

        [Fact]
        public void AddMessageHistoryShouldNotNull()
        {
            //act
            var messageHistoryList = new List<MessageHistory>();


            var dbsetMoq = TestHelper.GetDbSetMoq(messageHistoryList);
            var contextMoq = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            contextMoq.Setup(x => x.MessageHistory)
                .Returns(dbsetMoq.Object);

            contextMoq.Setup(x => x.SaveChanges()).Returns(1);
            
            var repository = new MessageHistoryRepository(contextMoq.Object);

            //act
            
            var messageHistory = new MessageHistory()
            {
                Id = 1,
                ReceiverId = 2,
                SenderId = 3,
                Message = "Hello",
                MessageDate = DateTime.Now
            };

            var addingMessage = repository.Add(messageHistory);

            //assert
            Assert.NotNull(addingMessage);
            
        }

        [Fact]
        public void AddBlockingUserShouldNotNull()
        {   
            //arrange
            var blockingUserList = new List<BlockingUser>();

            var dbSetMoq = TestHelper.GetDbSetMoq(blockingUserList);

            var contextMoq = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            contextMoq.Setup(x => x.BlockingUser)
                .Returns(dbSetMoq.Object);

            contextMoq.Setup(x => x.SaveChanges()).Returns(1);

            var repository = new MessageHistoryRepository(contextMoq.Object);

            //act
            var blockingUser = new BlockingUser()
            {
                Id = 1,
                BlockingUserId = 1,
                UserId = 2
            };

            var addingBlockingUser = repository.AddBlockingkUser(blockingUser);

            //assert
            Assert.NotNull(addingBlockingUser);
        }
    }
}
