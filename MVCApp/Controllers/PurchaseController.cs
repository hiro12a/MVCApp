using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using QuickKartDataAccessLayer;
using QuickKartDataAccessLayer.Models;

namespace MVCApp.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly QuickKartContext _context;
        private readonly IMapper _mapper;
        QuickKartRepository repository;
        
        public PurchaseController(QuickKartContext context, IMapper mapper)
        {
            _context = context;
            repository = new QuickKartRepository(_context);
            _mapper = mapper;
        }

        public IActionResult PurchaseProduct(Product product)
        {
            Purchase purchaseObj = new Purchase();
            purchaseObj.EmailId = HttpContext.Session.GetString("username");
            purchaseObj.ProductId = product.ProductId;
            purchaseObj.DateOfPurchase = DateTime.Now;
            TempData["ProductName"] = product.ProductName;
            return View(purchaseObj);
        }
        public IActionResult SavePurchaseProduct(Models.Purchase purchase)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    ViewData["QuantityPurchased"] = purchase.QuanityPurchased;
                    status = repository.PurchaseProduct(_mapper.Map<PurchaseDetails>(purchase));
                    if (status)
                    {
                        return View("Success");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View(purchase);
        }
    }
}
