using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class Resource
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; }
        public string TopicName { get; set; }
        public string Video { get; set; }
        public string Note { get; set; }
        public string Difficulity { get; set;}
    }
}
