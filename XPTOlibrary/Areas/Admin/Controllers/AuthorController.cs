using Microsoft.AspNetCore.Mvc;
using XPTOlibrary.DataAccess;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using System.Collections.Generic;


namespace XPTOlibrary.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public AuthorController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Author> objAuthorList = _unitOfWork.Author.GetAll();
            return View(objAuthorList);
        }
        //GET
        public IActionResult Create()

        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Author.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Author added successfully";
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
            var AuthorFromDB = _unitOfWork.Author.GetFirstOrDefault(x => x.AuthorId == id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Author.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Author updated successfully";
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
            var AuthorFromDbFirst = _unitOfWork.Author.GetFirstOrDefault(u => u.AuthorId == id);

            if (AuthorFromDbFirst == null)
            {
                return NotFound();
            }

            return View(AuthorFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Author.GetFirstOrDefault(u => u.AuthorId == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Author.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Author deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
