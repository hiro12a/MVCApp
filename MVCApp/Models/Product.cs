using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCApp.Models
{
    public class Product
    {
        // Product ID
        [Required(ErrorMessage = "Product Id is Mandatory")]
        [DisplayName("Product Id")]
        public string ProductId { get; set; }
        // Category ID
        [Required(ErrorMessage = "Category Id is Mandatory")]
        [DisplayName("Category Id")]
        public byte? CategoryId { get; set; }

        // Price
        [Required(ErrorMessage = "Price is Mandatory")]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        // Product Name
        [Required(ErrorMessage = "Product Name is Mandatory")]
        [DisplayName("Product Name")]
        [MinLength(3, ErrorMessage = "Product Name should be a minimum of 3 characters")]
        public string ProductName { get; set; }

        // Quantity Available
        [Required(ErrorMessage = "Quantity Available is Mandatory")]
        [DisplayName("Quantity Available")]
        public int QuantityAvailable { get; set; }
    }
}
