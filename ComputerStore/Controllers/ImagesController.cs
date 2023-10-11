using ComputerStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Controllers
{
    public class ImagesController : Controller
    {
        private ApplicationDbContext _context;
        public ImagesController(ApplicationDbContext context) 
        { 
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> DisplayImg(string id)
        {
            var image = await _context.Images.Where(img => img.Id == id).FirstOrDefaultAsync();
            if(image != null && image.Bytes != null)
            {
                return File(image.Bytes, "image/jpeg"); 
            }
            return NotFound();
        }
    }
}
