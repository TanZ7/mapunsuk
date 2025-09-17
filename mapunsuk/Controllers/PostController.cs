using mapunsuk.Data;
using mapunsuk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace mapunsuk.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Location,Category,StartDate,EndDate,MaxParticipants,ExpirationDate")] Post post)
        {
            ModelState.Remove("Owner");
            ModelState.Remove("OwnerId");

            if (ModelState.IsValid)
            {
                post.OwnerId = _userManager.GetUserId(User);
                _context.Add(post);
                await _context.SaveChangesAsync();
                // เปลี่ยนกลับไปที่หน้าแรกของ Home Controller
                return RedirectToAction("Index", "Home");
            }
            return View(post);
        }
    }
}