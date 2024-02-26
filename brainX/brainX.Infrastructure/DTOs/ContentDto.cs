using brainX.Infrastructure.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.DTOs
{
    public class ContentDto
    {
        public string ContentName { get; set; }
        public string NoteUrl { get; set; }
        public string VideoUrl { get; set; }
        public Course Course { get; set; }
        public Guid CourseId { get; set; }
    }
}
