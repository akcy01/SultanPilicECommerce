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
            return View();
        }
    }
}
