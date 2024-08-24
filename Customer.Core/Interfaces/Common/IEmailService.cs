using Customer.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Core.Interfaces.Common
{
    public interface IEmailService
    {
        public Result<object> Send(EmailToDTO emailModel);

    }
}
