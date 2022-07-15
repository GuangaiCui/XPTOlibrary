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

    public class TopicController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public TopicController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Topic> objTopicList = _unitOfWork.Topic.GetAll();
            return View(objTopicList);
        }
        //GET
        public IActionResult Create()

        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Topic obj)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Topic> topics = _unitOfWork.Topic.GetAll();
                foreach (Topic topic in topics)
                {
                    if (topic.TopicName == obj.TopicName)
                    {
                        TempData["error"] = "Topic already exist, id is" + topic.TopicId;
                        return View(obj);
                    }
                }
                    _unitOfWork.Topic.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Topic added successfully";
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
            var TopicFromDB = _unitOfWork.Topic.GetFirstOrDefault(x => x.TopicId == id);
            return View(TopicFromDB);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Topic obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Topic.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Topic updated successfully";
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
            var TopicFromDbFirst = _unitOfWork.Topic.GetFirstOrDefault(u => u.TopicId == id);

            if (TopicFromDbFirst == null)
            {
                return NotFound();
            }

            return View(TopicFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Topic.GetFirstOrDefault(u => u.TopicId == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Topic.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Topic deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
