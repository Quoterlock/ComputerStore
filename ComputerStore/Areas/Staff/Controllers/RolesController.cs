using ComputerStore.Areas.Staff.ViewModels;
using ComputerStore.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Net.WebSockets;

namespace ComputerStore.Areas.Staff.Controllers
{
    [Area("Staff")]
    //[Authorize(Roles = RolesContainer.ADMINISTRATOR)]
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var admins = await _userManager.GetUsersInRoleAsync(RolesContainer.ADMINISTRATOR);
            var managers = await _userManager.GetUsersInRoleAsync(RolesContainer.MANAGER);
            return View(new RolesListViewModel
            {
                Admins = admins.ToList(),
                Managers = managers.ToList(),
            });
        }

        [HttpGet]
        public async Task<ActionResult> AddUser(string role)
        {
            if (role != null)
                return View(nameof(AddUser), role);
            else 
                return NotFound("role");
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(string role, string userName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(role))
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
                if (user != null)
                {
                    if (role.Equals(RolesContainer.ADMINISTRATOR) || role.Equals(RolesContainer.MANAGER))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                        return RedirectToAction(nameof(Index));
                    }
                    else return NotFound(role);
                }
                else return NotFound(user);
            }
            else return RedirectToAction(nameof(AddUser), new { role });
        }

        [HttpGet]
        public async Task<ActionResult> RemoveUser(string role, string userId)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(role))
            {
                var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    if (role.Equals(RolesContainer.ADMINISTRATOR) || role.Equals(RolesContainer.MANAGER))
                    {
                        await _userManager.RemoveFromRoleAsync(user, role);
                        return RedirectToAction(nameof(Index));
                    }
                    else return NotFound(role);
                }
                else return NotFound(userId);
            }
            else return RedirectToAction(nameof(AddUser), new { role });
        }

        private string? GetUserId()
        {
            var id = User
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?
                .Value;
            return id;
        }
    }

}
