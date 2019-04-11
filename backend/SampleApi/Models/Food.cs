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
        public decimal Carbohydrates { get; set; }
        /// <summary>
        /// Represents the amount of protein(in grams) in a food item.
        /// </summary>
        public decimal Protein { get; set; }

        /// <summary>
        /// Represents the number of servings eaten.
        /// </summary>
        public int Servings { get; set; }

        /// <summary>
        /// Represents the number of grams per serving.
        /// </summary>
        public decimal ServingSize { get; set; }
    }
}
