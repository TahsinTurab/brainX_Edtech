using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace brainX.Areas.Instructor.Models
{
    public class ContentCreateModel
    {
        public List<string> ContentNames { get; set; }
        public List<IFormFile> VideoFiles { get; set; }
        public List<IFormFile> NoteFiles { get; set; }
        public List<string> NoteUrls { get; set; }
        public List<string> VideoUrls { get; set; }
        public Guid CourseId { get; set; }
        public string ContentName { get; set; }
        public string NoteUrl { get; set; }
        public string VideoUrl { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
    }
}
