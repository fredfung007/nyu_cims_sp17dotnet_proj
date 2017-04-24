using System;
using System.Web.Helpers;
using BusinessLogic.Handlers;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace BusinessLogicTest
{
    [TestClass]
    public class ProfileHandlerTest
    {
        [TestMethod]
        public void TestGetProfile()
        {
            var mock = new Mock<IProfileRepository>();
            Guid profileGuid = new Guid();
            var profile = new Profile
            {
                FirstName = "testFristName",
                LastName = "testLastName",
                Id = profileGuid
            };
            mock.Setup(framework => framework.GetProfile(profileGuid))
                .Returns(profile);
            var profileRepository = mock.Object;
            var profileHandler = new ProfileHandler(profileRepository);
            Assert.AreEqual("testFristName", profileHandler.GetProfile(profileGuid).FirstName);
            Assert.AreEqual("testLastName", profileHandler.GetProfile(profileGuid).LastName);
            Assert.AreEqual(profile, profileHandler.GetProfile(profileGuid));
        }

        [TestMethod]
        public void TestGetAddress()
        {
            var mock = new Mock<IProfileRepository>();
            Guid profileGuid = new Guid();
            var profile = new Profile
            {
                Address = new Address
                {
                    ZipCode = "10001"
                },
                Id = profileGuid
            };
            mock.Setup(framework => framework.GetProfile(profileGuid))
                .Returns(profile);
            var profileRepository = mock.Object;
            var profileHandler = new ProfileHandler(profileRepository);
            Assert.AreEqual("10001", profileHandler.GetAddress(profileGuid).ZipCode);
        }
    }
}
