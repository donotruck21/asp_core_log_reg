using System;
using System.ComponentModel.DataAnnotations;

namespace LogReg.Models
{
    public abstract class BaseEntity {}
    public class User : BaseEntity
    {
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLengthAttribute(8)]
        public string Password { get; set; }

        
        [Compare("Password", ErrorMessage = "Password confirmation must match Password")]
        public string PasswordConfirmation { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}