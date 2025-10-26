using Microsoft.AspNetCore.Mvc;

namespace ProductManagment.WebUI.Controllers
{
    public class CustomController : Controller
    {
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
