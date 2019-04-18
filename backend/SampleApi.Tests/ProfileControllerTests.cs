using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApi.Controllers;
using SampleApi.DAL;
using SampleApi.Models;
using SampleApi.Tests.DAL;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SampleApi.Tests
{
    [TestClass]
    public class ProfileControllerTests : FitnessTrackingAppTests
    {
        private ProfileController NewController()
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

            ProfileController controller = new ProfileController(userDao, profileDao)
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
        public void GetProfile_ReturnsCorrectName()
        {
            // ARRANGE
            // Create the controller mocked with identity: 
            ProfileController controller = NewController();

            // ACT
            // Create a new profile object
            Profile profile = new Profile();

            // Call the Create method on the controller to add the post to the db
            ActionResult<Profile> viewresult = controller.GetProfile();
            // Cast ActionResult to an ObjectResult
            ObjectResult result = viewresult.Result as ObjectResult;

            profile = result.Value as Profile;

            //Alternative syntax for calling profile
            //profile = (Profile)result.Value;

            // ASSERT

            // The new profile username is correctly returned from the database
            Assert.AreEqual("TedKennedy", profile.Name);
        }

        [TestMethod]
        public void GetProfile_ReturnsCorrectGoalWeight()
        {
            // ARRANGE
            // Create the controller mocked with identity: 
            ProfileController controller = NewController();

            // ACT
            // Create a new profile object
            Profile profile = new Profile();

            // Call the Create method on the controller to add the post to the db
            ActionResult<Profile> viewresult = controller.GetProfile();
            // Cast ActionResult to an ObjectResult
            ObjectResult result = viewresult.Result as ObjectResult;

            profile = result.Value as Profile;

            // ASSERT

            // The new profile username is correctly returned from the database
            Assert.AreEqual(200, profile.GoalWeight);
        }

    }
}
