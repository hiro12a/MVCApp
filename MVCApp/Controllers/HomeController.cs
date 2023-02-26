using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using QuickKartDataAccessLayer;
using QuickKartDataAccessLayer.Models;
using System.Diagnostics;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuickKartContext _context;
        QuickKartRepository repository;
        public HomeController(ILogger<HomeController> logger, QuickKartContext context)
        {
            _logger = logger;
            _context = context;
            repository = new QuickKartRepository(_context);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveRegisterUser(Models.User userObj)
        {
            if (ModelState.IsValid)
            {
                var returnValue = repository.RegisterUser(userObj.EmailId, userObj.UserPassword, userObj.Gender,
                    userObj.DateOfBirth, userObj.Address);
                if (returnValue)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return View("Error");
                }
            }
            return View(userObj);
        }

        public IActionResult CheckRole(IFormCollection form)
        {
            string userName = form["name"];
            string password = form["password"];
            string checkBox = form["RememberMe"];

            // Save UserName and Password for next login
            if(checkBox == "on")
            {
                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddDays(1); // Expires in 1 day
                Response.Cookies.Append("UserId", userName, cookie);
                Response.Cookies.Append("Password", password, cookie);
            }

            string user = userName.Split("@")[0];
            byte? roleId = repository.ValidateCredentials(userName, password);
            if(roleId == 1)
            {
                HttpContext.Session.SetString("username", user);
                return RedirectToAction("AdminHome","Admin");
            }
            else if(roleId == 2)
            {
                ViewData["UserId"] = userName;
                HttpContext.Session.SetString("username", user);
                return RedirectToAction("CustomerHome", "Customer");
            }
            else
            {
                ModelState.AddModelError("Error", "Incorrect Email or Password");
            }
            return View("Login");
        }
        public JsonResult GetCoupons()
        {
            Random random = new Random();
            Dictionary<string, string> data = new Dictionary<string, string>();
            string[] value = new string[5];
            string[] key = { "Arts", "Electronics", "Fashion", "Home", "Toys" };
            for(int i = 0; i < 5; i++)
            {
                string number = "RUSH" + random.Next(1, 10).ToString() + random.Next(1, 10).ToString() + random.Next(1, 10).ToString();
                value[i] = number;
            }
            for (int i = 0; i < 5; i++){
                data.Add(key[i], value[i]);
            }
            return Json(data);
        }
        public IActionResult Terms()
        {
            return View();
        }
        public FileResult DownloadFile()
        {
            string fileName = "TermsAndConditions.pdf";
            string filePaths = Path.Combine("Downloads/") + fileName;
            byte[] bytes = System.IO.File.ReadAllBytes(filePaths);

            return File(bytes, "application/octet-stream", fileName);
        }

        public IActionResult Contact()
        {
            return View();
        }
        public RedirectResult FAQ()
        {
            return Redirect("/Home/Contact");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}