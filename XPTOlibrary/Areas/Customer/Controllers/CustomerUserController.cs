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
    [Area("Customer")]
    public class CustomerUserController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CustomerUserController(IUnitofWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
           string userId =_userManager.GetUserId(User);
            ApplicationUser applicationUser=_unitOfWork.ApplicationUser.GetFirstOrDefault(u=>u.Id == userId);
            var users = _userManager.Users.ToList();
            UserRolesVM userRolesVMList = new UserRolesVM()
            {
                UserId = applicationUser.Id,
                UserName = applicationUser.UserName,
                Name = applicationUser.Name,
                RegisterTime = applicationUser.RegisterTime,
                Status = applicationUser.Status,
                Roles = await GetUserRoles(applicationUser),
            };
            return View(userRolesVMList);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Hibernate(string id)
        {
            //List<UserRolesVM> userRolesVMList = new List<UserRolesVM>();
            //var user= userRolesVMList.FirstOrDefault(u=>u.UserId==id);
            if (_userManager.GetUserId(User) == id)
            {
                ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
                user.Status = UserStatus.Status_Hibernate;
                IEnumerable<BorrowRecord> borrowRecords = _unitOfWork.BorrowRecord.GetAll(u => u.ApplicationUserId == id);
                foreach (BorrowRecord record in borrowRecords)
                {
                    if (record.DateReturn == null)
                    {
                        record.DateReturn = DateTime.Now;
                        _unitOfWork.BorrowRecord.Update(record);
                    }
                }
                _unitOfWork.ApplicationUser.Update(user);
                _unitOfWork.Save();
                TempData["success"] = "User hibernated successfully";
            }

            return RedirectToAction("Index");
        }


    }
}
