using Microsoft.AspNetCore.Mvc;
using XPTOlibrary.DataAccess;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using System.Collections.Generic;


namespace XPTOlibrary.Controllers
{
    public class BookInformationController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public BookInformationController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<BookInformation> objBookInformationList = _unitOfWork.BookInformation.GetAll();
            return View(objBookInformationList);
        }
        //GET
        public IActionResult Create()

        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookInformation obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.BookInformation.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Book added successfully";
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
            var BookInformationFromDB = _unitOfWork.BookInformation.GetFirstOrDefault(x => x.BookISBN == id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookInformation obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.BookInformation.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Book updated successfully";
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
            var BookInformationFromDbFirst = _unitOfWork.BookInformation.GetFirstOrDefault(u => u.BookISBN == id);

            if (BookInformationFromDbFirst == null)
            {
                return NotFound();
            }

            return View(BookInformationFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.BookInformation.GetFirstOrDefault(u => u.BookISBN == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.BookInformation.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Book deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
