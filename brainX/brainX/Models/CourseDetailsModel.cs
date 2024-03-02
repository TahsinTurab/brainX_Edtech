using brainX.Infrastructure.Domains;

namespace brainX.Models
{
    public class CourseDetailsModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ThumbnailUrl { get; set; }
        public int Duration { get; set; }
        public int Fee { get; set; }
        public DateOnly CreationDate { get; set; }
        public string Difficulities { get; set; }
        public string InstructorName { get; set; }
        public string InstructorId { get; set; }
        public string InstructorImageUrl { get; set; }
        public List<string> ContentsList { get; set; }
        public int Students { get; set; }
        public List<Review> Reviews { get; set; }
        public int AverageRating { get; set; }
    }
}
