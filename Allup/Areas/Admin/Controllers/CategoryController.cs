using Allup.Areas.Admin.ViewModels.Categoryes;
using Allup.DAL;
using Allup.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<GetCategoryAdminVM> categories = await _context.Categories.Include(w => w.Product).Select(x => new GetCategoryAdminVM { Id = x.Id, Name = x.Name, ProductCount = x.Product.Count }).ToListAsync();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM category)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }
            bool isExist = await _context.Categories.AnyAsync(c => c.Name.Trim().ToLower() == category.Name.Trim().ToLower());

            if (isExist)
            {
                ModelState.AddModelError("Name", "This category already exists");
                return View();
            }
            Category category1 = new()
            {
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                Name = category.Name
            };

            await _context.Categories.AddAsync(category1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            CategoryUpdateVM category = await _context.Categories.Where(x => x.Id == id).Select(x => new CategoryUpdateVM
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
                IsDeleted = x.IsDeleted
            }).FirstOrDefaultAsync();

            if (category is null) return NotFound();

            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM category)
        {
            if (id == null || id < 1) return BadRequest();

            Category existed = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            bool result = await _context.Categories.AnyAsync(c => c.Name.Trim() == category.Name.Trim() && c.Id != id);
            if (result)
            {
                ModelState.AddModelError(nameof(Category.Name), "Category already exists");
                return View();
            }
            existed.Name = category.Name;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is null) return NotFound();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Category category = await _context.Categories.Include(pr => pr.Product).FirstOrDefaultAsync(c => c.Id == id);

            if (category is null) return NotFound();

            return View(category);
        }

    }

}
