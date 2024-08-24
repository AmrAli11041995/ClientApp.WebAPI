using AutoMapper;
using Customer.DTOs.AppDTOs.Client;
using Customer.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Infrastructure.MappingProfile
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientCreateDTO, Customer.Core.DomainModels.Client>().ReverseMap();
            CreateMap< Customer.Core.DomainModels.Client, ClientListingDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

            CreateMap<ClientUpdateDTO, Customer.Core.DomainModels.Client>().ReverseMap();
            CreateMap<StockModel, Customer.Core.DomainModels.StockMarket>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid()))
                .ForMember(dest => dest.Ticker, opt => opt.MapFrom(src => "AAPL"))
                .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => src.t))
                .ForMember(dest => dest.HighestPrice, opt => opt.MapFrom(src => src.h))
                .ForMember(dest => dest.LowestPrice, opt => opt.MapFrom(src => src.l))
                .ForMember(dest => dest.NumberOfTransactions, opt => opt.MapFrom(src => src.n))
                .ForMember(dest => dest.OpenPrice, opt => opt.MapFrom(src => src.o))
                .ForMember(dest => dest.ClosePrice, opt => opt.MapFrom(src => src.c))
                .ForMember(dest => dest.TradingVolume, opt => opt.MapFrom(src => src.v))
                .ForMember(dest => dest.VolumeWeighted, opt => opt.MapFrom(src => src.vw));
        }
    }
}