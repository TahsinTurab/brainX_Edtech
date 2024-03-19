using brainX.Data;
using brainX.Models;
using brainX.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace brainX.Controllers
{
    public class CommunityController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICommunityRespsitory _communityRepository;

        public CommunityController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICommunityRespsitory communityRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _communityRepository = communityRepository;
        }

        public IActionResult Index(string status = null)
        {
            ViewBag.Status = status;
            var model = new CommunityModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(CommunityModel model)
        {
            var userId = Guid.NewGuid();
            var isAnonymous = false;
            var statusMessage = "Sorry! can't post. Please try again.";
            if (!_signInManager.IsSignedIn(User))
            {
                isAnonymous = true;
            }
            else
            {
                var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
                userId = Guid.Parse(applicationUser.Id);
            }
            var result = await _communityRepository.CreateAsync(model, userId, isAnonymous);
            if (result)
            {
                statusMessage = "Your question has been posted successfully";
            }
            return RedirectToAction("Index", new {status = statusMessage});
        }
    }
}
