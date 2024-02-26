using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class Instructor
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName {  get; set; }
        public string Email { get; set; }
        public string ImageUrl {  get; set; }
        public string Password {  get; set; }
        public List<string> Qualifications { get; set; }
        public ICollection<Course> Courses { get; set; }
        public Account Account { get; set; }
    }
}
