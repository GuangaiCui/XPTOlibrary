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
            List<UserRolesVM> userRolesVMList = new List<UserRolesVM>();
            foreach(ApplicationUser user in users)
            {
                var thisVM= new UserRolesVM();
                thisVM.UserId=user.Id;
                thisVM.UserName=user.UserName;
                thisVM.Name=user.Name;
                thisVM.Birthday = user.Birthday;
                thisVM.Status=user.Status;
                thisVM.Roles =await GetUserRoles(user);
                userRolesVMList.Add(thisVM);
            }
            return View(userRolesVMList);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(string id)
        {
            //List<UserRolesVM> userRolesVMList = new List<UserRolesVM>();
            //var user= userRolesVMList.FirstOrDefault(u=>u.UserId==id);
            if (User.IsInRole(SD.Role_Admin))
            {
                ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
                user.Status = "Normal";
                _unitOfWork.ApplicationUser.Update(user);
                _unitOfWork.Save();
                TempData["success"] = "User updated successfully";
            }
            TempData["error"] = "Only Admin could make this change";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == id);
            UserRolesVM userRolesVM = new UserRolesVM()
            {
            UserId = user.Id,
            UserName = user.UserName,
            Name = user.Name,
            Birthday = user.Birthday,
            Status = user.Status,
            Roles = await GetUserRoles(user)
        };
            return View(userRolesVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserRolesVM userRolesVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == userRolesVM.UserId);
                _unitOfWork.ApplicationUser.Update(user);
                _unitOfWork.Save();
                TempData["success"] = "User updated successfully";
                return RedirectToAction("Index");
            }
            return View(userRolesVM);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string? id)
        {
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
            
            if (id == null)
            {
                return NotFound();
            }

            _unitOfWork.ApplicationUser.Remove(user);
            _unitOfWork.Save();
            TempData["success"] = "User deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
