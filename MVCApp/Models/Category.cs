using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCApp.Models
{
    public class Category
    {
        [ScaffoldColumn(false)]
        [Key]
        public byte CategoryId { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MinLength(3, ErrorMessage = "Minimum length is 3 characters")]
        public string CategoryName { get; set; }
    }
}
