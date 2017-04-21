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
        public void staffPasswordCorrectTest()
        {
            var mock = new Mock<IAuthRepository>();
            Staff staff = new Staff();
            staff.Username = "testUser";
            staff.HashedPassword = Crypto.HashPassword("Password");
            mock.Setup(framework => framework.getStaff("testUser"))
                .Returns(staff);
            IAuthRepository authRepo = mock.Object;
            AuthHandler authHandler = new AuthHandler(authRepo);
            Assert.AreEqual(true, authHandler.authorizeStaff("testUser", "Password"));
        }

        [TestMethod]
        public void staffPasswordIncorrectTest()
        {
            var mock = new Mock<IAuthRepository>();
            Staff staff = new Staff();
            staff.Username = "testUser";
            staff.HashedPassword = Crypto.HashPassword("Password");
            mock.Setup(framework => framework.getStaff("testUser"))
                .Returns(staff);
            IAuthRepository authRepo = mock.Object;
            AuthHandler authHandler = new AuthHandler(authRepo);
            Assert.AreEqual(false, authHandler.authorizeStaff("testUser", "Password!"));
        }

        [TestMethod]
        public void userPasswordCorrectTest()
        {
            var mock = new Mock<IAuthRepository>();
            User user = new User();
            user.Username = "testUser";
            user.HashedPassword = Crypto.HashPassword("Password");
            mock.Setup(framework => framework.getUser("testUser"))
                .Returns(user);
            IAuthRepository authRepo = mock.Object;
            AuthHandler authHandler = new AuthHandler(authRepo);
            Assert.AreEqual(true, authHandler.authorizeUser("testUser", "Password"));
        }

        [TestMethod]
        public void userPasswordIncorrectTest()
        {
            var mock = new Mock<IAuthRepository>();
            User user = new User();
            user.Username = "testUser";
            user.HashedPassword = Crypto.HashPassword("Password");
            mock.Setup(framework => framework.getUser("testUser"))
                .Returns(user);
            IAuthRepository authRepo = mock.Object;
            AuthHandler authHandler = new AuthHandler(authRepo);
            Assert.AreEqual(false, authHandler.authorizeUser("testUser", "Password!"));
        }
    }
}
