using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sultan.DataAccess;
using Sultan.DataAccess.Repository;
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
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;  
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
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
        public IActionResult Upsert(ProductVM obj , IFormFile file) //IFormFile dosya yüklememizi sağlar.
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();//Benzersiz bir dosya adı oluşturmak için bunu yaptık.
                    var uploads = Path.Combine(wwwRootPath, @"images\products");//Yüklenen görselin gideceği klasörü belirttik.
                    var extension = Path.GetExtension(file.FileName);//Uzantıları test ettik burada.

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))  //FileStream yeni bir dosya oluşturmak için kullanılır.
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                //Burdan yukarısında file ekleme işlemi yaptık.
                _unitOfWork.Product.Add(obj.Product);
                _unitOfWork.Save();
                TempData["success"] = "Ürün başarıyla eklendi.";
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
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,ChickType");
            return Json(new {data = productList});
        }
        #endregion
    }
}
