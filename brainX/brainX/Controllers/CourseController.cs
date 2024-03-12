using brainX.Areas.Instructor.Models;
using brainX.Data;
using brainX.Infrastructure.Domains;
using brainX.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace brainX.Controllers
{
    public class CourseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ApplicationDbContext _dbContext;

        public CourseController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            ICourseRepository courseRepository,
            IStudentRepository studentRepository,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(string message = null)
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            
            var model = new CourseViewModel();
            model.CategoryList = await _courseRepository.GetAllCategoriesAsync();
            var myCourses = (List<Course>)await _courseRepository.GetAllAsync();
            bool isCategory = false;

            foreach (var category in model.CategoryList)
            {
                if (category == message)
                {
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
        
        public async Task<IActionResult> Details(Guid id)
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            if (applicationUser != null)
            {
                var roles = await _userManager.GetRolesAsync(applicationUser);
                foreach (var role in roles)
                {
                    if (role == "Student")
                    {
                        var courseList = await _dbContext.StudentCourses.ToListAsync();
                        foreach (var c in courseList)
                        {
                            if (c.CourseId == id && c.StudentId == Guid.Parse(applicationUser.Id))
                            {
                                return RedirectToAction("Learn", "Course", new { area = "Student", courseId = id });
                            }
                        }
                    }

                    else if (role == "Instructor")
                    {
                        var course = await _dbContext.Courses.FirstOrDefaultAsync(e => e.Id == id);
                        if (course.InstructorId == Guid.Parse(applicationUser.Id))
                        {
                            return RedirectToAction("Update", "Course", new { area = "Instructor", id = id });
                        }
                    }
                }
            }

            var model = await _courseRepository.GetCourseDetailsbyId(id);
            return View(model);
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Payment(Guid id)
        {
            var model = await _courseRepository.GetCourseDetailsbyId(id);
            return View(model);
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Enroll(Guid id)
        {
            try
            {
                var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
                var result = await _studentRepository.EnrollCourseAsync(Guid.Parse(applicationUser.Id), id);
                if (result)
                {
                    ViewBag.isOk = true;

                }
                else
                {
                    ViewBag.isOk = false;
                }
            }
            catch
            {
                ViewBag.isOk = false;
            }
            
            return View();
        }
    }
}
