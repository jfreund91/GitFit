using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Models;
using SampleApi.DAL;
using Microsoft.AspNetCore.Authorization;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : GitFitController
    {
        protected IFavoritesDAO favoritesDao;

        public FavoritesController(IUserDAO userDao, IFavoritesDAO favoritesDao) : base(userDao)
        {
            this.favoritesDao = favoritesDao;
        }

        /// <summary>
        /// Gets a list of all the user's favorites.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<Food> GetFavorites()
        {
            return favoritesDao.GetFavorites(CurrentUser.Id);
        }

        /// <summary>
        /// Adds a food item into the favorites table for a user.
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public Food AddFavorite([FromBody]Food food)
        {
            return favoritesDao.AddFavorite(food, CurrentUser.Id);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult RemoveFavorite([FromBody]Food food)
        {
           favoritesDao.RemoveFavorite(food.EntryId);
            return Ok();
        }
    }
}