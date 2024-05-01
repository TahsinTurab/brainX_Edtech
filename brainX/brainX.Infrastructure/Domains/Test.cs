using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class Test
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TotalTime { get; set; }
        public string? PracticalTask1 { get; set; }
        public string? PracticalTask2 { get; set; }
        public string? PracticalTask3 { get; set; }
        public Guid CourseId { get; set; }
        public Guid AuthorId { get; set; }
    }
}
