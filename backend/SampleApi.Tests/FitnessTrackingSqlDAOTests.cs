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
        // This unit test is designed to verify the CreateUser method is adding a new row on the user db table
        public void CreateUser_ShouldAddNewRowItem()
        {
            //ARRANGE
            UserSqlDAO userDAO = new UserSqlDAO("Server=.\\SQLEXPRESS;Database=DemoDB;Trusted_Connection=True;");

            //ACT
            User user = new User() { Username = "Name", Password = "Password", Salt = "salt", Role = "role"};
            userDAO.CreateUser(user);
            int actual = this.GetRowCount("users");

            //ASSERT
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        // This test is our first integration test comparing ability of adding an associated profile to a user
        public void CreateProfile_ShouldAddNewRowItem()
        {
            //ARRANGE
            ProfileSqlDAO profileDAO = new ProfileSqlDAO("Server=.\\SQLEXPRESS;Database=DemoDB;Trusted_Connection=True;");
            UserSqlDAO userDAO = new UserSqlDAO("Server=.\\SQLEXPRESS;Database=DemoDB;Trusted_Connection=True;");


            //ACT
            User user = new User() { Username = "Name", Password = "Password", Salt = "salt", Role = "role" };
            userDAO.CreateUser(user);

            Profile profile  = new Profile() { UserId=1, Name = "Name", CurrentWeight = 100, GoalWeight=200, BirthDate=DateTime.Today, Height=60, ActivityLevel="moderate", Gender='F'};
            profileDAO.CreateProfile(profile);
            int actual = this.GetRowCount("user_profiles");

            //ASSERT
            Assert.AreEqual(1, actual);
        }

    }
}
