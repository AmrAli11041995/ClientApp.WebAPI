using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Core.DomainModels
{
    public class StockMarket
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public decimal? ClosePrice { get; set; }
        public decimal? OpenPrice { get; set; }
        public decimal? HighestPrice { get; set; }
        public decimal? LowestPrice { get; set; }
        public decimal? TimeStamp { get; set; }
        public int? NumberOfTransactions { get; set; }
        public decimal? TradingVolume { get; set; }
        public decimal? VolumeWeighted { get; set; }
    }
}
