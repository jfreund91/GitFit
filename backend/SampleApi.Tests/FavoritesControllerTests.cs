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
    public class FavoritesControllerTests : FitnessTrackingAppTests
    {
        private FavoritesController NewController()
        {
            List<Claim> claims = new List<Claim>()
                {
                // Passing in username as parameter
                new Claim(ClaimTypes.Name, "Ted"),
                };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "TestAuthType");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            UserSqlDAO userDao = new UserSqlDAO(ConnectionString);
            FavoritesSqlDAO favoritesDao = new FavoritesSqlDAO(ConnectionString);

            FavoritesController controller = new FavoritesController(userDao, favoritesDao)
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
        public void GetFavorites_ReturnsCorrectFoodItems()
        {
            // ARRANGE
            // Create the controller mocked with identity: 
            FavoritesController controller = NewController();

            // ACT

            IEnumerable<Food> foods = controller.GetFavorites();

            // ASSERT

            Food food = foods.First();
            Assert.AreEqual(600, food.Calories);
        }

    }
}
