using brainX.Areas.Instructor.Models;
using brainX.Areas.Student.Models;
using brainX.Data;
using brainX.Infrastructure.Domains;
using brainX.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace brainX.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
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
            var model = new CourseViewModel();
            model.CategoryList = await _courseRepository.GetAllCategoriesAsync();
            var myCourses = (List<Course>)await _courseRepository.GetAllCourseOfStudentAsync(Guid.Parse(applicationUser.Id));
            bool isCategory = false;

            foreach (var category in model.CategoryList)
            {
                if (category == message)
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

        public async Task<IActionResult> Learn(Guid courseId)
        {
            var model = new CourseLearnModel();
            model.Course = await _courseRepository.GetCourseIdAsync(courseId);
            model.ContentsList = await _courseRepository.GetAllContentsOfCourse(courseId);
            return View(model);
        }

        public async Task<IActionResult> TakeTest(Guid courseId)
        {
            var model = new TakeTestModel();
            return View(model);
        }
    }
}
