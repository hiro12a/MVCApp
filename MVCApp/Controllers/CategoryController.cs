using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuickKartDataAccessLayer;
using QuickKartDataAccessLayer.Models;

namespace MVCApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly QuickKartContext _context;
        private readonly IMapper _mapper;
        QuickKartRepository repository;
        public CategoryController(QuickKartContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            repository = new QuickKartRepository(_context);
        }

        public IActionResult ViewCategory()
        {
            var lstCategory = repository.GetCategories();
            List<Models.Category> catObj = new List<Models.Category>();
            foreach(var category in lstCategory)
            {
                catObj.Add(_mapper.Map<Models.Category>(category));
            }
            return View(catObj);
        }
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCreateCategory(Models.Category id)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    status = repository.AddCategory(_mapper.Map<Categories>(id));
                    if (status)
                    {
                        return RedirectToAction("ViewCategory");
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

        public IActionResult EditCategory(Models.Category id)
        {
            return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEditCategory(Models.Category id)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    status = repository.UpdateCategory(_mapper.Map<Categories>(id));
                    if (status)
                    {
                        return RedirectToAction("ViewCategory");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                catch(Exception)
                {
                    return View("Error");
                }
            }
            return View(id);
        }

        public IActionResult DeleteCategory(Models.Category id)
        {
            return View(id);
        }

        [HttpPost]
        public IActionResult SaveDeleteCategory(byte id)
        {
            bool status = false;
            var obj = _context.Categories.Find(id);
            try
            {
                status = repository.DeleteCategory(obj);
                if (status)
                {
                    return RedirectToAction("ViewCategory");
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

    }
}
