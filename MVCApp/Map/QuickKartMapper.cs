using QuickKartDataAccessLayer.Models;
using AutoMapper;

namespace MVCApp.Map
{
    public class QuickKartMapper : Profile
    {
        public QuickKartMapper()
        {
            // Map Products
            CreateMap<Products, Models.Product>();
            CreateMap<Models.Product, Products>();

            // Map Category
            CreateMap<Categories, Models.Category>();
            CreateMap<Models.Category, Categories>();

            // Map Purchase
            CreateMap<PurchaseDetails, Models.Purchase>();
            CreateMap<Models.Purchase, PurchaseDetails>();

        }
    }
}
