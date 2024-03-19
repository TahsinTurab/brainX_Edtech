using brainX.Infrastructure.Domains;

namespace brainX.Models
{
    public class CommunityModel
    {
        public string SearchKey { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<CommunityQuestion> communityQuestions { get; set; }
        public Guid QuestionId { get; set; }
        public string Answer {  get; set; }
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAnonymous { get; set; }
    }
}
