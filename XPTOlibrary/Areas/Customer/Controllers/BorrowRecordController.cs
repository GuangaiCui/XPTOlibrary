using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using XPTOlibrary.Models.ViewModels;
using XPTOlibrary.Utility;

namespace XPTOlibrary.Controllers;
[Area("Customer")]
public class BorrowRecordController : Controller
{
    private readonly IUnitofWork _unitOfWork;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public BorrowRecordController(IUnitofWork unitOfWork, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _unitOfWork = unitOfWork;
        _signInManager = signInManager;
        _userManager = userManager;

    }

    public IActionResult Index(int? SelectOption,DateTime? start, DateTime? end)
    {

        if (!end.HasValue) end = DateTime.Now.Date;
        var userId = "";
        
        ViewBag.end = end;

        if (_signInManager.IsSignedIn(User))
        {
            userId = _userManager.GetUserId(User);
        }
        IEnumerable<BorrowRecord> BorrowRecordList;
        if (User.IsInRole(SD.Role_User))
        {
            BorrowRecordList = _unitOfWork.BorrowRecord.GetAll(u => u.ApplicationUserId == userId, includeProperties: "BookInformation,ApplicationUser,Cores").OrderByDescending(s=>s.DateBorrow);
            if (!start.HasValue) start = BorrowRecordList.Last().DateBorrow;
            ViewBag.start = start;
            BorrowRecordList = BorrowRecordList.Where(x=>x.DateBorrow>start&&x.DateReturn<end).OrderByDescending(s=>s.DateReturn);
        }
        else
        {
            BorrowRecordList = _unitOfWork.BorrowRecord.GetAll(includeProperties: "BookInformation,Cores").OrderByDescending(s => s.DateBorrow);
            if (!start.HasValue) start = BorrowRecordList.Last().DateBorrow;
            ViewBag.start = start;
            BorrowRecordList = BorrowRecordList.Where(x => x.DateBorrow >= start && x.DateReturn <= end).OrderByDescending(s => s.DateReturn);
        }
        if (SelectOption != null)
        {
            BorrowRecordList = BorrowRecordList.Where(u => u.Cores.CoreId == SelectOption);
        }

        return View(BorrowRecordList);

    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Return(int id)
    {
        var userId = "";
        BorrowRecord borrowRecord;
        borrowRecord = _unitOfWork.BorrowRecord.GetFirstOrDefault(u => u.RecordId == id);
        BookCores bookcore = _unitOfWork.BookCores.GetFirstOrDefault(u=>u.BookISBN == borrowRecord.BookISBN&&u.CoreId==borrowRecord.CoreId);
        if (_signInManager.IsSignedIn(User))
        {
            userId = _userManager.GetUserId(User);
            if (userId == borrowRecord.ApplicationUserId || User.IsInRole(SD.Role_Admin))
            {
                borrowRecord.DateReturn = DateTime.Now;
                bookcore.Copies += 1;
                _unitOfWork.Save();
                TempData["success"] = "Returned successfully";
            }
            else
                TempData["error"] = "Return failed";
        }
        else
        {
            TempData["error"] = "Please login first";
        }
        
        return RedirectToAction("Index");
    }
}