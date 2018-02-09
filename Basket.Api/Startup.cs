using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingBasket.Core.DomainServices;
using ShoppingBasket.Core.Interfaces;
using ShoppingBasket.Core.Repositories;
using ShoppingBasket.Infrastructure.ApplicatonServices;
using ShoppingBasket.Infrastructure.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace ShoppingBasket.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //injecting services here.
            services.AddSingleton<IBasketRepository, InMemoryBasketRepository>();
            services.AddSingleton<ICatalogService, InMemoryCatalogService>();
            services.AddScoped<IBasketService, BasketService>();

            // Adding OpenApi tools in order to self document project
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new Info {
                        Title = "My Shopping Basket Api",
                        Version = "v1"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket Api V.1");
            });

            app.UseMvc();
        }
    }
}
