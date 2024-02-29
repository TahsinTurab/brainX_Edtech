using brainX.Data;
using brainX.Repositories.Interface;
using System.ComponentModel.DataAnnotations;
namespace brainX.Areas.Instructor.Models
{
    public class CourseCreateModel
    {
        [Required(ErrorMessage = "Title must be provided")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ThumbnailUrl { get; set; }
        public int Duration { get; set; }
        public int Fee { get; set; }
        public DateOnly CreationDate { get; set; }
        public string Difficulities { get; set; }
        public List<string> CategoryList { get; set; }
        public IFormFile ThumbnailFile { get; set; }
    }
}
