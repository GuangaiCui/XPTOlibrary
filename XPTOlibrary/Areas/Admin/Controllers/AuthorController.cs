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
                IEnumerable<Author> authors = _unitOfWork.Author.GetAll();
                foreach (Author author in authors)
                {
                    if (author.AuthorName == obj.AuthorName)
                    {
                        TempData["error"] = "Author already exist, id is" + author.AuthorId;
                        return View(obj);
                    }
                }
                _unitOfWork.Author.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Author added successfully";
                return RedirectToAction("Index","Author");
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
            return View(AuthorFromDB);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author obj)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Author> authors = _unitOfWork.Author.GetAll();
                foreach (Author author in authors)
                {
                    if (author.AuthorName == obj.AuthorName)
                    {
                        TempData["error"] = "Author already exist, id is" + author.AuthorId;
                        return View(obj);
                    }
                }
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
