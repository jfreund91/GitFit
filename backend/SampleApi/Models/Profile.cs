using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    public class Profile
    {
        //public int ProfileId { get; set; }
        /// <summary>
        /// The user's Id.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Name of the user.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The user's current weight in lbs.
        /// </summary>
        public decimal CurrentWeight { get; set; }
        /// <summary>
        /// The user's goal weight in lbs.
        /// </summary>
        public decimal GoalWeight { get; set; }
        /// <summary>
        /// The user's birthdate.
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// The user's height in feet.
        /// </summary>
        public int Feet { get; set; }
        /// <summary>
        /// The user's height in inches.
        /// </summary>
        public int Inches { get; set; }
        /// <summary>
        /// The user's level of activity.
        /// </summary>
        public string ActivityLevel { get; set; }
        /// <summary>
        /// The user's gender.
        /// </summary>
        public char Gender { get; set; }
        /// <summary>
        /// The user's timeline to lose weight.
        /// </summary>
        public string Timeline { get; set; }
        /// <summary>
        /// A URL to the user's image
        /// </summary>
        public string UserImage { get; set; }

    }
}
