
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Project2.DataAccess.Entities;
using Project2.DataAccess.Entities.Repo;
using Project2.DataAccess.Entities.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api
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
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            
            var connectionString = Configuration.GetConnectionString("default");
            if (connectionString is null)
            {
                throw new InvalidOperationException("no connection string 'default' found");
            }
            services.AddDbContext<Project2Context>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ICardRepo, CardRepo>();
            services.AddScoped<IPackRepo, PackRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<ITradeRepo, TradeRepo>();
            services.AddScoped<IAuctionRepo, AuctionRepo>();
            services.AddScoped<IStoreRepo, StoreRepo>();

            services.AddControllers(options =>
            {
             
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
           
                options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());

                options.ReturnHttpNotAcceptable = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PokeCard.Api", Version = "v1" });
            });
			
			services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200",
                            "https://pokemon-auct-api.azurewebsites.net", "https://pokemon-auct-app.azurewebsites.net")
                            .AllowAnyMethod() // allow PUT & DELETE not just GET & POST
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });


            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project2.Api v1"));
            }


            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project2.Api v1"));


            app.UseHttpsRedirection();

            app.UseRouting();
			
			app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
