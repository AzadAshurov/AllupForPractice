using Microsoft.AspNetCore.Mvc;

namespace Allup.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error(string errorMessage)
        {
            return View("Error", model: errorMessage);
        }
    }
}
