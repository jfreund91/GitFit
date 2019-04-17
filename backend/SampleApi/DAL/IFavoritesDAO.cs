using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApi.Models;
namespace SampleApi.DAL
{
    public interface IFavoritesDAO
    {
        IList<Food> GetFavorites(int currentUser);

        void RemoveFavorite(int entryId);

        Food AddFavorite(Food food, int currentUser);
    }
}
