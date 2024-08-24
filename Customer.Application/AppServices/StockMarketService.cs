using AutoMapper;
using Customer.Application.Helper;
using Customer.Core.DomainModels;
using Customer.Core.Interfaces.Common;
using Customer.Core.Interfaces.IAppServices;
using Customer.DTOs.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.AppServices
{
    public class StockMarketService : IStockMarketService
    {
        private readonly APIServer _server;
        private IEmailService _emailService;
        private readonly IRepository<Customer.Core.DomainModels.StockMarket, Guid> _stockRepository;
        private readonly IRepository<Customer.Core.DomainModels.Client, Guid> _clientRepository;

        private readonly IMapper _mapper;
        public StockMarketService(IRepository<Customer.Core.DomainModels.Client, Guid> clientRepository,IRepository<Customer.Core.DomainModels.StockMarket, Guid> stockRepository, IEmailService emailService,
          IMapper mapper, APIServer server)
        {
            _mapper = mapper;
            _stockRepository = stockRepository;
            _server = server;
            _emailService = emailService;
            _clientRepository = clientRepository;
        }

        public async Task<ResponseModel> GetStock()
        {
            try
            {
                var res = _server.CallApiGet("https://api.polygon.io/", $"v2/aggs/ticker/AAPL/range/1/day/2024-08-01/{DateTime.Now.Date.ToString("yyyy-MM-dd")}?adjusted=true&sort=asc&apiKey=dU9NPSY5R8oYVdaa49CDO9yrxJUtPdY3", null);
                var checkCanDeserialize = CheckIfCanDeserializeObject(res.Result);
                if (!checkCanDeserialize)
                    return new ResponseModel() { IsSuccess = false, Data = null, Message = "Can't Deserialize" };

                var deserialized = JsonConvert.DeserializeObject<StockResponseModel>(res.Result);

                if (deserialized.results == null || deserialized.results.Count == 0)
                    return new ResponseModel() { IsSuccess = false, Message = "No Data Found For AAPL Ticker To Add" };

                List<StockMarket> clientsListing = _mapper.Map<List<StockMarket>>(deserialized.results);
                await _stockRepository.CreateManyAsync(clientsListing);
                await _stockRepository.SaveChangesAsync();


                var mails = _clientRepository.GetWhere().Select(x => x.Email).ToList();

                if (mails == null || mails.Count == 0)
                    return new ResponseModel() { IsSuccess = false, Message = "Added Successfully but, No Client Email Found To Send Email" };

                var model = new EmailToDTO()
                {
                    Body = $"{clientsListing.Count()} record for AAPL Ticker has been added into stock market from Date 01-08-2024 until now ",
                    Subject = "update AAPL Ticker of stock Market",
                    EmailTo = mails
                };
                _emailService.Send(model);

                return new ResponseModel() { IsSuccess = true, Message = "Data Added Successfully" };


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private bool CheckIfCanDeserializeObject(string? obj)
        {
            try
            {
                var deserialized = JsonConvert.DeserializeObject<StockResponseModel>(obj);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
