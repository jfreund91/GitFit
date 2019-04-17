using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleApi.DAL;
using SampleApi.Models;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : GitFitController
    {
        private IMealDAO mealDao;

        public MealController(IUserDAO userDao, IMealDAO mealDao) : base(userDao)
        {
            this.mealDao = mealDao;
        }
    [Authorize]
    [HttpGet]
    [Route("api/profile")]
    public IEnumerable<Food> GetMeal([FromBody]int mealId)
    {
            return mealDao.GetMeal(mealId);
    }
    }
}