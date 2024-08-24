using Customer.Application.AppServices;
using Customer.Application.Common;
using Customer.Core.Interfaces.Common;
using Customer.Core.Interfaces.IAppServices;
using Customer.Infrastructure.DBContext;
using Customer.Infrastructure.Repository;

namespace Customer.WebAPI.Startup
{
    public static class DependencyInjectionSetup
    {
     

        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            #region AppServices

            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddTransient<IStockMarketService, StockMarketService>();
            #endregion



            return services;
        }

     
        public static IServiceCollection RegisterAppRepositories(this IServiceCollection services)
        {
            #region App

            services.AddScoped(typeof(IRepository<Customer.Core.DomainModels.Client, Guid>), typeof(Repository<Customer.Core.DomainModels.Client, Guid, AppDBContext>));
            services.AddScoped(typeof(IRepository<Customer.Core.DomainModels.StockMarket, Guid>), typeof(Repository<Customer.Core.DomainModels.StockMarket, Guid, AppDBContext>));
            #endregion



            return services;
        }
    }
}

