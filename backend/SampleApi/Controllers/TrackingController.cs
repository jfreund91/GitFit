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
        [Authorize]
        [HttpPost]
        public void AddFoodItem([FromBody] Food food)
        {
            foodDao.AddFoodItem(CurrentUser.Id, food);
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Food> GetTodaysFoodEntries()
        {
            return foodDao.GetFoodEntriesInRange(CurrentUser.Id, DateTime.Today, DateTime.Today);
        }
        
        /// <summary>
        /// Gets the user's food entries dating back 1 year from current date.
        /// </summary>
        /// <returns></returns>
        [HttpGet("year")]
        [Authorize]
        public IEnumerable<Food> GetYearlyFoodEntries()
        {
            return foodDao.GetFoodEntriesInRange(CurrentUser.Id, DateTime.Today.AddYears(-1), DateTime.Today);
        }

        /// <summary>
        /// Deletes an entry from the user's food log.
        /// </summary>
        [HttpDelete("remove")]
        [Authorize]
        public IActionResult DeleteEntry([FromBody]Food food)
        {
            foodDao.RemoveFoodItem(food.EntryId);
            return Ok();
        }
     
        [HttpPatch("update")]
        [Authorize]
        public IActionResult UpdateEntry([FromBody]Food food)
        {
            foodDao.EditEntry(food, CurrentUser.Id);
            return Ok();
        }
    }
}