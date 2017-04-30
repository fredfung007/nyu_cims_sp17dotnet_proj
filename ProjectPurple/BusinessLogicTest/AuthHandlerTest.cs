using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using System.Web.Helpers;
using BusinessLogic.Handlers;
using DataAccessLayer.EF;

namespace BusinessLogicTest
{
    [TestClass]
    public class AuthHandlerTest
    {
        //[TestMethod]
        //public void StaffPasswordCorrectTest()
        //{
        //    var mock = new Mock<IAuthRepository>();
        //    var staff = new Staff
        //    {
        //        Username = "testUser",
        //        HashedPassword = Crypto.HashPassword("Password")
        //    };
        //    mock.Setup(framework => framework.GetStaff("testUser"))
        //        .Returns(staff);
        //    var authRepo = mock.Object;
        //    var authHandler = new AuthHandler(authRepo);
        //    Assert.AreEqual(true, authHandler.AuthorizeStaff("testUser", "Password"));
        //}

        //[TestMethod]
        //public void StaffPasswordIncorrectTest()
        //{
        //    var mock = new Mock<IAuthRepository>();
        //    var staff = new Staff
        //    {
        //        Username = "testUser",
        //        HashedPassword = Crypto.HashPassword("Password")
        //    };
        //    mock.Setup(framework => framework.GetStaff("testUser"))
        //        .Returns(staff);
        //    var authRepo = mock.Object;
        //    var authHandler = new AuthHandler(authRepo);
        //    Assert.AreEqual(false, authHandler.AuthorizeStaff("testUser", "Password!"));
        //}

        [TestMethod]
        public void UserPasswordCorrectTest()
        {
            var mock = new Mock<IAuthRepository>();
            var user = new AspNetUser
            {
                UserName = "testUser",
                PasswordHash = Crypto.HashPassword("Password")
            };
            mock.Setup(framework => framework.GetUser("testUser"))
                .Returns(user);
            var authRepo = mock.Object;
            var authHandler = new AuthHandler(authRepo);
            Assert.AreEqual(true, authHandler.AuthorizeUser("testUser", "Password"));
        }

        [TestMethod]
        public void UserPasswordIncorrectTest()
        {
            var mock = new Mock<IAuthRepository>();
            var user = new AspNetUser
            {
                UserName = "testUser",
                PasswordHash = Crypto.HashPassword("Password")
            };
            mock.Setup(framework => framework.GetUser("testUser"))
                .Returns(user);
            var authRepo = mock.Object;
            var authHandler = new AuthHandler(authRepo);
            Assert.AreEqual(false, authHandler.AuthorizeUser("testUser", "Password!"));
        }
    }
}
