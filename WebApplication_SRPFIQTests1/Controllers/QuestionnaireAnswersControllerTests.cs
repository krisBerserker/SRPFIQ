using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication_SRPFIQ.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_SRPFIQ.Data;
using WebApplication_SRPFIQ.Models;

namespace WebApplication_SRPFIQ.Controllers.Tests
{
    [TestClass()]
    public class QuestionnaireAnswersControllerTests
    {
        private QuestionnaireAnswersController GetControllerWithDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<SRPFIQDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new SRPFIQDbContext(options);

            // Pré-remplir les données nécessaires pour les SelectList
            context.Users.Add(new Users { ID = 1, FirstName = "John", LastName = "Doe", UserName = "jdoe", PasswordHash = "pwd", Active = true });
            context.Requests.Add(new Requests { ID = 1, FolioNumber = "F123", FullName = "Alice Smith", CreatedDate = DateTime.Now, NbPregnancy = "1", IsMonoparental = false, EstimatedDeliveryDate = DateTime.Now.AddMonths(7), SpokenLanguage = "English", ImmigrationStatus = "Citizen", NbChilds = 0, ChildsAge = "", MedicalCoverage = "None" });
            context.Questionnaires.Add(new Questionnaires { ID = 1, Name = "Q1", Active = true, CreatedDate = DateTime.Now });

            context.SaveChanges();
            return new QuestionnaireAnswersController(context);
        }

        [TestMethod]
        public async Task Create_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var dbName = "QuestionnaireAnswersValidDb";
            var controller = GetControllerWithDb(dbName);

            var answer = new QuestionnaireAnswers
            {
                ID = 1,
                IdQuestionnaire = 1,
                IdRequest = 1,
                IdUser = 1,
                IdStatuts = 1,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };

            // Act
            var result = await controller.Create(answer);

            // Assert redirection
            var redirect = result as RedirectToActionResult;
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.ActionName);

            // Vérifier que l'entité a été ajoutée
            var optionsCheck = new DbContextOptionsBuilder<SRPFIQDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            using var contextCheck = new SRPFIQDbContext(optionsCheck);
            var count = await contextCheck.QuestionnaireAnswers.CountAsync();
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public async Task Create_InvalidModel_ReturnsViewWithSelectList()
        {
            // Arrange
            var dbName = "QuestionnaireAnswersInvalidDb";
            var controller = GetControllerWithDb(dbName);

            controller.ModelState.AddModelError("IdQuestionnaire", "Required"); // Simuler erreur
            var answer = new QuestionnaireAnswers();

            // Act
            var result = await controller.Create(answer);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(answer, viewResult.Model);

            // Vérifier les SelectList
            Assert.IsTrue(viewResult.ViewData.ContainsKey("IdQuestionnaire"));
            Assert.IsInstanceOfType(viewResult.ViewData["IdQuestionnaire"], typeof(SelectList));

            Assert.IsTrue(viewResult.ViewData.ContainsKey("IdRequest"));
            Assert.IsInstanceOfType(viewResult.ViewData["IdRequest"], typeof(SelectList));

            Assert.IsTrue(viewResult.ViewData.ContainsKey("IdUser"));
            Assert.IsInstanceOfType(viewResult.ViewData["IdUser"], typeof(SelectList));
        }

        [TestMethod()]
        public void QuestionnaireAnswersControllerTest()
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