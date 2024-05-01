using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class Solution
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public Guid InstructorId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime EndingDate { get; set;}
        public int Attemp { get; set; }
        public string? Solution1 { get; set; }
        public string? Solution2 { get; set; }
        public string? Solution3 { get; set; }
        public string verdict { get; set; }
    }
}
