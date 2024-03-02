using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class Student
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public IList<Course>? EnrolledCourses { get; set; }
    }
}
