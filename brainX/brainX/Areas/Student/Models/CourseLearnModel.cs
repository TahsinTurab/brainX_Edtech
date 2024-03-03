using brainX.Infrastructure.Domains;

namespace brainX.Areas.Student.Models
{
    public class CourseLearnModel
    {
        public IList<Content> ContentsList {  get; set; }
        public Course Course { get; set; }
    }
}
