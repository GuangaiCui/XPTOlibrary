﻿using Microsoft.AspNetCore.Identity;
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

    public IActionResult Index()
    {
        var userId = "";
        if (_signInManager.IsSignedIn(User))
        {
            userId = _userManager.GetUserId(User);
        }
        IEnumerable<Bookcore> BorrowRecordList;
        if (User.IsInRole(SD.Role_User))
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());

            //var current_User = _userManager.GetUserAsync(HttpContext.User);
            BorrowRecordList = _unitOfWork.BorrowRecord.GetAll(u => u.ApplicationUserId == userId, includeProperties: "BookInformation,Cores");
        }
        else
        {
            BorrowRecordList = _unitOfWork.BorrowRecord.GetAll(includeProperties: "BookInformation,Cores");
        }

        return View(BorrowRecordList);

    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Return(int id)
    {
        Bookcore borrowRecord = _unitOfWork.BorrowRecord.GetFirstOrDefault(u=>u.RecordId == id);
        borrowRecord.DateReturn= DateTime.Now;
        return RedirectToAction("Index");
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