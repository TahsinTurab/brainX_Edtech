using brainX.Data;
using brainX.Infrastructure.Repositories.Implementation;
using brainX.Infrastructure.Repositories.Interface;
using brainX.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace brainX.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IStudentRepository _studentRepository;

        public HomeController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IStudentRepository studentRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _studentRepository = studentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            await _studentRepository.CreateAsync(Guid.Parse(applicationUser.Id), applicationUser.UserName);
            return View();
        }
    }
}
