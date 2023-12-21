using Microsoft.VisualStudio.TestTools.UnitTesting;
using Visma_Internship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Reflection;

namespace VismaResourceManagement.Tests
{
    [TestClass]
    public class ReadWriteTests
    {
        private ReadWrite readWrite;

        [TestInitialize]
        public void SetUp()
        {
            readWrite = new ReadWrite();
        }

        [TestMethod]
        public void AddShortageTest() // Test succesfull if the Shortage is added
        {
            var newShortage = new Shortage
            {

                CreatorName = "Dovydas",
                Title = "title",
                Name = "name",
                Room = RoomType.Bathroom,
                Category = CategoryType.Other,
                Priority = 5,
                CreatedOn = DateTime.Now,
            };

            var result = readWrite.AddShortage(newShortage);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void AddShortageWithSameTitleAndLowerPriority() //Test successfull if the new Shortage is not added
        {
            var newShortage = new Shortage
            {

                CreatorName = "Dovydas",
                Title = "title",
                Name = "name",
                Room = RoomType.Bathroom,
                Category = CategoryType.Other,
                Priority = 4,
                CreatedOn = DateTime.Now,
            };

            var result = readWrite.AddShortage(newShortage);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteShortageTest_IfDeletedUnsuccesfully() //test succesfull if it fails to delete due to not having perssimision
        {
            string userName = "Lukas";
            var role = UserRole.RegularUser;
            string title = "title";
            var result = readWrite.DeleteShortage(title, userName, role);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteShortageTest_IfDeletedSuccesfully() //deletes succesfully
        {
            string userName = "Dovydas";
            var role = UserRole.RegularUser;
            string title = "title";
            var result = readWrite.DeleteShortage(title, userName, role);
            Assert.IsTrue(result);
        }
    }
}