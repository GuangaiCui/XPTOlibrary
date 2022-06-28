using Microsoft.AspNetCore.Mvc;
using XPTOlibrary.DataAccess;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using System.Collections.Generic;


namespace XPTOlibrary.Controllers
{
    [Area("Admin")]
    public class BookCoresController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public BookCoresController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<BookCores> objBookCoresList = _unitOfWork.BookCores.GetAll();
            return View(objBookCoresList);
        }
        //GET
        public IActionResult Create()

        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookCores obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.BookCores.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "BookCores added successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? book, int? core)
        {
            if(book == null|| book == 0)
            {
                return NotFound();
            }
            if(core == null || core == 0)
            {
                return NotFound();
            }
            var BookCoresFromDB = _unitOfWork.BookCores.GetFirstOrDefault(x => x.BookISBN == book && 
            x.CoreId==core);
            return View(BookCoresFromDB);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookCores obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.BookCores.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "BookCores updated successfully";
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
            var BookCoresFromDbFirst = _unitOfWork.BookCores.GetFirstOrDefault(u => u.CoreId == id);

            if (BookCoresFromDbFirst == null)
            {
                return NotFound();
            }

            return View(BookCoresFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.BookCores.GetFirstOrDefault(u => u.CoreId == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.BookCores.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "BookCores deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
