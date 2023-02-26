using Microsoft.AspNetCore.Mvc;

namespace MVCApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminHome()
        {
            return View();
        }
    }
}
