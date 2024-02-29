using brainX.Areas.Instructor.Models;
using brainX.Data;
using brainX.Infrastructure.DTOs;
using brainX.Models;
using brainX.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace brainX.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles = "Instructor")]
    public class CourseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICourseRepository _courseRepository;

        public CourseController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICourseRepository courseRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CourseCreateModel();
            model.CategoryList = await _courseRepository.GetAllCategoriesAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateModel courseModel) 
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);

            return RedirectToAction(nameof(CreateContent));
        }

        [HttpGet]
        public async Task<IActionResult> CreateContent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContent(ContentCreateModel contentModel)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
