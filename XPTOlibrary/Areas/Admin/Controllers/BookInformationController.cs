using XPTOlibrary.DataAccess;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using XPTOlibrary.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XPTOlibrary.Utility;

namespace XPTOlibrary.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class BookInformationController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public BookInformationController(IUnitofWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {

            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            BookInformationVM BookInformationVM = new()
            {
                BookInformation = new(),

                PublisherList = _unitOfWork.Publisher.GetAll().Select(i => new SelectListItem
                {
                    Text = i.PublisherName,
                    Value = i.PublisherId.ToString()
                }),
                AuthorList = _unitOfWork.Author.GetAll().Select(i => new SelectListItem
                {
                    Text = i.AuthorName,
                    Value = i.AuthorId.ToString()
                }),
                TopicList = _unitOfWork.Topic.GetAll().Select(i => new SelectListItem
                {
                    Text = i.TopicName,
                    Value = i.TopicId.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                //create product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(BookInformationVM);
            }
            else
            {
                BookInformationVM.BookInformation = _unitOfWork.BookInformation.GetFirstOrDefault(u => u.BookISBN == id);
                return View(BookInformationVM);

                //update product
            }


        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookInformationVM obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\BookInformation");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.BookInformation.Cover != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.BookInformation.Cover.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.BookInformation.Cover = @"\images\BookInformation\" + fileName + extension;

                }
                if (_unitOfWork.BookInformation.GetFirstOrDefault(u => u.BookISBN == obj.BookInformation.BookISBN) == null)
                {
                    _unitOfWork.BookInformation.Add(obj.BookInformation);
                }
                else
                {
                    _unitOfWork.BookInformation.Update(obj.BookInformation);
                }
                _unitOfWork.Save();
                TempData["success"] = "BookInformation created successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var BookInformationList = _unitOfWork.BookInformation.GetAll(includeProperties: "Publisher,Author,Topic");
            return Json(new { data = BookInformationList });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.BookInformation.GetFirstOrDefault(u => u.BookISBN == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.Cover.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.BookInformation.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}