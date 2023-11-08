using ComputerStore.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

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
        public ActionResult Index()
        {
            // get all roles in separate lists
            /*
            var rolesList = _roleManager.Roles.ToList();
            List<string> rolesNames = new List<string>();
            foreach (var role in rolesList)
            {
                rolesNames.Add(role.Name.ToString());
            }
            return View(rolesNames);
            */
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Add(string role/*, string userId*/)
        {
            /*
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
                await _userManager.AddToRoleAsync(user, RolesContainer.ADMINISTRATOR);
            */
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Remove(string role, string userId)
        {
            // remove role from user is it has
            return RedirectToAction(nameof(Index));
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
