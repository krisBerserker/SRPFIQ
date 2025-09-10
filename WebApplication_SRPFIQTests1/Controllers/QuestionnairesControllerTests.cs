using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication_SRPFIQ.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_SRPFIQ.Data;
using WebApplication_SRPFIQ.Models;

namespace WebApplication_SRPFIQ.Controllers.Tests
{
    [TestClass()]
    public class QuestionnairesControllerTests
    {
        private QuestionnairesController GetControllerWithDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<SRPFIQDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new SRPFIQDbContext(options);
            return new QuestionnairesController(context);
        }

        [TestMethod]
        public async Task Create_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var dbName = "QuestionnairesValidDb";
            var controller = GetControllerWithDb(dbName);

            var questionnaire = new Questionnaires
            {
                ID = 1,
                Name = "Questionnaire Test",
                Active = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var result = await controller.Create(questionnaire);

            // Assert redirection
            var redirect = result as RedirectToActionResult;
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.ActionName);

            // Vérifier que le questionnaire a été ajouté
            var optionsCheck = new DbContextOptionsBuilder<SRPFIQDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            using var contextCheck = new SRPFIQDbContext(optionsCheck);
            var count = await contextCheck.Questionnaires.CountAsync();
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public async Task Create_InvalidModel_ReturnsView()
        {
            // Arrange
            var dbName = "QuestionnairesInvalidDb";
            var controller = GetControllerWithDb(dbName);

            controller.ModelState.AddModelError("Name", "Required"); // simuler erreur
            var questionnaire = new Questionnaires();

            // Act
            var result = await controller.Create(questionnaire);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(questionnaire, viewResult.Model);
        }

        [TestMethod()]
        public void QuestionnairesControllerTest()
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
        public void CreateTest1()
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