using brainX.Infrastructure.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.DTOs
{
    public class AccountDto
    {
        public double TotalRevenue { get; set; }
        public double CurrentBalance { get; set; }
        public double Withdraw { get; set; }
    }
}
