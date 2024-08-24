using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.DTOs.Common
{

    public class StockResponseModel
    {
        public string status { get; set; }
        public int queryCount { get; set; }
        public int resultsCount { get; set; }
        public string request_id { get; set; }
        public int count { get; set; }
        public List<StockModel> results { get; set; }
    }
    public class StockModel
    {
        public string T { get; set; }
        public decimal? v { get; set; }
        public decimal? vw { get; set; }
        public decimal? o { get; set; }
        public decimal? c { get; set; }
        public decimal? h { get; set; }
        public decimal? l { get; set; }
        public decimal? t { get; set; }
        public decimal? n { get; set; }
    }
}
