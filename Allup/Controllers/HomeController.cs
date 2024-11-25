using Allup.DAL;
using Allup.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Slides = _context.Slides.OrderBy(s => s.Order).ToList(),
                Products = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .ToList()
            };


            return View(homeVM);
        }
    }
}
