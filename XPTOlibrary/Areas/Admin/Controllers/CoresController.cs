using Microsoft.AspNetCore.Mvc;
using XPTOlibrary.DataAccess;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using XPTOlibrary.Utility;

namespace XPTOlibrary.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoresController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public CoresController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Cores> objCoresList = _unitOfWork.Cores.GetAll();
            return View(objCoresList);
        }
        //GET
        public IActionResult Create()

        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cores obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Cores.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cores added successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if(id == null|| id == 0)
            {
                return NotFound();
            }
            var CoresFromDB = _unitOfWork.Cores.GetFirstOrDefault(x => x.CoreId == id);
            return View(CoresFromDB);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cores obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Cores.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cores updated successfully";
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
            var CoresFromDbFirst = _unitOfWork.Cores.GetFirstOrDefault(u => u.CoreId == id);

            if (CoresFromDbFirst == null)
            {
                return NotFound();
            }

            return View(CoresFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Cores.GetFirstOrDefault(u => u.CoreId == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Cores.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cores deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
