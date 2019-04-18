using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public interface IFoodDAO
    {
        Food AddFoodItem(int currentUserId, Food food);

        void RemoveFoodItem(int entryId);

        IList<Food> GetLifetimeFoodEntries(int currentUser);

        IList<Food> GetFoodEntriesInRange(int currentUser, DateTime start, DateTime finish);

        void EditEntry(Food food, int currentUser);

        List<decimal> GetTimelineCaloriesByDay(int currentUser, int timeline);
    }
}
