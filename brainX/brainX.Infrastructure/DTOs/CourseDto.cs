using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.DTOs
{
    public class CourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public int Duration { get; set; }
        public int Fee { get; set; }
        public DateOnly CreationDate { get; set; }
        public string Difficulities { get; set; }
    }
}
