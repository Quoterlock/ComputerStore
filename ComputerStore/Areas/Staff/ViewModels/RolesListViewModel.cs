using Microsoft.AspNetCore.Identity;

namespace ComputerStore.Areas.Staff.ViewModels
{
    public class RolesListViewModel
    {
        public List<IdentityUser> Managers { get; set; } = new List<IdentityUser>();
        public List<IdentityUser> Admins { get; set; } = new List<IdentityUser>();
    }
}
