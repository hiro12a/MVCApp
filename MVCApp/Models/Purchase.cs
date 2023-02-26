using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCApp.Models
{
    public class Purchase
    {
        // Purchase Id
        [DisplayName("Purchase Id")]
        public long PurchaseId { get; set; }

        // Email Id
        [DisplayName("Email Id")]
        public string EmailId { get; set; }


        // Product Id
        [DisplayName("Product Id")]
        public string ProductId { get; set; }


        // Quanity Purchased
        [DisplayName("Quantity Purchased")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Purchased must be greater than 0")]
        public short QuanityPurchased { get; set; }


        // Date of Purchase
        [DisplayName("Date of Purchase")]
        public DateTime DateOfPurchase { get; set; }
    }
}
