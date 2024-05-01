using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class Tutorial
    {
        public Guid Id { get; set; }
        public string Course { get; set; }
        public string Topic { get; set; }
        public string Link { get; set; }
    }
}
