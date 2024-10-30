using Microsoft.AspNetCore.Mvc;

namespace Sales.API.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
