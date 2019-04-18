using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApi.Controllers;
using SampleApi.DAL;
using SampleApi.Models;
using SampleApi.Tests.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SampleApi.Tests
{
    [TestClass]
    public class TrackingControllerTests : FitnessTrackingAppTests
    {
        private TrackingController NewController()
        {
            List<Claim> claims = new List<Claim>()
                {
                // Passing in username as parameter
                new Claim(ClaimTypes.Name, "Ted"),
                };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "TestAuthType");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            UserSqlDAO userDao = new UserSqlDAO(ConnectionString);
            ProfileSqlDAO profileDao = new ProfileSqlDAO(ConnectionString);
            FoodSqlDAO foodDao = new FoodSqlDAO(ConnectionString);

            TrackingController controller = new TrackingController(userDao, profileDao, foodDao)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = claimsPrincipal
                    }
                }
            };
            return controller;
        }

        [TestMethod]
        public void GetTodaysFoodEntries_VerifyCorrectNutrientTotals()
        {
            // ARRANGE
            // Create the controller mocked with identity: 
            TrackingController controller = NewController();

            // ACT

            // Call the Create method on the controller to add the post to the db
            IEnumerable<Food> foods = controller.GetTodaysFoodEntries();

            // ASSERT

            // The new profile username is correctly returned from the database
            Assert.AreEqual(1, foods.Count());
            Food food = foods.First();
            Assert.AreEqual(20, food.Protein);
            Assert.AreEqual(1, food.Carbs);
            Assert.AreEqual(100, food.Fat);

        }


    }
}
