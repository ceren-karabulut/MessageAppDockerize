using MessageCase.Data.Context;
using MessageCase.Data.Entities;
using MessageCase.Repository;
using MessageCase.Repository.Concrete;
using MessageCase.Test.Helpers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MessageCase.Test.RepositoriesTests
{
    public class UserRepositoryTest
    {
        [Fact]
        public async Task RegisterUserShouldNotNull()
        {
            //arrange
            var userList = new List<User>();

            var dbSetMoq = TestHelper.GetDbSetMoq(userList);

            var contextMoq = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            contextMoq.Setup(x => x.User)
                .Returns(dbSetMoq.Object);

            contextMoq.Setup(x => x.SaveChanges()).Returns(1);

            var repository = new UserRepository(contextMoq.Object);

            //act

            byte[] passwordHash, passwordSalt;
            string password = "1234";


            RepositoryHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User()
            {
                Id = 1,
                Username = "ceren",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreateTime = DateTime.Now
            };

            var registeringUser = await repository.Register(user, password);

            //assert
            Assert.NotNull(registeringUser);

        }

        [Fact]
        public void UserLoginShouldReturnUser()
        {
            //arrange
            byte[] passwordHash, passwordSalt;
            string password = "1234";
            string username = "ceren";

            RepositoryHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var userList = new List<User>()
            {
                new User()
                {
                    Id=1,
                    Username=username,
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                    CreateTime = DateTime.Now
                }
            };

            var dbSetMoq = TestHelper.GetDbSetMoq(userList);

            var contextMoq = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            contextMoq.Setup(x => x.User)
                .Returns(dbSetMoq.Object);

            var repository = new UserRepository(contextMoq.Object);
            //act
            var result = repository.Login(username, password);
            //assert
            Assert.IsType<Task<User>>(result);
        }


        [Fact]
        public void GetUsernameByIdShouldGivenId()
        {
            var userId = 1;
            var exceptedUsername = "ceren";

            var userList = new List<User>()
            {
                new User()
                {
                    Id = userId,
                    Username = exceptedUsername
                },

                new User()
                {
                    Id = 3,
                    Username ="berfin"
                }
            };

            var dbSetMoq = TestHelper.GetDbSetMoq(userList);

            var contextMoq = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            contextMoq.Setup(x => x.User)
                .Returns(dbSetMoq.Object);

            var repository = new UserRepository(contextMoq.Object);

            var result = repository.GetUsernameById(userId);

            Assert.Equal(exceptedUsername, result);

        }

        [Theory]
        [InlineData("ceren")]
        public void UserExistShouldNotNull(string username)
        {
            //arange
            var userList = new List<User>();

            var dbSetMoq = TestHelper.GetDbSetMoq(userList);

            var contextMoq = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            contextMoq.Setup(x => x.User)
                .Returns(dbSetMoq.Object);

            var repository = new UserRepository(contextMoq.Object);

            var result = repository.UserExist(username);

            //act
            Assert.NotNull(result);

        }

     

    }
}
