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
            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        // This unit test is designed to verify the AddFoodItem method is adding a new row on the user db table
        public void AddFoodItem_ShouldAddNewRowItem()
        {
            //ARRANGE
            FoodSqlDAO foodItemDAO = new FoodSqlDAO("Server=.\\SQLEXPRESS;Database=DemoDB;Trusted_Connection=True;");
            UserSqlDAO userDAO = new UserSqlDAO("Server=.\\SQLEXPRESS;Database=DemoDB;Trusted_Connection=True;");


            //ACT
            User user = new User() { Username = "Name", Password = "Password", Salt = "salt", Role = "role" };
            userDAO.CreateUser(user);
            Food item = new Food()
            {
                Calories = 1000,
                Name = "Salami",
                Fat = 20,
                Carbs = 10,
                Protein = 30,
                MealType = "Lunch",
                Servings = 3,
                Date = DateTime.Now,
                ndbno = 200
            };

            foodItemDAO.AddFoodItem(user.Id, item);
            int actual = this.GetRowCount("food_entries");

            //ASSERT
            Assert.AreEqual(2, actual);
        }


        [TestMethod]
        // This is an integration test comparing ability of adding an associated profile to a user
        public void CreateProfile_ShouldAddNewRowItem()
        {
            //ARRANGE
            ProfileSqlDAO profileDAO = new ProfileSqlDAO("Server=.\\SQLEXPRESS;Database=DemoDB;Trusted_Connection=True;");
            UserSqlDAO userDAO = new UserSqlDAO("Server=.\\SQLEXPRESS;Database=DemoDB;Trusted_Connection=True;");


            //ACT
            User user = new User() { Username = "Name", Password = "Password", Salt = "salt", Role = "role" };
            user.Id = (userDAO.CreateUser(user)).Id;

            Profile profile  = new Profile() { UserId=user.Id, Name = "Name", CurrentWeight = 100, GoalWeight=200, BirthDate=DateTime.Today, Feet=6, Inches =9, ActivityLevel="moderate", Gender='F', Timeline="Maintain", UserImage=""};
            profileDAO.CreateProfile(profile);
            int actual = this.GetRowCount("user_profiles");

            //ASSERT
            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        // This unit test is designed to verify the AddFoodItem method is adding a new row on the user db table
        public void RemoveFoodItem_ShouldReturnZeroRows()
        {
            //ARRANGE
            FoodSqlDAO foodItemDAO = new FoodSqlDAO("Server=.\\SQLEXPRESS;Database=DemoDB;Trusted_Connection=True;");
            UserSqlDAO userDAO = new UserSqlDAO("Server=.\\SQLEXPRESS;Database=DemoDB;Trusted_Connection=True;");


            //ACT
            User user = new User() { Username = "Name", Password = "Password", Salt = "salt", Role = "role" };
            userDAO.CreateUser(user);
            Food item = new Food()
            {
                Calories = 1000,
                Name = "Salami",
                Fat = 20,
                Carbs = 10,
                Protein = 30,
                MealType = "Lunch",
                Servings = 3,
                Date = DateTime.Now,
                ndbno = 200
            };

            foodItemDAO.AddFoodItem(user.Id, item);
            foodItemDAO.RemoveFoodItem(item.EntryId);
            int actual = this.GetRowCount("food_entries");

            //ASSERT
            Assert.AreEqual(1, actual);
        }

    }
}
