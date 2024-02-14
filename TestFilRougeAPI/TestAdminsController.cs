using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilRougeAPI.Models;
using FilRougeAPI.Services;
using FilRougeAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Protocol.Core.Types;
using FilRougeAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using FilRougeAPI.Configs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestFilRougeAPI.UnitTests
{
    [TestClass]
    public class TestAdminsController : ControllerBase
    {

        //[ClassInitialize]
        //public void SetUp()
        //{
        //    _service = new FilRougeAPI.Services.Service(new Repository<Admin>);

        //}
        //private readonly DbContextOptions<BibliothequeDbContext> _contextOptions;

        //#region Constructor
        //public TestAdminsController()
        //{
        //    _contextOptions = new DbContextOptionsBuilder<BibliothequeDbContext>()
        //        .UseInMemoryDatabase("BibliothequeControllerTest")
        //        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        //        .Options;

        //    using var context = new BibliothequeDbContext(_contextOptions);

        //    context.Database.EnsureDeleted();
        //    context.Database.EnsureCreated();
        //    context.AddRange(
        //    new Blog { Name = "Blog1", Url = "http://blog1.com" },
        //        new Blog { Name = "Blog2", Url = "http://blog2.com" });

        //    context.SaveChanges();
        //}
        //#endregion

        // Test contre la base de données de "production" 




        private readonly IService<Admin> _service;



        // extend adminc ? use adminc ? 
        public TestAdminsController(IService<Admin> service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<Admin> GetAdmin(int id)
        {
            var admin = _service.GetById(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        [TestMethod]
        public void GetAdmin1_ShouldReturnAdmin1()
        {
            //var testAdmins = _service.GetAll();
            //var controller = new AdminsController(_service);
            // Arrange
            var repositoryMock = new Mock<IRepository<Admin>>();
            var serviceMock = new Service<Admin>(repositoryMock.Object);

            repositoryMock
                .Setup(r => r.GetById(1))
            .Returns(new Admin { Id = 1, Nom = "Parmesan" });


            var controller = new TestAdminsController(serviceMock);

            // Act
            var result = controller.GetAdmin(1).Value;

            //var result = controller.GetAdmins() as List<Admin>;
            //(result ?? new List<Admin>() ).Count
            //Assert.AreEqual(testAdmins.Count<Admin>(), result.Count  );
            Assert.AreEqual(1, result.Id );
        }
    }
}
