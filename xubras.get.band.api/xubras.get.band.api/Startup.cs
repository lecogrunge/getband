using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;
using xubras.get.band.api.Core.Compression;
using xubras.get.band.api.Exceptions;
using xubras.get.band.api.Security;
using xubras.get.band.data.Persistence.EF;
using xubras.get.band.data.Transactions;
using xubras.get.band.domain.Business;
using xubras.get.band.domain.Contract.Business;
using xubras.get.band.domain.Contract.Repository;
using xubras.get.band.domain.Contract.Services;
using xubras.get.band.domain.Repository;
using xubras.get.band.domain.Services;
using xubras.get.band.domain.Util;

namespace xubras.get.band.api
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
            #region Compression
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
            });
            #endregion

            #region Mysql Connection
            string connection = Configuration.GetConnectionString("SqlConnection");
            services.AddDbContext<GetBandContext>(o => o.UseMySql(connection));
            #endregion

            #region injections
            // Services
            services.AddScoped<GetBandContext, GetBandContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IBusinessUser, BusinessUser>();
            services.AddTransient<IEmailService, EmailService>();
            //services.AddTransient<IBandService, BandService>();

            // Repositories
            services.AddTransient<IUserSaveRepository, UserSaveRepository>();
            services.AddTransient<IBandSaveRepository, BandSaveRepository>();
            #endregion

            services.AddCors();
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    });

            // Leitura de arquivo de configuração
            services.Configure<Configuration>(Configuration.GetSection("Configuration"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "getBandAPI", Version = "V1", Description = "Api site de recrutamento e interação de bandas " });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.OperationFilter<ApplySwaggerImplementationNotesFilterAttributes>();
                c.IgnoreObsoleteProperties();
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpStatusCodeExceptionMiddleware();
            }
            else
            {
                app.UseHttpStatusCodeExceptionMiddleware();
                app.UseExceptionHandler();
            }

            
            app.UseResponseCompression();

            #region Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GetBand V1");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "GetBand Documentation";
                c.DocExpansion(DocExpansion.None);
                c.SwaggerEndpoint(Configuration.GetValue<string>("SwaggerEndpoint"), "getBand Web API");
                c.RoutePrefix = string.Empty;
            });
            #endregion

            app.UseMvc();
        }
    }
}