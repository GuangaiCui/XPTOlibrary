using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using XPTOlibrary.Models.ViewModels;
using XPTOlibrary.Utility;

namespace XPTOlibrary.Controllers;
[Area("Customer")]
[Authorize]
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

    public IActionResult Index(int? SelectOption, DateTime? start, DateTime? end)
    {

        if (!end.HasValue) end = DateTime.Now;
        var userId = "";

        ViewBag.end = end;

        if (_signInManager.IsSignedIn(User))
        {
            userId = _userManager.GetUserId(User);
        }
        IEnumerable<BorrowRecord> borrowRecordLists = _unitOfWork.BorrowRecord.GetAll(includeProperties: "BookInformation,ApplicationUser,Cores").OrderByDescending(s => s.DateBorrow);
        IEnumerable<BorrowRecord> BorrowRecordListsByUser = _unitOfWork.BorrowRecord.GetAll(u => u.ApplicationUserId == userId, includeProperties: "BookInformation,ApplicationUser,Cores").OrderByDescending(s => s.DateBorrow);
        if (User.IsInRole(SD.Role_User))
        {
            if (BorrowRecordListsByUser.Count() == 0)
            {
                start = borrowRecordLists.Last().DateBorrow;
                ViewBag.start = start;
                BorrowRecordListsByUser = BorrowRecordListsByUser.Where(x => x.DateBorrow >= start && x.DateBorrow <= end).OrderByDescending(s => s.DateReturn);
            }
            else
            {
                if (!start.HasValue) start = BorrowRecordListsByUser.Last().DateBorrow;
                ViewBag.start = start;
                BorrowRecordListsByUser = BorrowRecordListsByUser.Where(x => x.DateBorrow >= start && x.DateBorrow <= end).OrderByDescending(s => s.DateReturn);
            }
            if (SelectOption != null)
            {
                BorrowRecordListsByUser = BorrowRecordListsByUser.Where(u => u.Cores.CoreId == SelectOption);
            }

            return View(BorrowRecordListsByUser);
        }
        //admin
        else
        {
            if (!start.HasValue) start = borrowRecordLists.Last().DateBorrow;
            ViewBag.start = start;
            borrowRecordLists = borrowRecordLists.Where(x => x.DateBorrow >= start && x.DateReturn <= end).OrderByDescending(s => s.DateReturn);
        }

        if (SelectOption != null)
        {
            borrowRecordLists = borrowRecordLists.Where(u => u.Cores.CoreId == SelectOption);
        }

        return View(borrowRecordLists);

    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Return(int id)
    {
        var userId = "";
        BorrowRecord borrowRecord;
        borrowRecord = _unitOfWork.BorrowRecord.GetFirstOrDefault(u => u.RecordId == id);
        BookCores bookcore = _unitOfWork.BookCores.GetFirstOrDefault(u => u.BookISBN == borrowRecord.BookISBN && u.CoreId == borrowRecord.CoreId);
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