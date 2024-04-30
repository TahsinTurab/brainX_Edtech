using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class CommunityAnswer
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid? UserId { get; set; }
        public string Reply {  get; set; }
        public string? ImageUrl { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public string? UserPhotoUrl { get; set; }

    }
}
