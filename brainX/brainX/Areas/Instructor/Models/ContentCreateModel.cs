using Microsoft.AspNetCore.Mvc;

namespace brainX.Areas.Instructor.Models
{
    public class ContentCreateModel
    {
        public List<string> ContentNames { get; set; }
        public List<IFormFile> VideoFiles { get; set; }
        public List<IFormFile> NotesFiles { get; set; }
        public List<string> NoteUrls { get; set; }
        public List<string> VideoUrls { get; set; }
        public Guid CourseId { get; set; }
        public List<string> Emails { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
    }
}
