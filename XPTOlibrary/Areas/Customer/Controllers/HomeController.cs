using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using XPTOlibrary.Models.ViewModels;

namespace XPTOlibrary.Controllers;
[Area("Customer")]
public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitofWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<BookInformation> BookInformationList = _unitOfWork.BookInformation.GetAll(includeProperties: "Publisher,Author,Topic");

            return View(BookInformationList);

        }
    public IActionResult Details(int id)
    {
        BookInformation BookInformations = _unitOfWork.BookInformation.GetFirstOrDefault(u => u.BookISBN == id, includeProperties: "Publisher,Author,Topic");
        BookCores BookCores = _unitOfWork.BookCores.GetFirstOrDefault(u => u.BookISBN == id, includeProperties: "BookInformation,Cores");
        BookwithCoresVM bookwithCores = new BookwithCoresVM()
        {
            BookInformation = BookInformations,
            BookCores = BookCores,
        };
        return View(bookwithCores);
    }

    public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }