using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using XPTOlibrary.Models.ViewModels;
using XPTOlibrary.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace XPTOlibrary.Controllers;
[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitofWork _unitOfWork;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    public HomeController(ILogger<HomeController> logger, IUnitofWork unitOfWork, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _signInManager = signInManager;
        _userManager = userManager;
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
                                                                     || u.Publisher.PublisherName.Contains(searchString) || u.Topic.TopicName.Contains(searchString), includeProperties: "Publisher,Author,Topic");
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
    [ValidateAntiForgeryToken]
    public async Task< IActionResult>  Borrow(int id)
    {
        var userId = "";
        ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId);
        BookCores bookCores = _unitOfWork.BookCores.GetFirstOrDefault(u => u.BookCoreid == id, includeProperties: "BookInformation,Cores");

        if (_signInManager.IsSignedIn(User))
        {
            userId = _userManager.GetUserId(User);



            if (User.IsInRole(SD.Role_User)&&applicationUser.Status == "Normal")
            {
                if (bookCores.Copies > 1)
                {
                    bookCores.Copies -= 1;
                    //borrow succeed

                    BorrowRecord borrowRecord = new BorrowRecord()
                    {
                        BookISBN = bookCores.BookISBN,
                        ApplicationUserId = userId,
                        CoreId = bookCores.CoreId,
                        DateBorrow = DateTime.Now,
                        DateReturn = null
                    };
                    _unitOfWork.BorrowRecord.Add(borrowRecord);
                    TempData["sucess"] = "Borrow succeed!";
                }
                else
                {
                    TempData["failed"] = "Borrow failed";
                }
            }

            _unitOfWork.Save();
        }
        return RedirectToAction("Index");
    }
    //Get
    public IActionResult MoveCopies(int id)
    {
        MoveCopiesVM moveCopiesVM = new()
        {
            BookCores = _unitOfWork.BookCores.GetFirstOrDefault(u=>u.BookISBN==id,includeProperties:"BookInformation"),

            OriginCoreList = _unitOfWork.BookCores.GetAll(includeProperties:"Cores").Select(i => new SelectListItem
            {
                Text = i.Cores.CoreName,
                Value = i.CoreId.ToString()
            }),
            DestinationCoreList = _unitOfWork.BookCores.GetAll(includeProperties: "Cores").Select(i => new SelectListItem
            {
                Text = i.Cores.CoreName,
                Value = i.CoreId.ToString()
            }),

        };
        return View(moveCopiesVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult MoveCopies(BookCores bookCores)
    {
        return View(bookCores);
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