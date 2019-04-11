using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public interface IFoodDAO
    {
        void AddFoodItem(int currentUserId, Food food);

        void RemoveFoodItem(int entryId);
    }
}
