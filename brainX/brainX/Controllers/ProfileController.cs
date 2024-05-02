using brainX.Data;
using brainX.Infrastructure.Domains;
using brainX.Models;
using brainX.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace brainX.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICommunityRespsitory _communityRepository;


        public ProfileController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICommunityRespsitory communityRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _communityRepository = communityRepository;
        }

        public async Task<IActionResult> Index(string status = null)
        {
            ViewBag.Status = status;
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = new ApplicationUserModel(applicationUser);
            user.communityQuestions = await _communityRepository.GetAllAsync();
            user.communityQuestions = user.communityQuestions
                .OrderByDescending(item => item.DateTime).ToList();
            var userQuestion = new List<CommunityQuestion>();
            foreach(var q in user.communityQuestions)
            {
                if(q.UserId == Guid.Parse(applicationUser.Id))
                {
                    userQuestion.Add(q);
                }
            }
            user.communityQuestions = userQuestion;
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ApplicationUserModel model)
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
                model.UserName = applicationUser.FirstName + " " + applicationUser.LastName;
                model.UserPhotoUrl = applicationUser.ImageUrl;
            }
            var communityModel = new CommunityModel();
            communityModel.Title = model.Title;
            communityModel.Description = model.Description;
            communityModel.Image = model.Image;
            var result = await _communityRepository.CreateAsync(communityModel, userId, isAnonymous);
            if (result)
            {
                statusMessage = "Your question has been posted successfully";
            }
            return RedirectToAction("Index", new { status = statusMessage });
        }

    }
}
