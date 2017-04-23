using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using System.Web.Helpers;
using BusinessLogic.Handlers;

namespace BusinessLogicTest
{
    [TestClass]
    public class AuthHandlerTest
    {
        [TestMethod]
        public void StaffPasswordCorrectTest()
        {
            var mock = new Mock<IAuthRepository>();
            var staff = new Staff
            {
                Username = "testUser",
                HashedPassword = Crypto.HashPassword("Password")
            };
            mock.Setup(framework => framework.getStaff("testUser"))
                .Returns(staff);
            var authRepo = mock.Object;
            var authHandler = new AuthHandler(authRepo);
            Assert.AreEqual(true, authHandler.authorizeStaff("testUser", "Password"));
        }

        [TestMethod]
        public void StaffPasswordIncorrectTest()
        {
            var mock = new Mock<IAuthRepository>();
            var staff = new Staff
            {
                Username = "testUser",
                HashedPassword = Crypto.HashPassword("Password")
            };
            mock.Setup(framework => framework.getStaff("testUser"))
                .Returns(staff);
            var authRepo = mock.Object;
            var authHandler = new AuthHandler(authRepo);
            Assert.AreEqual(false, authHandler.authorizeStaff("testUser", "Password!"));
        }

        [TestMethod]
        public void UserPasswordCorrectTest()
        {
            var mock = new Mock<IAuthRepository>();
            var user = new User
            {
                Username = "testUser",
                HashedPassword = Crypto.HashPassword("Password")
            };
            mock.Setup(framework => framework.getUser("testUser"))
                .Returns(user);
            var authRepo = mock.Object;
            var authHandler = new AuthHandler(authRepo);
            Assert.AreEqual(true, authHandler.authorizeUser("testUser", "Password"));
        }

        [TestMethod]
        public void UserPasswordIncorrectTest()
        {
            var mock = new Mock<IAuthRepository>();
            var user = new User
            {
                Username = "testUser",
                HashedPassword = Crypto.HashPassword("Password")
            };
            mock.Setup(framework => framework.getUser("testUser"))
                .Returns(user);
            var authRepo = mock.Object;
            var authHandler = new AuthHandler(authRepo);
            Assert.AreEqual(false, authHandler.authorizeUser("testUser", "Password!"));
        }
    }
}
