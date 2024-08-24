using AutoMapper;
using Customer.Application.Helper;
using Customer.Core.Interfaces.IAppServices;
using Customer.Infrastructure.DBContext;
using Customer.Infrastructure.MappingProfile;
using Customer.WebAPI.Startup;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<AppDBContext>(options=> { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.RegisterAppRepositories();
builder.Services.RegisterAppServices();
builder.Services.AddControllers();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddHangfire((sp, config) =>
{
    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
    config.UseSqlServerStorage(connectionString);
});
builder.Services.AddHangfireServer();

#region Mapping
var config = new MapperConfiguration(cfg => {
    cfg.AddProfile(new ClientProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddSingleton<APIServer>();

#endregion



var app = builder.Build();

app.ConfigerSwagger();

app.UseAuthorization();

app.MapControllers();
app.UseCors(x => x
             .AllowAnyMethod()
             .AllowAnyHeader()
             .SetIsOriginAllowed(origin => true) // allow any origin
             .AllowCredentials());
app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<IStockMarketService>(x => x.GetStock(), "0 0 */6 ? * *");
app.Run();
