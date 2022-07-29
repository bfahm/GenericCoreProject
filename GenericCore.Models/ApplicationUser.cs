using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace GenericCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        public DateTime Birthday { get; set; }

        public string CurrentAddress { get; set; }
    }
}
