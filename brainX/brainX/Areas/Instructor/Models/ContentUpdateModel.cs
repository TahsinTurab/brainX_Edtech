using brainX.Infrastructure.Domains;

namespace brainX.Areas.Instructor.Models
{
    public class ContentUpdateModel
    {
        public List<Content> oldContent { get; set; }
        public IFormFile VideoFiles { get; set; }
        public IFormFile NoteFiles { get; set; }
        public Guid Id { get; set; }
        public Guid contentId { get; set; }
        public string? ContentName { get; set; }
    }
}
