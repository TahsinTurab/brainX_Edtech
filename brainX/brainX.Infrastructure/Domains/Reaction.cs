using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class Reaction
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public int UpVote { get; set; } = 0;
        public int DownVote { get; set; } = 0;
    }
}
