using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApi.DAL;
using SampleApi.Models;
using SampleApi.Tests.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApi.Tests
{
    [TestClass]
    public class FitnessTrackingSqlDAOTests: FitnessTrackingAppTests
    {
        [TestMethod]
        public void CreateUser_ShouldAddNewRowItem()
        {
            //ARRANGE
            UserSqlDAO userDAO = new UserSqlDAO("Server=.\\SQLEXPRESS;Database=DemoDB;Trusted_Connection=True;");

            //ACT
            User user = new User() { Username = "Name", Password = "Password", Salt = "salt", Role = "role"};
            userDAO.CreateUser(user);
            int actual = this.GetRowCount("user_profile");

            //ASSERT
            Assert.AreEqual(1, actual);
        }
    }
}
