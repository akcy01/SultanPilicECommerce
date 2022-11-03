using Microsoft.AspNetCore.Mvc;
using Sultan.DataAccess;
using Sultan.DataAccess.Repository.IRepository;
using Sultan.Models;

namespace SultanPilic.Controllers
{
    [Area("Admin")]
    public class ChickTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChickTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;  
        }
        public IActionResult Index()
        {
            IEnumerable<ChickType> objChickTypeList= _unitOfWork.ChickType.GetAll();
            return View(objChickTypeList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ChickType obj)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.ChickType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Tavuk çeşidi başarıyla oluşturuldu.";
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
            var chickTypeFromDb = _unitOfWork.ChickType.GetFirstOrDefault(u => u.Id == id);
            if(chickTypeFromDb == null)
            {
                return NotFound();
            }
            return View(chickTypeFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ChickType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ChickType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Tavuk çeşidi başarıyla güncellendi.";
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
            var chickTypeFromDb = _unitOfWork.ChickType.GetFirstOrDefault(u => u.Id == id);
            if (chickTypeFromDb == null)
            {
                return NotFound();
            }
            return View(chickTypeFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.ChickType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.ChickType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Tavuk çeşidi başarıyla silindi";
            return RedirectToAction("Index");
        }
    }
}
