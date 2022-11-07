using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sultan.DataAccess;
using Sultan.DataAccess.Repository.IRepository;
using Sultan.Models;
using Sultan.Models.ViewModels;
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
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                ChickTypeList = _unitOfWork.ChickType.GetAll().Select(i => new SelectListItem
                {
                    Text=i.Name,
                    Value=i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
            //    ViewBag.CategoryList = CategoryList;
            //    ViewData["ChickTypeList"] = ChickTypeList;
                return View(productVM);
            }
            else
            {

            }
            return View(productVM);
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
