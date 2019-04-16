using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApi.Models;

 namespace SampleApi.DAL
{
    public interface IMealDAO
    {
        void RemoveFoodFromMeal(Food food);

        Food AddFoodToMeal(Food food);

        IList<Food> GetMeal(int mealId);
    }
}
