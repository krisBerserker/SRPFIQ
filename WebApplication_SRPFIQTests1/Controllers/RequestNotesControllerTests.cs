using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication_SRPFIQ.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication_SRPFIQ.Data;
using WebApplication_SRPFIQ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication_SRPFIQ.Controllers.Tests
{
    [TestClass()]
    public class RequestNotesControllerTests
    {
        private RequestNotesController GetControllerWithDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<SRPFIQDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new SRPFIQDbContext(options);

            // Ajouter un utilisateur et une requête pour les SelectList
            context.Users.Add(new Users { ID = 1, FirstName = "John", LastName = "Doe", UserName = "jdoe", PasswordHash = "pwd", Active = true });
            context.Requests.Add(new Requests { ID = 1, FolioNumber = "F123", FullName = "Alice Smith", CreatedDate = DateTime.Now, NbPregnancy = "1", IsMonoparental = false, EstimatedDeliveryDate = DateTime.Now.AddMonths(7), SpokenLanguage = "English", ImmigrationStatus = "Citizen", NbChilds = 0, ChildsAge = "", MedicalCoverage = "None" });
            context.SaveChanges();

            return new RequestNotesController(context);
        }

        [TestMethod]
        public async Task Create_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var dbName = "RequestNotesValidDb";
            var controller = GetControllerWithDb(dbName);

            var note = new RequestNotes
            {
                ID = 1,
                IdRequest = 1,
                IdUser = 1,
                Note = "Test note",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };

            // Act
            var result = await controller.Create(note);

            // Assert redirection
            var redirect = result as RedirectToActionResult;
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.ActionName);

            // Vérifier que la note a été ajoutée
            var optionsCheck = new DbContextOptionsBuilder<SRPFIQDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            using var contextCheck = new SRPFIQDbContext(optionsCheck);
            var count = await contextCheck.RequestNotes.CountAsync();
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public async Task Create_InvalidModel_ReturnsViewWithSelectList()
        {
            // Arrange
            var dbName = "RequestNotesInvalidDb";
            var controller = GetControllerWithDb(dbName);

            controller.ModelState.AddModelError("Note", "Required"); // Simuler erreur
            var note = new RequestNotes();

            // Act
            var result = await controller.Create(note);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(note, viewResult.Model);

            // Vérifier que les SelectList pour IdUser et IdRequest sont créées
            Assert.IsTrue(viewResult.ViewData.ContainsKey("IdRequest"));
            Assert.IsInstanceOfType(viewResult.ViewData["IdRequest"], typeof(SelectList));

            Assert.IsTrue(viewResult.ViewData.ContainsKey("IdUser"));
            Assert.IsInstanceOfType(viewResult.ViewData["IdUser"], typeof(SelectList));
        }

        [TestMethod()]
        public void RequestNotesControllerTest()
        {
            Assert.Fail();
        }
    }
}