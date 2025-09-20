using mapunsuk.Data;
using mapunsuk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mapunsuk.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create(Post post, IFormFile coverImageFile)
        {
            ModelState.Remove("Owner");
            ModelState.Remove("OwnerId");
            ModelState.Remove("CoverImage");

            if (ModelState.IsValid)
            {
                if (coverImageFile != null && coverImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/covers");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(coverImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await coverImageFile.CopyToAsync(fileStream);
                    }
                    post.CoverImage = "/images/covers/" + uniqueFileName;
                }

                post.OwnerId = _userManager.GetUserId(User);
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage));
            }
            return View(post);
        }
    }
}