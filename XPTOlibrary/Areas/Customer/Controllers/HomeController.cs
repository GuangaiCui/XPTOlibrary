using Microsoft.AspNetCore.Identity;
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
    //private readonly UserManager<ApplicationUser> _userManager;
    public HomeController(ILogger<HomeController> logger, IUnitofWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index(string? searchString)
    {
        IEnumerable<BookInformation> BookInformationList;
        if (searchString == null)
        {
            BookInformationList = _unitOfWork.BookInformation.GetAll(includeProperties: "Publisher,Author,Topic");
        }
        else
        {
            BookInformationList = _unitOfWork.BookInformation.GetAll(u => u.BookName.Contains(searchString) || u.Author.AuthorName.Contains(searchString)
                                                                     || u.Publisher.PublisherName.Contains(searchString) || u.Topic.TopicName.Contains(searchString),includeProperties: "Publisher,Author,Topic");
        }

        return View(BookInformationList);

    }
    public IActionResult Details(int id)
    {
        var BookInformations = _unitOfWork.BookInformation.GetFirstOrDefault(u => u.BookISBN == id, includeProperties: "Publisher,Author,Topic");
        IEnumerable<BookCores> BookCoresList = _unitOfWork.BookCores.GetAll(u => u.BookISBN == id, includeProperties: "BookInformation,Cores");
        BookwithCoresVM bookwithCores = new()
        {
            BookInformation = BookInformations,
            BookCores = BookCoresList,
        };
        return View(bookwithCores);
    }
    [HttpPost]
    public IActionResult Borrow(int id)
    {
        string userId = "1abd";
        int countOfBorrowed = 0;
        IEnumerable<Bookcore> borrowRecord = _unitOfWork.BorrowRecord.GetAll(u => u.ApplicationUserId == userId);
        BookCores BookCores = _unitOfWork.BookCores.GetFirstOrDefault(u => u.BookCoreid==id);
        ApplicationUser applicationUser = null;
        foreach (var record in borrowRecord)
        {
            if (record.DateReturn == null)
            {
                countOfBorrowed++;

                if (countOfBorrowed == 4)
                {
                    break;
                }
                return RedirectToAction("Index");
            }
        }
        //borrow failed
        //alert("maximum of 4 books")
        //if (record.DateBorrow.AddDays(15) > record.DateReturn || record.DateBorrow.AddDays(15) > DateTime.Today
        //ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.ApplicationUserId == userId);
        //var user = UserManager.FindById(User.Identity.GetUserId());


        if (applicationUser.Status == "Normal")
        {


            if (BookCores.Copies > 1)
            {
                BookCores.Copies -= 1;
                //borrow succeed
            }
        }
        return View();
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