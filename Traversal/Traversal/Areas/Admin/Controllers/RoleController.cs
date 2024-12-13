using DTOLayer.DTOs.RolesDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Traversal.Areas.Admin.Models.BookingHotelSearchViewModel;

namespace Traversal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var values=_roleManager.Roles.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RolesViewDTO roles)
        {
            AppRole role = new AppRole()
            {
                Name = roles.RoleName
            };
            var values=await _roleManager.CreateAsync(role);
            if(values.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> DeleteRole(int id)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            await _roleManager.DeleteAsync(value);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            var values=_roleManager.Roles.FirstOrDefault(x=>x.Id==id);
            UpdateRoleDTO updateRole = new UpdateRoleDTO()
            {
                RoleID = values.Id,
                RoleName = values.Name
            };
            return View(updateRole);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleDTO updateRoleDTO)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == updateRoleDTO.RoleID);
            value.Name = updateRoleDTO.RoleName;
            await _roleManager.UpdateAsync(value);
            return RedirectToAction("Index");
        }
        public IActionResult UserList()
        {
            var values = _userManager.Users.ToList();
            return View(values);
        }
        [HttpGet]
        public async Task <IActionResult> AssignRole(int id)
        {
            var user=_userManager.Users.FirstOrDefault(x => x.Id == id);
            TempData["Userid"] = user.Id;
            var role=_roleManager.Roles.ToList();
            var userRoles=await _userManager.GetRolesAsync(user);
            List<RoleAssignDTO> roleAssignDTOs = new List<RoleAssignDTO>();
            foreach(var item in role)
            {
                RoleAssignDTO dTO = new RoleAssignDTO();
                dTO.RoleId = item.Id;
                dTO.RoleName = item.Name;
                dTO.RoleExist = userRoles.Contains(item.Name);
                roleAssignDTOs.Add(dTO);
            }
            return View(roleAssignDTOs);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignDTO> dTOs)
        {
            var userid = (int)TempData["userid"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userid);
            foreach (var item in dTOs)
            {
                if (item.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("UserList");
        }
    }
}
