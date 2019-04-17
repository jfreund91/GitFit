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
    public class ProfileControllerTests 
        //: FitnessTrackingAppTests
    {
        protected string ConnectionString { get; } = "Server=.\\SQLEXPRESS;Database=DemoDB;Trusted_Connection=True;";

        private ProfileController NewController()
        {
            List<Claim> claims = new List<Claim>()
                {
                // Passing in as parameter username value
                new Claim(ClaimTypes.Name, "trevorwerner@hotmail.com"),

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
        public void GetUser_ReturnsCorrectUsername()
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

            profile = (Profile)result.Value;
            profile = result.Value as Profile;

            // ASSERT

            // The new profile username is correctly returned from the database
            Assert.AreEqual("Trevor", profile.Name);
        }

        [TestMethod]
        public void GetProfile_ReturnsCorrectUsername()
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

            profile = (Profile)result.Value;
            profile = result.Value as Profile;
            
            // ASSERT

            // The new profile username is correctly returned from the database
            Assert.AreEqual("Trevor", profile.Name);
        }
    }
}
