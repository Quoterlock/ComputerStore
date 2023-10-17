using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace ComputerStore.Areas.Manager.Controllers
{
    //[Authorize(Roles = RolesContainer.ADMINISTRATOR)]
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
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

        [HttpPost]
        public ActionResult Add(string role, string userId)
        {
            // add role to user if it hasn't
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Remove(string role, string userId)
        {
            // remove role from user is it has
            return RedirectToAction(nameof(Index));
        }
    }
}
