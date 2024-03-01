using brainX.Infrastructure.Domains;

namespace brainX.Areas.Instructor.Models
{
    public class CourseViewModel
    {
        public List<Course> MyCourses { get; set; }
        public string CategoryName { get; set; }
        public string CourseName { get; set; }
        public List<string> CategoryList { get; set; }
    }
}
