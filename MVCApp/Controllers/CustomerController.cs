using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuickKartDataAccessLayer;
using QuickKartDataAccessLayer.Models;

namespace MVCApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly QuickKartContext _context;
        QuickKartRepository repository;
        private readonly IMapper _mapper;
        public CustomerController(QuickKartContext context, IMapper mapper)
        {
            _context = context;
            repository = new QuickKartRepository(_context);
            _mapper = mapper;
        }

        public IActionResult CustomerHome()
        {
            return View();
        }
        public ActionResult GetProduct(byte? categoryId)
        {
            ViewBag.CategoryList = repository.GetCategories();
            if (categoryId != null)
            {
                ViewData["categoryId"] = categoryId;
            }
            else
            {
                categoryId = Convert.ToByte(ViewData["categoryId"]);
            }
            ViewBag.SelectedCategory = repository.GetCategories().Where(x => x.CategoryId == categoryId).Select(x => x.CategoryName).FirstOrDefault();
            if (ViewBag.SelectedCategory == null)
                ViewBag.SelectedCategory = "--Select--";
            var productList = repository.GetProducts();
            var products = new List<Models.Product>();
            foreach (var product in productList)  
            {
                products.Add(_mapper.Map<Models.Product>(product));
            }
            var filteredProducts = products.Where(model => model.CategoryId == categoryId);
            return View(filteredProducts);
        }
    }
}
