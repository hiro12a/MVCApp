using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuickKartDataAccessLayer;
using QuickKartDataAccessLayer.Models;

namespace MVCApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly QuickKartContext _context;
        private readonly IMapper _mapper;
        QuickKartRepository repository;


        public ProductController(QuickKartContext context, IMapper mapper)
        {
            _context = context;
            repository = new QuickKartRepository(_context);
            _mapper = mapper;
        }

        public IActionResult ViewProduct()
        {
            var lstProducts = repository.GetProducts();
            List<Models.Product> prodObj = new List<Models.Product>();
            foreach (var product in lstProducts)
            {
                prodObj.Add(_mapper.Map<Models.Product>(product));
            }
            return View(prodObj);
        }
        public IActionResult CreateProduct()  
        {
            string productId = repository.GetNextProductId();
            ViewBag.NextProductId = productId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCreatedProduct(Models.Product id)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    status = repository.AddProduct(_mapper.Map<Products>(id));
                    if (status)
                    {
                        return RedirectToAction("ViewProduct");
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
            return View(id);
        }

        public IActionResult EditProduct(Models.Product prod)
        {
            return View(prod);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEditProduct(Models.Product prod)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    status = repository.UpdateProduct(_mapper.Map<Products>(prod));
                    if (status)
                    {
                        return RedirectToAction("ViewProduct");
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
            return View(prod);
        }

        public IActionResult DeleteProduct(Models.Product prodId)
        {
            return View(prodId);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveDeleteProduct(string id)
        {
            bool status = false;
            try
            {
                status = repository.DeleteProduct(id);
                if (status)
                {
                    return RedirectToAction("ViewProduct");
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
            return View(id);
        }

        public IActionResult ProductDetails(Models.Product id)
        {
            return View(id);
        }
    }
}
