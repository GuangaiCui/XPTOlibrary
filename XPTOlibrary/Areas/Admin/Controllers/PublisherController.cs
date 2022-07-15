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
    [Authorize(Roles =SD.Role_Admin)]
    public class PublisherController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public PublisherController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Publisher> objPublisherList = _unitOfWork.Publisher.GetAll();
            return View(objPublisherList);
        }
        //GET
        public IActionResult Create()

        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Publisher> publishers = _unitOfWork.Publisher.GetAll();
                foreach (Publisher publisher in publishers)
                {
                    if (publisher.PublisherName == obj.PublisherName)
                    {
                        TempData["error"] = "Publisher already exist, id is" + publisher.PublisherId;
                        return View(obj);
                    }
                }
                    _unitOfWork.Publisher.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Publisher added successfully";
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
            var PublisherFromDB = _unitOfWork.Publisher.GetFirstOrDefault(x => x.PublisherId == id);
            return View(PublisherFromDB);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Publisher.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Publisher updated successfully";
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
            var PublisherFromDbFirst = _unitOfWork.Publisher.GetFirstOrDefault(u => u.PublisherId == id);

            if (PublisherFromDbFirst == null)
            {
                return NotFound();
            }

            return View(PublisherFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Publisher.GetFirstOrDefault(u => u.PublisherId == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Publisher.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Publisher deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
