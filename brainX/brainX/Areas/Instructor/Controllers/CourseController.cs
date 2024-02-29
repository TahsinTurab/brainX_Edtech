using AutoMapper;
using brainX.Areas.Instructor.Models;
using brainX.Data;
using brainX.Infrastructure.Domains;
using brainX.Infrastructure.DTOs;
using brainX.Infrastructure.Services;
using brainX.Models;
using brainX.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;

namespace brainX.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles = "Instructor")]
    public class CourseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICourseRepository _courseRepository;

        public CourseController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            ICourseRepository courseRepository)
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
            var statusMessage = "Your course didn't created";
            try
            {
                var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
                //var isOk = true;
                var courseId = await _courseRepository.CreateAsync(courseModel, Guid.Parse(applicationUser.Id));
                if (courseId != null)
                {
                    var Message = "Your course has been created Successfully! Now add course contents.";
                    return RedirectToAction("CreateContent", new {id = courseId, message = Message});
                }
                else
                {
                    courseModel.StatusMessage = statusMessage;
                    return RedirectToAction();
                }
            }
            catch
            {
                courseModel.StatusMessage = statusMessage;
                return RedirectToAction();
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateContent(string id, string message)
        {
            var model = new ContentCreateModel();
            model.CourseId = Guid.Parse(id);
            model.StatusMessage = message; 
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContent(ContentCreateModel contentModel)
        {
            var Message = "Please provide contents by following the guidelines.";
            try
            {
                var result = await _courseRepository.CreateContentsAsync(contentModel);
                if(result == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                    //return RedirectToAction("UpdateContent", new { id = courseId, message = Message });
                }
            }
            catch
            {
                return RedirectToAction("Index");
                //return RedirectToAction("UpdateContent", new { id = courseId, message = Message });
            }
        }
    }
}
