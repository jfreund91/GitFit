﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.DAL;
using SampleApi.Models;

namespace SampleApi.Controllers
{
    /// <summary>
    /// Creates a new profile controller for the user.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : GitFitController
    {
        private IProfileDAO profileDao;

        /// <summary>
        /// Creates a new profile controller.
        /// </summary>
        /// <param name="userDao"></param>
        /// <param name="profileDao"></param>
        public ProfileController (IUserDAO userDao, IProfileDAO profileDao) : base(userDao)
        {
            this.profileDao = profileDao;
        }

        // GET: api/Profile
        //[Authorize]
        [HttpGet]
        public ActionResult GetProfile()
        {
            //returns to Profile.vue page values:
            //profile.UserId
            //    profile.Name
            //    profile.CurrentWeight
            //    profile.GoalWeight
            //    profile.BirthDate
            //    profile.Feet
            //    profile.Inches
            //    profile.ActivityLevel
            //    profile.Gender
            //    profile.Timeline


            Profile p = profileDao.GetProfile(CurrentUser.Id);
            //UserId Pulled from the login

            return Ok(profileDao.GetProfile(CurrentUser.Id));
        }

    }
}