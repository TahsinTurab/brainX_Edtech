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
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        
        public async Task<IActionResult> IndexAsync(string message = null)
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.Message = message;
            if(message == "Your course has been created successfully.")
            {
                ViewBag.isOk = true;
            }
            else
            {
                ViewBag.isOk = false;
            }
            var model = new CourseViewModel();
            model.CategoryList = await _courseRepository.GetAllCategoriesAsync();
            var myCourses = (List<Course>)await _courseRepository.GetAllAsync(Guid.Parse(applicationUser.Id));
            bool isCategory = false;

            foreach (var category in model.CategoryList)
            {
                if(category == message)
                {
                    ViewBag.isOk = true;
                    isCategory = true;
                    break;
                }
            }

            if (isCategory)
            {
                model.MyCourses = new List<Course>();
                foreach (var courses in myCourses)
                {
                    if (courses.Category == message)
                    {
                        model.MyCourses.Add(courses);
                    }
                }
            }
            else
            {
                model.MyCourses = myCourses;
            }

            return View(model);
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
        public async Task<IActionResult> CreateContent(string id, string message = null)
        {
            var model = new ContentCreateModel();
            model.CourseId = Guid.Parse(id);
            model.StatusMessage = message; 
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContent(ContentCreateModel contentModel)
        {
            var Message = "Please provide contents by following the guidelines. You can add contents by click update course.";
            try
            {
                if(contentModel.ContentNames==null)  return RedirectToAction("Index", new { message = Message });

                var result = await _courseRepository.CreateContentsAsync(contentModel);
                if(result == true)
                {
                    Message = "Your course has been created successfully.";
                    return RedirectToAction("Index", new { message = Message });
                }
                else
                {
                    return RedirectToAction("Index", new { message = Message });
                    //return RedirectToAction("UpdateContent", new { id = courseId, message = Message });
                }
            }
            catch
            {
                return RedirectToAction("Index", new { message = Message });
                //return RedirectToAction("UpdateContent", new { id = courseId, message = Message });
            }
        }

        public async Task<IActionResult> Update(Guid id, string message = null)
        {
            ViewBag.Message = message;
            var model = await _courseRepository.GetCourseByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseCreateModel courseUpdateModel)
        {
            var result = await _courseRepository.UpdateAsync(courseUpdateModel);
            ViewBag.isOk = false;
            string Message = "Course Information didn't Updated!";
            if (result == true)
            {
                ViewBag.isOk = true;
                Message = "Your Course Information Updated Successfully";
            }
            return RedirectToAction("Update", new {Id = courseUpdateModel.Id, message = Message });
        }

        public async Task<IActionResult> UpdateContent(Guid id, string message = null)
        {
            ViewBag.Message = message;
            var model = await _courseRepository.GetContentsOfCourseById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContent(ContentUpdateModel contentUpdateModel)
        {
            var result = await _courseRepository.UpdateContentAsync(contentUpdateModel);
            ViewBag.isOk = false;
            string Message = "Course Content didn't Updated!";
            if (result == true)
            {
                ViewBag.isOk = true;
                Message = "Your Course Content Updated Successfully";
            }
            return RedirectToAction("UpdateContent", new { Id = contentUpdateModel.Id, message = Message });
        }


        public async Task<IActionResult> Delete(Guid Id)
        {
            return View();
        }
    }
}
