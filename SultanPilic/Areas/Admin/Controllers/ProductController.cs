using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sultan.DataAccess;
using Sultan.DataAccess.Repository.IRepository;
using Sultan.Models;
using System.Collections.Generic;

namespace SultanPilic.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;  
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objCategoryList= _unitOfWork.Product.GetAll();
            return View(objCategoryList);
        }
       
        public IActionResult Upsert(int? id)
        {
            /* Aşağıda tanımladığımız IEnumerable'lar ile ilişkisel bir yapı kurduk.Kategori ile ürün arasında. */
            Product product = new();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            IEnumerable<SelectListItem> ChickTypeList = _unitOfWork.ChickType.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            if (id == null || id == 0)
            {
                ViewBag.CategoryList = CategoryList;
                ViewData["ChickTypeList"] = ChickTypeList;
                return View(product);
            }
            else
            {

            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
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
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
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
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Kategori başarıyla silindi";
            return RedirectToAction("Index");
        }
    }
}
