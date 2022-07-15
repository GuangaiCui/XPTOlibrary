using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using XPTOlibrary.Models.ViewModels;
using XPTOlibrary.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize]

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
    [Authorize(Roles = SD.Role_User)]

    public async Task<IActionResult> Borrow(int id)
    {
        var userId = "";
        

        if (_signInManager.IsSignedIn(User))
        {
            userId = _userManager.GetUserId(User);
            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId);
            BookCores bookCore = _unitOfWork.BookCores.GetFirstOrDefault(u => u.BookCoreid == id, includeProperties: "BookInformation,Cores");


            if (applicationUser.Status == "Normal")
            {
                IEnumerable<BorrowRecord> borrowRecords=_unitOfWork.BorrowRecord.GetAll(u=>u.ApplicationUserId == userId&&u.DateReturn==null);
                if (borrowRecords.Count() <= 4) {
                    if (bookCore.Copies > 1)
                    {
                        bookCore.Copies -= 1;
                        //borrow succeed
                        _unitOfWork.BookCores.Update(bookCore);
                        BorrowRecord borrowRecord = new BorrowRecord()
                        {
                            BookISBN = bookCore.BookISBN,
                            ApplicationUserId = userId,
                            CoreId = bookCore.CoreId,
                            DateBorrow = DateTime.Now,
                            DateReturn = null
                        };

                        _unitOfWork.BorrowRecord.Add(borrowRecord);
                        _unitOfWork.Save();
                        TempData["sucess"] = "Borrow succeed!";
                    }
                }
                else
                {
                    TempData["error"] = "Borrow failed";
                }
            }

        }
        return RedirectToAction("Index");
    }
    //Get
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult MoveCopies(int id)
    {
        MoveCopiesVM moveCopiesVM = new()
        {
            BookCores = new(),
            MoveCopies = 1,

            OriginCoreList = _unitOfWork.Cores.GetAll().Select(i => new SelectListItem
            {
                Text = i.CoreName,
                Value = i.CoreId.ToString()
            }),
            DestinationCoreList = _unitOfWork.Cores.GetAll().Select(i => new SelectListItem
            {
                Text = i.CoreName,
                Value = i.CoreId.ToString()
            }),

        };
        moveCopiesVM.BookCores = _unitOfWork.BookCores.GetFirstOrDefault(u => u.BookISBN == id, includeProperties: "BookInformation,Cores");
        return View(moveCopiesVM);
    }
    [HttpPost, ActionName("MoveCopies")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = SD.Role_Admin)]
    public async Task< IActionResult> MoveCopiesPost(MoveCopiesVM moveCopiesVM)
    {
        BookCores originBookCores = _unitOfWork.BookCores.GetFirstOrDefault(u => u.BookISBN == moveCopiesVM.BookCores.BookISBN &&
         u.CoreId == moveCopiesVM.OriginCoreId);
        BookCores destinationBookCores = _unitOfWork.BookCores.GetFirstOrDefault(u => u.BookISBN == moveCopiesVM.BookCores.BookISBN &&
        u.CoreId == moveCopiesVM.DestinationCoreId);

        moveCopiesVM.BookCores = _unitOfWork.BookCores.GetFirstOrDefault(u => u.BookISBN == moveCopiesVM.BookCores.BookISBN, includeProperties: "BookInformation,Cores");
        if (ModelState.IsValid)
        {
            if (moveCopiesVM.OriginCoreId == moveCopiesVM.DestinationCoreId|| originBookCores.Copies < 1 + moveCopiesVM.MoveCopies)
            {
                
                moveCopiesVM.OriginCoreList = _unitOfWork.Cores.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CoreName,
                    Value = i.CoreId.ToString()
                });
                moveCopiesVM.DestinationCoreList = _unitOfWork.Cores.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CoreName,
                    Value = i.CoreId.ToString()
                });
                return View(moveCopiesVM);
                TempData["error"] = "Can not move to the same core Or Not enough copies to move ";
            }
            else
            {
                originBookCores.Copies -= moveCopiesVM.MoveCopies;
                destinationBookCores.Copies += moveCopiesVM.MoveCopies;
                _unitOfWork.BookCores.Update(originBookCores);
                _unitOfWork.BookCores.Update(destinationBookCores);

                _unitOfWork.Save();
                TempData["success"] = "Book moved successfully";
                return RedirectToAction("Details", new { id = moveCopiesVM.BookCores.BookISBN });
            }
        }
        return RedirectToAction("Details", new { id = moveCopiesVM.BookCores.BookISBN });
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