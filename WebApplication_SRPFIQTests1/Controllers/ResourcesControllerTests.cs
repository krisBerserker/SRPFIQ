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
using WebApplication_SRPFIQ.ViewModel;

namespace WebApplication_SRPFIQ.Controllers.Tests
{
    [TestClass()]
    public class ResourcesControllerTests
    {
        private ResourcesController GetControllerWithDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<SRPFIQDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new SRPFIQDbContext(options);

            // Pré-remplir quelques villes et catégories pour les SelectList
            context.ResourceCities.AddRange(
                new ResourceCities { ID = 1, Name = "City1" },
                new ResourceCities { ID = 2, Name = "City2" }
            );

            context.ResourceCategories.AddRange(
                new ResourceCategories { ID = 1, Name = "Cat1" },
                new ResourceCategories { ID = 2, Name = "Cat2" }
            );

            context.SaveChanges();
            return new ResourcesController(context);
        }

        [TestMethod]
        public async Task Create_InvalidModel_ReturnsViewWithLists()
        {
            // Arrange
            var dbName = "ResourcesInvalidDb";
            var controller = GetControllerWithDb(dbName);

            var model = new ResourceCreateViewModel
            {
                Nom = "", // invalide
                PhoneNumber = "",
                Adresse = "",
                SelectedCityId = 0,
                SelectedCategoryIds = new List<int>(),
                BusList = new List<string>(),
                BusinessHours = new List<BusinessHourInput>()
            };

            controller.ModelState.AddModelError("Nom", "Required"); // simuler ModelState invalide

            // Act
            var result = await controller.Create(model);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(model, viewResult.Model);

            // Vérifier que les listes sont chargées
            Assert.IsTrue(model.Cities.Count > 0);
            Assert.IsTrue(model.Categories.Count > 0);
        }

        [TestMethod]
        public async Task Create_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var dbName = "ResourcesValidDb";
            var controller = GetControllerWithDb(dbName);

            var model = new ResourceCreateViewModel
            {
                Nom = "Ressource1",
                PhoneNumber = "123456789",
                Adresse = "123 Street",
                SelectedCityId = 1,
                SelectedCategoryIds = new List<int> (),
                BusList = new List<string> { "Bus1", "Bus2" },
                BusinessHours = new List<BusinessHourInput>()
                
            };

            // Act
            var result = await controller.Create(model);

            // Assert
            var redirect = result as RedirectToActionResult;
            //Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.ActionName);

            // Vérifier que la ressource a été ajoutée
            var optionsCheck = new DbContextOptionsBuilder<SRPFIQDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            using var contextCheck = new SRPFIQDbContext(optionsCheck);
            Assert.AreEqual(1, contextCheck.Resources.Count());
            var res = contextCheck.Resources.Include(r => r.Resources_ResourceCategories).Include(r => r.ResourceBusinessHours).First();
            Assert.AreEqual(2, res.Resources_ResourceCategories.Count);
            Assert.AreEqual(2, res.ResourceBusinessHours.Count);
        }

        [TestMethod()]
        public void ResourcesControllerTest()
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