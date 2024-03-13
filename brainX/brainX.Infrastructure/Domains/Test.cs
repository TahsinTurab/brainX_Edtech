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
        public int QuizTime { get; set; }
        public IList<Quiz> Quizes { get; set; }
        public string PracticalTask { get; set; }
        public Guid CourseId { get; set; }
    }
}
