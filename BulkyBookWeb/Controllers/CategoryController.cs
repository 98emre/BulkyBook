using BulkyBookWeb.Data;
using BulkyBookWeb.Migrations;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Category = BulkyBookWeb.Models.Category;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();

            return View(categories);
        }

        //GET Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // GET Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);

            if(category == null){
                return NotFound();
            }

            return View(category);
        }

        //POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exatcly match the name");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                TempData["success"] = "Category updated successfully";

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name","The DisplayOrder cannot exatcly match the name");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                TempData["success"] = "Category created successfully";

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
