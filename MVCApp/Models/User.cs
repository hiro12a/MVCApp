using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCApp.Models
{
    public class User
    {
        // Email
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is mandatory")]
        public string EmailId { get; set; }

        // Password
        [StringLength(maximumLength: 10)]
        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is mandatory")]
        public string UserPassword { get; set; }

        // Role ID
        [ScaffoldColumn(false)] // Be there but not rendered in the view
        public byte? RoleId { get; set; }

        // Gender
        [RegularExpression("M|F", ErrorMessage = "Gender should be M or F")]
        [Required(ErrorMessage = "Gender is mandatory")]
        public string Gender { get; set; }

        // Date of Birth
        [DataType(DataType.DateTime)] // Accepts only datetime
        [DisplayName("Date of Birth")]
        [Required(ErrorMessage = "Date of Birth is mandatory")]
        public DateTime DateOfBirth { get; set; }

        // Address
        [Required(ErrorMessage = "Address is mandatory")]
        public string Address { get; set; }
    }
}
