using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    public class Profile
    {
        public int UserId { get; set; }

        public decimal CurrentWeight { get; set; }

        public decimal GoalWeight {get;set;}

        public int age { get; set; }

        public int height { get; set; }

        public string ActivityLevel { get; set; }

    }
}
