using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    /// <summary>
    /// Represents a user created meal.
    /// </summary>
    public class Meal
    {
        /// <summary>
        /// Represents the ID of a meal
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represents the name of the user created meal.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Represents the ingredients in a user created meal.
        /// </summary>
        public IList<Food> MealItems { get; set; }
    }
}
