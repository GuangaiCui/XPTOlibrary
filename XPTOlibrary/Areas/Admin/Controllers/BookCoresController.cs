using XPTOlibrary.DataAccess;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using XPTOlibrary.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            IEnumerable<BookCores> objBookCoresList = _unitOfWork.BookCores.GetAll(includeProperties:"BookInformation,Cores").OrderBy(u=>u.BookISBN).ThenBy(u=>u.CoreId);
            
            return View(objBookCoresList);
        }
        //get
        public IActionResult Create()

        {
            BookCoresVM bookCoresVM = new()
            {
                BookCores = new(),

                BookList = _unitOfWork.BookInformation.GetAll().Select(i => new SelectListItem
                {
                    Text = i.BookName,
                    Value = i.BookISBN.ToString()
                }),
                CoreList = _unitOfWork.Cores.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CoreName,
                    Value = i.CoreId.ToString()
                }),

            };
            return View(bookCoresVM);
        }
        //post
        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePOST(BookCoresVM bookCoresVM)
        {
            IEnumerable<BookCores> bookCoreList = _unitOfWork.BookCores.GetAll();
            BookCores bookCore = new BookCores();
            bookCore = bookCoresVM.BookCores;
            if (ModelState.IsValid)
            {
                foreach (BookCores core in bookCoreList)
                {
                    if (core.BookISBN == bookCore.BookISBN && core.CoreId == bookCore.CoreId)
                    {
                        TempData["error"] = "Already exist";
                        bookCoresVM.BookList = _unitOfWork.BookInformation.GetAll().Select(i => new SelectListItem
                        {
                            Text = i.BookName,
                            Value = i.BookISBN.ToString()
                        });
                        bookCoresVM.CoreList = _unitOfWork.Cores.GetAll().Select(i => new SelectListItem
                        {
                            Text = i.CoreName,
                            Value = i.CoreId.ToString()
                        });

                        return View(bookCoresVM);
                    }
                }
                if (bookCore.Copies < 1)
                {
                    TempData["error"] = "At least have one copy";
                    return View(bookCoresVM);
                }
                _unitOfWork.BookCores.Add(bookCore);
                _unitOfWork.Save();
                TempData["success"] = "BookCores added successfully";
                return RedirectToAction("Index");
            }
            return View(bookCore);
        }
        //Get
        public IActionResult Edit(int? id)
        {
            if(id == null|| id == 0)
            {
                return NotFound();
            }

            var BookCoresFromDB = _unitOfWork.BookCores.GetFirstOrDefault(x => x.BookCoreid == id,includeProperties:"BookInformation,Cores");
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
        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BookCores BookCoresFromDb = _unitOfWork.BookCores.GetFirstOrDefault(u => u.BookCoreid == id,includeProperties:"BookInformation,Cores");

            return View(BookCoresFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            BookCores obj = _unitOfWork.BookCores.GetFirstOrDefault(u => u.BookCoreid == id);
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
