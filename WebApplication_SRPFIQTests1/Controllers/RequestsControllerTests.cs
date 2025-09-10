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
    public class RequestsControllerTests
    {
            private RequestsController GetControllerWithDb(string dbName)
            {
                var options = new DbContextOptionsBuilder<SRPFIQDbContext>()
                    .UseInMemoryDatabase(databaseName: dbName)
                    .Options;

                var context = new SRPFIQDbContext(options);
                return new RequestsController(context);
            }

        [TestMethod()]
        public void RequestsControllerTest()
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
        public async Task Create_InvalidModel_ReturnsViewWithSelectList()
        {
            // Arrange
            var dbName = "RequestsValidDb";
            var controller = GetControllerWithDb(dbName);

            var request = new Requests
            {
                ID = 1,
                FolioNumber = "F12345",
                FullName = "Alice Smith",
                Email = "alice@test.com",
                PhoneNumber = "123456789",
                NbPregnancy = "1",
                CreatedDate = System.DateTime.Now
            };

            // Act
            var result = await controller.Create(request);

            // Assert redirection
            var redirect = result as RedirectToActionResult;
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.ActionName);

            // Vérifier que la requête a été ajoutée
            var optionsCheck = new DbContextOptionsBuilder<SRPFIQDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            using var contextCheck = new SRPFIQDbContext(optionsCheck);
            var count = await contextCheck.Requests.CountAsync();
            Assert.AreEqual(1, count);
        }

        [TestMethod()]
        public async Task Create_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var dbName = "RequestsValidDb";
            var controller = GetControllerWithDb(dbName);

            var request = new Requests
            {
                ID = 1,
                FolioNumber = "F12345",
                FullName = "Alice Smith",
                Email = "alice@test.com",
                PhoneNumber = "123456789",
                CreatedDate = System.DateTime.Now,
                NbPregnancy = "1",
                IsMonoparental = false,
                EstimatedDeliveryDate = System.DateTime.Now.AddMonths(7),
                SpokenLanguage = "English",
                ImmigrationStatus = "Citizen",
                NbChilds = 0,
                ChildsAge = "",
                MedicalCoverage = "None"
            };

            // Act
            var result = await controller.Create(request);

            // Assert
            var redirect = result as RedirectToActionResult;
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.ActionName);

            // Vérifier l’ajout dans la DB
            var optionsCheck = new DbContextOptionsBuilder<SRPFIQDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            using var contextCheck = new SRPFIQDbContext(optionsCheck);
            var count = await contextCheck.Requests.CountAsync();
            Assert.AreEqual(1, count);
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