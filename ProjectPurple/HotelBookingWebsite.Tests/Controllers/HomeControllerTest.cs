﻿using System.Web.Mvc;
using HotelBookingWebsite.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelBookingWebsite.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}