using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    public class Profile
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public decimal CurrentWeight { get; set; }

        public decimal GoalWeight {get;set;}

        public int Age { get; set; }

        public int Height { get; set; }

        public string ActivityLevel { get; set; }

        public char Gender { get; set; }

    }
}
