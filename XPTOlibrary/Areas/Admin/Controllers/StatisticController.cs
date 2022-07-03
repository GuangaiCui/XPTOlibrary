using Microsoft.AspNetCore.Mvc;
using XPTOlibrary.DataAccess;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using System.Collections.Generic;


namespace XPTOlibrary.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public StatisticController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
           IEnumerable<BookInformation> bookInformations=_unitOfWork.BookInformation.GetAll();
            ViewData["NoOfBooks"] =bookInformations.Count();
            IEnumerable<BorrowRecord>   borrowRecords=_unitOfWork.BorrowRecord.GetAll();
            ViewData["No.BorrowRecords"]=borrowRecords.Count();
            IEnumerable<Cores> cores = _unitOfWork.Cores.GetAll();
            ViewData["NoOfCores"] = cores.Count();
            int NoOfCopies = 0;
            IEnumerable<BookCores> bookCores = _unitOfWork.BookCores.GetAll();
            foreach (BookCores bookCore in bookCores)
            {
                NoOfCopies += bookCore.Copies;
            }
            ViewData["NoOfCopies"]=NoOfCopies;
            ViewData["BookCores"] = bookCores;



            return View(borrowRecords);
        }
    }
}
