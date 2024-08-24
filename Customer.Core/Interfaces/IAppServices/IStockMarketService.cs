using Customer.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Core.Interfaces.IAppServices
{
    public interface IStockMarketService
    {
        Task<ResponseModel> GetStock();
    }
}
