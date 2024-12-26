using System.ComponentModel.DataAnnotations;

namespace UserJwtSolution.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; } // Primary Key, Auto-Incremented

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } // First Name (Required, Max Length 50)

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } // Last Name (Required, Max Length 50)

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } // Email Address (Required, Must be Valid Format)

        [Required]
        [MinLength(8)]
        public string Password { get; set; } // Password (Required, Min Length 8)

        [Required]
        public bool IsActive { get; set; } = true; // Is Active (Default True)

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
