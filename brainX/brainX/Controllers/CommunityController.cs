using brainX.Data;
using brainX.Models;
using brainX.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;

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

        public async Task<IActionResult> Index(string status = null)
        {
            ViewBag.Status = status;
            var model = new CommunityModel();
            model.communityQuestions = await _communityRepository.GetAllAsync();
            model.communityQuestions = model.communityQuestions
                .OrderByDescending(item => item.DateTime).ToList();
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
                model.UserName = applicationUser.FirstName+ " "+ applicationUser.LastName;
                model.UserPhotoUrl = applicationUser.ImageUrl;
            }
            var result = await _communityRepository.CreateAsync(model, userId, isAnonymous);
            if (result)
            {
                statusMessage = "Your question has been posted successfully";
            }
            if(isAnonymous)return RedirectToAction("Index", new {status = statusMessage});
            return RedirectToAction("Index", "Profile", new { status = statusMessage });
        }


        public async Task<IActionResult> Answers(Guid Id, string status = null)
        {
            ViewBag.Status = status;
            var model = new CommunityModel();
            model.QuestionId = Id;
            model.Question = await _communityRepository.GetQuestionByIDAsync(Id);
            model.communityAnswers = await _communityRepository.GetAllAnswersByIDAsync(Id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAnswer(CommunityModel model)
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
            var result = await _communityRepository.CreateAnswerAsync(model, userId, isAnonymous);
            if (result)
            {
                statusMessage = "Your answer has been posted successfully";
            }
            return RedirectToAction("Answers", new { Id = model.QuestionId, status = statusMessage });
        }
    }
}
