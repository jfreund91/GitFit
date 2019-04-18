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
        [Route("/api/[controller]/addfood")]
        [Authorize]
        [HttpPost]
        public void AddFoodItem([FromBody] Food food)
        {
            foodDao.AddFoodItem(CurrentUser.Id, food);
        }
        /// <summary>
        /// Gets the user's daily log of food entries.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<Food> GetTodaysFoodEntries()
        {
            return foodDao.GetFoodEntriesInRange(CurrentUser.Id, DateTime.Today, DateTime.Today);
        }

        /// <summary>
        /// Gets the user's daily log of food entries.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("/api/[controller]/dailyfood")]
        public IEnumerable<Food> GetFoodEntriesByDay([FromBody] DateTime date)
        {
            return foodDao.GetFoodEntriesInRange(CurrentUser.Id, date, date);
        }

        /// <summary>
        /// Gets the user's lifetime entries.
        /// </summary>
        /// <returns></returns>
        [Route("/api/[controller]/lifetime")]
        [HttpGet]
        [Authorize]
        public IEnumerable<Food> GetLifeTimeEntries()
        {
            return foodDao.GetLifetimeFoodEntries(CurrentUser.Id);
        }

        /// <summary>
        /// Gets the user's food entries dating back 1 year from current date.
        /// </summary>
        /// <returns></returns>
        [Route("/api/[controller]/year")]
        [HttpGet("year")]
        [Authorize]
        public IEnumerable<Food> GetYearlyFoodEntries()
        {
            return foodDao.GetFoodEntriesInRange(CurrentUser.Id, DateTime.Today.AddYears(-1), DateTime.Today);
        }

        /// <summary>
        /// Gets the user's entries dating back 1 month from current date.
        /// </summary>
        /// <returns></returns>
        [Route("/api/[controller]/month")]
        [HttpGet]
        [Authorize]
        public IEnumerable<Food> GetMonthlyEntries()
        {
            return foodDao.GetFoodEntriesInRange(CurrentUser.Id, DateTime.Today.AddDays(-30), DateTime.Today);
        }

        /// <summary>
        /// Gets the user's entries dating back 1 week.
        /// </summary>
        /// <returns></returns>
        [Route("/api/[controller]/week")]
        [HttpGet]
        [Authorize]
        public IEnumerable<Food> GetWeeklyEntries()
        {
            return foodDao.GetFoodEntriesInRange(CurrentUser.Id, DateTime.Today.AddDays(-7), DateTime.Today);
        }

        /// <summary>
        /// Deletes an entry from the user's food log.
        /// </summary>
        [Route("/api/[controller]/remove")]
        [HttpDelete("remove")]
        [Authorize]
        public IActionResult DeleteEntry([FromBody]int foodId)
        {
            foodDao.RemoveFoodItem(foodId);
            return Ok();
        }

        /// <summary>
        /// Updates a user's food entry.
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        [Route("/api/[controller]/updatefood")]
        [HttpPatch("update")]
        [Authorize]
        public IActionResult UpdateEntry([FromBody]Food food)
        {
            foodDao.EditEntry(food, CurrentUser.Id);
            return Ok();
        }

        [Route("/api/[controller]/yearlycals")]
        [HttpGet]
        [Authorize]
        public IEnumerable<decimal> GetYearlyCalories()
        {
            return foodDao.GetTimelineCaloriesByDay(CurrentUser.Id, 365);
        }

        [Route("/api/[controller]/monthlycals")]
        [HttpGet]
        [Authorize]
        public IEnumerable<decimal> GetMonthlyCalories()
        {
            return foodDao.GetTimelineCaloriesByDay(CurrentUser.Id, 30);
        }
        [Route("/api/[controller]/weeklycals")]
        [HttpGet]
        [Authorize]
        public IEnumerable<decimal> GetWeeklyCalories()
        {
            return foodDao.GetTimelineCaloriesByDay(CurrentUser.Id, 7);
        }
    }
}