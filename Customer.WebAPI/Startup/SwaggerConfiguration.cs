using Customer.WebAPI.Controllers;
using Microsoft.OpenApi.Models;

namespace Customer.WebAPI.Startup
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {

                option.SwaggerDoc(Contexts.CustomerAppContext, new OpenApiInfo { Title = Contexts.CustomerAppContext, Version = "v1" });


                option.CustomSchemaIds(i => i.FullName);
                option.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    }
                );
                option.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
                    }
                );
            });
            return services;
        }
        public static WebApplication ConfigerSwagger(this WebApplication app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";

            });

            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint($"/swagger/{Contexts.CustomerAppContext}/swagger.json", Contexts.CustomerAppContext);

                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);


            });

            return app;
        }

    }
}