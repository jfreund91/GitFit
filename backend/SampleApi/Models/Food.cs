using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    /// <summary>
    /// Models a food item.
    /// </summary>
    public class Food
    {
        /// <summary>
        /// Represents the Id of the food item
        /// </summary>
        public int EntryId { get; set; }

        /// <summary>
        /// Represents the amount of calories in a food item.
        /// </summary>
        public int ndbno { get; set; }
     
        /// <summary>
        /// The name of the food
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Represents the amount of calories in a food item.
        /// </summary>
        public decimal Calories { get; set; }
        /// <summary>
        /// Represents the amout of fat(in grams) in a food item item.
        /// </summary>
        public decimal Fat { get; set; }

        /// <summary>
        /// Represents the amount of carbohydrates(in grams) in a food item.
        /// </summary>
        public decimal Carbs { get; set; }
        /// <summary>
        /// Represents the amount of protein(in grams) in a food item.
        /// </summary>
        public decimal Protein { get; set; }

        /// <summary>
        /// Represents the meal where the item was consumed (snack, lunch, etc.)
        /// </summary>
        public string MealType { get; set; }
        /// <summary>
        /// Represents the number of servings eaten.
        /// </summary>
        public decimal Servings { get; set; }

        /// <summary>
        /// The date the item was consumed
        /// </summary>
        public DateTime Date { get; set; }


    }
}
