namespace brainX.Areas.Instructor.Models
{
    public class ContentCreateModel
    {
        public string ContentName { get; set; }
        public string NoteUrl { get; set; }
        public string VideoUrl { get; set; }
        public Guid CourseId { get; set; }
    }
}
