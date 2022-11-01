using Microsoft.AspNetCore.Mvc;
using SultanPilic.Data;
using SultanPilic.Models;

namespace SultanPilic.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext context)
        {
            _dbContext = context;   
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList= _dbContext.Categories;
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Categories.Add(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "Kategori başarıyla oluşturuldu.";
                return RedirectToAction("Index");
            }
                return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _dbContext.Categories.Find(id);

            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "Kategori başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _dbContext.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _dbContext.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _dbContext.Categories.Remove(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "Kategori başarıyla silindi";
            return RedirectToAction("Index");
        }
    }
}
