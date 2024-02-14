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
using Xunit;

namespace TestFilRougeAPIxUnit.UnitTests
{
    
    public class TestAdminsController : ControllerBase
    {


        private readonly Mock<IService<Admin>> _service;



        public TestAdminsController()
        {
            _service = new Mock<IService<Admin>>();
        }


        [Fact]
        public void GetAllAdmins_ShouldReturnAllAdmins()
        {
            //arrange
            var adminList = GetAdminsData().AsQueryable();
            _service.Setup(x => x.GetAll())
                .Returns(adminList);
            var adminsController = new AdminsController(_service.Object);
            //act
            var adminResult = adminsController.GetAdmins();
            //assert
            Xunit.Assert.NotNull(adminResult);
            Xunit.Assert.Equal(GetAdminsData().Count(), adminResult.Count());
            Xunit.Assert.Equal(GetAdminsData().ToString(), adminResult.ToString());
            Xunit.Assert.True(adminList.Equals(adminResult));
        }

        [Fact]
        public void GetAdminById_ShouldReturnAdminWithId()
        {
            //arrange
            var adminList = GetAdminsData().AsQueryable();
            _service.Setup(x => x.GetById(1))
                .Returns(adminList.FirstOrDefault((a => a.Id == 1)));
            var adminController = new AdminsController(_service.Object);
            //act
            var adminResult = adminController.GetAdmin(1);
            //assert
            Xunit.Assert.NotNull(adminResult);
            Xunit.Assert.Equal(adminList.FirstOrDefault(a => a.Id==1).Id, adminResult.Value.Id);
            Xunit.Assert.True(adminList.FirstOrDefault(a => a.Id == 1).Id == adminResult.Value.Id);
        }

        [Fact]
        public void AddAdmin_ShouldAddAdmin()
        {
            //arrange
            var adminList = GetAdminsData();
            _service.Setup(x => x.Add(adminList[2]))
                .Returns(adminList[2]);
            var adminController = new AdminsController(_service.Object);
            //act
            var adminResult = adminController.PostAdmin(adminList[2]);
            //assert
            Xunit.Assert.NotNull(adminResult);
            //Xunit.Assert.Equal(adminList[2], adminResult.Value);
            //Xunit.Assert.True(adminList[2] == adminResult.Value);
        }

        private List<Admin> GetAdminsData()
        {
            List<Admin> AdminData = new List<Admin>

        {
            new Admin
            {
                Id = 1,
                Nom = "Cheddar",
                Prenom = "Jo",
                Email = "jo.cheddar@nasa.lu",
                MotDePasse = "123",
                Telephone = "123"
            },
             new Admin
            {
                Id = 2,
                Nom = "Cheddar",
                Prenom = "Ja",
                Email = "ja.cheddar@nasa.lu",
                MotDePasse = "123",
                Telephone = "123"
            },
             new Admin
            {
                Id = 3,
                Nom = "Gouda",
                Prenom = "Mike",
                Email = "mike.gouda@sigma.fi",
                MotDePasse = "123",
                Telephone = "123"
            },
        };
            return AdminData;
        }
    }
}
