using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.DTOs.Common
{
    public class EmailToDTO
    {
        public List<string> EmailTo { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
