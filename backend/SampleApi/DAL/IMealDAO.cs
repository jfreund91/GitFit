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

        Food AddFoodToMeal(Food food, Meal meal, int currentUser);

        Meal CreateMeal(Meal meal, int currentUser);

        IList<Food> GetMeal(int mealId);

        void DeleteMeal(Meal meal, int currentUser);
    }
}
