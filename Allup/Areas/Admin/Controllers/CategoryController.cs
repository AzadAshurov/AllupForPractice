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
            List<GetCategoryAdminVM> CategoriesList = await _context.Categories.Include(a => a.ProductCategories).Select(x => new GetCategoryAdminVM { Id = x.Id, Name = x.Name, ProductCount = x.ProductCategories.Count }).ToListAsync();
            return View(CategoriesList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int? id, GetCategoryAdminVM CategoryVm)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }
            bool isExist = await _context.Categories.AnyAsync(c => c.Name.Trim().ToLower() == CategoryVm.Name.Trim().ToLower());

            if (isExist)
            {
                ModelState.AddModelError("Name", "This Category already exists");
                return View();
            }
            Category Category = new Category
            {
                CreatedAt = DateTime.Now,
                Name = CategoryVm.Name,
                IsDeleted = false
            };
            await _context.Categories.AddAsync(Category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View();
            }
            Category Category = await _context.Categories.FirstOrDefaultAsync(s => s.Id == id);
            if (Category is null) return NotFound();
            CategoryUpdateVM CategoryVM = new CategoryUpdateVM
            {
                Name = Category.Name
            };
            return View(CategoryVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM CategoryVM)
        {
            if (id == null || id < 1) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View();
            }
            Category Category = await _context.Categories.FirstOrDefaultAsync(s => s.Id == id);
            if (Category is null) return NotFound();
            if (_context.Categories.Any(x => x.Name == CategoryVM.Name && x.Id != CategoryVM.Id))
            {
                ModelState.AddModelError(nameof(CategoryUpdateVM.Name), "Category must be unique");
                return View(CategoryVM);
            }
            Category.Name = CategoryVM.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Category Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (Category is null) return NotFound();
            _context.Categories.Remove(Category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Category Category = await _context.Categories.Include(t => t.ProductCategories).FirstOrDefaultAsync(s => s.Id == id);
            if (Category is null) return NotFound();
            DetailCategoryVM detailCategory = new DetailCategoryVM
            {
                Name = Category.Name,
                IsDeleted = Category.IsDeleted,
                CreatedAt = Category.CreatedAt,
                ProductCategories = Category.ProductCategories
            };

            return View(detailCategory);
        }
    }
}
