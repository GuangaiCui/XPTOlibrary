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
        public IActionResult Create()
        {
            return View();
        }
    }
}
