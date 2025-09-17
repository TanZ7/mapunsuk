using mapunsuk.Data;
using mapunsuk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        // GET: Post/Manage
        // เมธอดนี้จะถูกเรียกเมื่อเข้าไปที่ /Post/Manage
        public async Task<IActionResult> Manage()
        {
            var userId = _userManager.GetUserId(User);
            var userPosts = await _context.Posts
                                          .Where(p => p.OwnerId == userId)
                                          .OrderByDescending(p => p.CreatedAt)
                                          .ToListAsync();

            var totalPosts = await _context.Posts.CountAsync();

            var viewModel = new PostManagementViewModel
            {
                UserPosts = userPosts,
                UserPostsCount = userPosts.Count,
                TotalPosts = totalPosts
            };

            return View(viewModel);
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
                return RedirectToAction(nameof(Manage));
            }
            return View(post);
        }
    }
}