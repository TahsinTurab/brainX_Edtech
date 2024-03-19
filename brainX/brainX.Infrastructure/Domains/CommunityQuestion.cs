using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class CommunityQuestion
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string? ImageUrl { get; set; }
        public bool isAnonymous { get; set; }
        public Guid? UserId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
