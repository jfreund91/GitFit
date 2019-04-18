using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models.Account
{
    /// <summary>
    /// Represents an authentication model request.
    /// </summary>
    public class UpdatePasswordModel
    {
        /// <summary>
        /// The user's username.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// The user's new password.
        /// </summary>
        [Required]
        public string NewPassword { get; set; }
    }
}
