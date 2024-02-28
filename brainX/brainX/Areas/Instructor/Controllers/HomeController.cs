using brainX.Data;
using brainX.Infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace brainX.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles = "Instructor")]

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IInstructorRepository _instructorRepository;

        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IInstructorRepository instructorRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _instructorRepository = instructorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            await _instructorRepository.CreateAsync(Guid.Parse(applicationUser.Id), applicationUser.UserName);
            return View();
        }
    }
}
