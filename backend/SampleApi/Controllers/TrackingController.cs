using System;
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
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : GitFitController
    {
        protected IProfileDAO profileDao;

        protected IFoodDAO foodDao;

        public TrackingController (IUserDAO userDao, IProfileDAO profileDao, IFoodDAO foodDao):base (userDao)
        {
            this.profileDao = profileDao;
            this.foodDao = foodDao;
        }

        [HttpPost]
        public void AddFoodItem([FromBody] Food food)
        {
            foodDao.AddFoodItem(CurrentUser.Id, food);
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Food> GetLifetimeFoodEntries()
        {
            return foodDao.GetLifetimeFoodEntries(CurrentUser.Id);
        }
     
    }
}