using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApi.Controllers;
using SampleApi.Models;
using SampleApi.Tests.DAL;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SampleApi.Tests
{
    //[TestClass]
    //public class FitnessTrackingControllerTests : FitnessTrackingAppTests
    //{
    //    [TestMethod]
    //    public void CreateProfile()
    //    {

    //        // ARRANGE
    //        // Create the controller mocked with identity: ProfileController controller = NewController();

    //        private ProfileController NewController()
    //        {
    //            List<Claim> claims = new List<Claim>()
    //            {
    //            new Claim(ClaimTypes.Name, "user"),
    //            new Claim(ClaimTypes.NameIdentifier, "userId"),
    //            };
    //            ClaimsIdentity identity = new ClaimsIdentity(claims, "TestAuthType");
    //            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

    //            ProfileController controller = new ProfileController(mockUserDao.Object, mockProfileDao.Object);
    //            controller.ControllerContext = new ControllerContext()
    //            {
    //                HttpContext = new DefaultHttpContext()
    //                {
    //                    User = claimsPrincipal
    //                }
    //            };
    //            return controller;
    //        }
    //        // ACT
    //        // Create a new profile object
    //        Profile profile = new Profile();

    //        // Call the Create method on the controller to add the post to the db
    //        ActionResult<Profile> viewresult = controller.CreateProfile(profile);
    //        ObjectResult result = viewresult.Result as ObjectResult;
    //        profile = result.Value as ;
    //        // Assert

    //        // The new post has an id assigned from the database
    //        Assert.AreEqual(profile.UserId, 101);

    //        // The user id of the current user was assigned to the post
    //        Assert.AreEqual(profile.UserId, 1);

    //    }
    //}
}
