using brainX.Data;
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
    }
}
