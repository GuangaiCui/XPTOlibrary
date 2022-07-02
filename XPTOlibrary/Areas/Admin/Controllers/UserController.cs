using Microsoft.AspNetCore.Mvc;
using XPTOlibrary.DataAccess;
using XPTOlibrary.DataAccess.Repository.IRepository;
using XPTOlibrary.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using XPTOlibrary.Models.ViewModels;
using XPTOlibrary.Utility;

namespace XPTOlibrary.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUnitofWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task< IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            List<UserRolesVM> userRolesVM = new List<UserRolesVM>();
            foreach(ApplicationUser user in users)
            {
                var thisVM= new UserRolesVM();
                thisVM.UserId=user.Id;
                thisVM.UserName=user.UserName;
                thisVM.Name=user.Name;
                thisVM.Birthday = user.Birthday;
                thisVM.Status=user.Status;
                thisVM.Roles =await GetUserRoles(user);
                userRolesVM.Add(thisVM);
            }
            return View(userRolesVM);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }


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
