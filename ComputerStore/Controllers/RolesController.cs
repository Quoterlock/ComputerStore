using ComputerStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace ComputerStore.Controllers
{
    //[Authorize(Roles = RolesContainer.ADMINISTRATOR)]
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager) {
            _roleManager = roleManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var rolesList = _roleManager.Roles.ToList();
            List<string> rolesNames = new List<string>();
            foreach (var role in rolesList) { 
                rolesNames.Add(role.Name.ToString());
            }
            return View(rolesNames);
        }
    }
}
