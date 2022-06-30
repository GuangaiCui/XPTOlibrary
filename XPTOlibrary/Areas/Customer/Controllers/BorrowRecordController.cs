using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using XPTOlibrary.Models.ViewModels;

namespace XPTOlibrary.Controllers;
[Area("Customer")]
public class BorrowRecordController : Controller
{
    private readonly IUnitofWork _unitOfWork;

    public BorrowRecordController(IUnitofWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Bookcore> BorrowRecordList = _unitOfWork.BorrowRecord.GetAll(includeProperties: "BookInformation,Cores");

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