using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication_SRPFIQ.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication_SRPFIQ.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication_SRPFIQ.Models;

namespace WebApplication_SRPFIQ.Controllers.Tests
{
    [TestClass()]
    public class UsersControllerTests
    {
        private UsersController GetControllerWithDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<SRPFIQDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new SRPFIQDbContext(options);
            return new UsersController(context);
        }

        [TestMethod()]
        public async Task CreateTest1Async()
        {
            // Arrange
            var dbName = "ValidModelDb";
            var controller = GetControllerWithDb(dbName);

            var user = new Users
            {
                ID = 1,
                FirstName = "John",
                LastName = "Doe",
                UserName = "jdoe",
                PasswordHash = "hashedpw",
                Active = true
            };

            // Act
            var result = await controller.Create(user);

            // Assert redirection
            var redirect = result as RedirectToActionResult;
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.ActionName);

            // Vérifier via un nouveau contexte EF Core
            var options = new DbContextOptionsBuilder<SRPFIQDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            using var contextCheck = new SRPFIQDbContext(options);
            var count = await contextCheck.Users.CountAsync();
            Assert.AreEqual(1, count);
        }

        [TestMethod()]
        public void UsersControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IndexTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DetailsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        

        [TestMethod()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            Assert.Fail();
        }
    }
}