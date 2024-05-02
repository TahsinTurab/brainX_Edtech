using brainX.Data;
using brainX.Infrastructure.Domains;
using Microsoft.AspNetCore.Identity;

namespace brainX.Models
{
    public class ApplicationUserModel
    {
        public ApplicationUserModel(ApplicationUser user)
        {
            UserName = user.UserName;
            Email = user.Email;
            Name = user.FirstName + " " + user.LastName;
            PhoneNumber = user.PhoneNumber;
            ImageUrl = user.ImageUrl;
        }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }

        public string SearchKey { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<CommunityQuestion> communityQuestions { get; set; }
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
        public IFormFile Image { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsAnonymous { get; set; }
        public string UserPhotoUrl { get; set; }
        public ICollection<CommunityAnswer> communityAnswers { get; set; }
        public CommunityQuestion Question { get; set; }
        public CommunityModel communityModel { get; set; }
    }
}
