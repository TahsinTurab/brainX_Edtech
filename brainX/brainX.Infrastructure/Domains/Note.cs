using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }
}
