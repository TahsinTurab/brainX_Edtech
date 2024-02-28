using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Domains
{
    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double? TotalRevenue { get; set; }
        public double? CurrentBalance { get; set; }
        public double? Withdraw {  get; set; }
        public Instructor Instructor { get; set; }
        public Guid InstructorId { get; set; }
    }
}
