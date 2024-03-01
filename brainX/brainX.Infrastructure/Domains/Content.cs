using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class Content
    {
        public Guid Id { get; set; }
        public int ContentNo { get; set; }
        public string? ContentName { get; set; }
        public string? NoteUrl { get; set; }
        public string? VideoUrl { get; set; }
        public Course Course { get; set; }
        public Guid CourseId { get; set; }
    }
}
