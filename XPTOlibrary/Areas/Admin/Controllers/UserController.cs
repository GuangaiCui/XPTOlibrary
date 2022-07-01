using Microsoft.AspNetCore.Mvc;
using XPTOlibrary.DataAccess;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace XPTOlibrary.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IUnitofWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            _userManager.GetUserId(User);
            IEnumerable<ApplicationUser> objApplicationUserList = _unitOfWork.ApplicationUser.GetAll();
            return View(objApplicationUserList);
        }
        //public IActionResult Index()
        //{
        //    var user = _userManager.Users;
        //    return View(user.ToList());
        //}
        //GET


        public IActionResult Edit(int? id)
        {
            if(id == null|| id == 0)
            {
                return NotFound();
            }
            //var ApplicationUserFromDB = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.ApplicationUserId == id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationUser obj)
        {
            //var userId = _userManager.GetUserId;
            if (ModelState.IsValid)
            {
                obj.Status = "Normal";
                _unitOfWork.ApplicationUser.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "ApplicationUser updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var ApplicationUserFromDbFirst = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.ApplicationUserId == id);

            //if (ApplicationUserFromDbFirst == null)
            //{
            //    return NotFound();
            //}

            return View();
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            //var obj = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.ApplicationUserId == id);
            //if (obj == null)
            //{
            //    return NotFound();
            //}

            //_unitOfWork.ApplicationUser.Remove(obj);
            //_unitOfWork.Save();
            //TempData["success"] = "ApplicationUser deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
