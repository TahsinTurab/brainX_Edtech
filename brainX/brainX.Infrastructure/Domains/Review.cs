using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Reviewtext {  get; set; }
        public int Rating { get; set; }
        public Course Course { get; set; }
        public Guid CourseId { get; set; }
        public Student Student { get; set; }
        public Guid StudentId { get; set; }
    }
}
