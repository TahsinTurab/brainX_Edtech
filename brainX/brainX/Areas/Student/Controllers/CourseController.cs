using brainX.Areas.Instructor.Models;
using brainX.Areas.Student.Models;
using brainX.Data;
using brainX.Infrastructure.Domains;
using brainX.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);

            model.Course = await _courseRepository.GetCourseIdAsync(courseId);
            model.ContentsList = await _courseRepository.GetAllContentsOfCourse(courseId);
            var test = await _courseRepository.GetTestByIdAsync(courseId);
            if (test != null) { 
                var sol = await _courseRepository.GetSolutionAsync(test.Id, Guid.Parse(applicationUser.Id));
                if (sol != null) model.TestResult = sol.verdict;
                else model.TestResult = "No Attempt";
            }   
            return View(model);
        }

        public async Task<IActionResult> TakeTest(Guid id)
        {
            var model = new TakeTestModel();
            model.Test = await _courseRepository.GetTestByIdAsync(id);
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            model.Attemp = 0;
            model.StudentId = Guid.Parse(applicationUser.Id);
            model.TestId = model.Test.Id;
            model.InstructorId = model.Test.AuthorId;
            model.CourseId = id;
            if(model.Test != null)
            {
                var sol = await _courseRepository.GetSolutionAsync(model.Test.Id, Guid.Parse(applicationUser.Id));
                if (sol != null)
                {
                    model.Attemp = sol.Attemp;
                    // Calculate the difference
                    TimeSpan difference = DateTime.Now - sol.EndingDate;

                    // Get the total number of days
                    int daysDifference = (int)difference.TotalDays;
                    if (model.Attemp >= 3 && daysDifference > 30) model.Attemp = 2;

                }
            }
            model.Attemp++;
            await _courseRepository.CreateSolutionAsync(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TakeTest(TakeTestModel model)
        {
            await _courseRepository.CreateSolutionAsync(model);
            return RedirectToAction("Learn", new { courseId = model.CourseId});

        }
    }
}
