using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public int Duration { get; set; }
        public int Fee { get; set; }
        public DateOnly CreationDate { get; set; }
        public string Difficulities { get; set; }
        public Instructor Instructor { get; set; }
        public Guid InstructorId { get; set; }
        public ICollection<Content> Contents { get; set; }
    }
}
