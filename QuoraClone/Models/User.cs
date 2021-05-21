
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoraClone.Models
{
    public class User
    {
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string PasswordHash { get; set; }
    
    }
    
}