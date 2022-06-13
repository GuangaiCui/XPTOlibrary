using Microsoft.AspNetCore.Mvc;
using XPTOlibrary.DataAccess;
using XPTOlibrary.Models;

namespace XPTOlibrary.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<BookInformation> objBookList = _db.BookInformation;
            return View(objBookList);
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
                _db.BookInformation.Add(obj);
                _db.SaveChanges();
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
            var BookInformationFromDB = _db.BookInformation.FirstOrDefault(x => x.BookISBN == id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookInformation obj)
        {
            if (ModelState.IsValid)
            {
                _db.BookInformation.Update(obj);
                _db.SaveChanges();
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
            var BookInformationFromDbFirst = _db.BookInformation.FirstOrDefault(u => u.BookISBN == id);

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
            var obj = _db.BookInformation.FirstOrDefault(u => u.BookISBN == id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.BookInformation.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Book deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
