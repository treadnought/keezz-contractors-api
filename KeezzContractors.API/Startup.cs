﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using KeezzContractors.API.Services;
using Microsoft.Extensions.Configuration;
using KeezzContractors.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace KeezzContractors.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connectionString = Startup.Configuration["connectionStrings:keezzContractorsDBConnectionString"];
            services.AddDbContext<KeezzContractorsContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IKeezzContractorsRepository, KeezzContractorsRepository>();

#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            KeezzContractorsContext keezzContractorsContext)
        {
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            keezzContractorsContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            app.UseMvc();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Contractor, Models.ContractorDto>().ReverseMap();
                cfg.CreateMap<Entities.Contractor, Models.ContractorWithoutInvoicesDto>().ReverseMap();
                cfg.CreateMap<Entities.Contractor, Models.ContractorForCreationDto>().ReverseMap();
                cfg.CreateMap<Entities.Contractor, Models.ContractorForUpdateDto>().ReverseMap();
                cfg.CreateMap<Entities.ContractorInvoice, Models.ContractorInvoiceForCreationDto>().ReverseMap();
                cfg.CreateMap<Entities.ContractorInvoice, Models.ContractorInvoiceForUpdateDto>().ReverseMap();
                cfg.CreateMap<Entities.Expense, Models.ExpenseDto>().ReverseMap();
                cfg.CreateMap<Entities.Expense, Models.ExpenseForCreationDto>().ReverseMap();
                cfg.CreateMap<Entities.Expense, Models.ExpenseForUpdateDto>().ReverseMap();
            });
        }
    }
}
