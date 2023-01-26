using Dotmim.Sync;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NickWebApi.Models;
using NickWebApi.Repository;
using Microsoft.EntityFrameworkCore;
using Dotmim.Sync.SqlServer;
using NickWebApi.Controllers;

namespace NickWebApi
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

            services.AddControllers();

            services.AddDistributedMemoryCache();
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));

            
            // [Required]: Get a connection string to your server data source
            var connectionString = Configuration.GetConnectionString("DefaultConnection"); // Configuration.GetSection("ConnectionStrings")["SqlConnection"];


            var options = new SyncOptions
            {
                BatchDirectory = Path.Combine(SyncOptions.GetDefaultUserBatchDirectory(), "server")
            };

            // Create the setup used for your sync process
            var tables = new string[] {"Incident", "UserAccount"};

            var setup = new SyncSetup(tables)
            {
                // optional :
                StoredProceduresPrefix = "s",
                StoredProceduresSuffix = "",
                TrackingTablesPrefix = "s",
                TrackingTablesSuffix = ""
            };

            services.AddSyncServer<SqlSyncProvider>(connectionString, setup, options);


            services.AddMvc();

            services.AddCors(option => option.AddPolicy("NIckCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));

            services.AddDbContext<NickDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //services.AddDbContext<bmpContext>(options => options.UseMysql();
            //  services.AddDbContext<bmpContext>(item => item.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));

            services.AddScoped<IIncidentRepository, IncidentRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();


            // Add configuration for DbContext
            // Use connection string from appsettings.json file
            services.AddDbContext<NickDBContext>(builder =>
            {
                builder.UseSqlServer(Configuration["AppSettings:ConnectionString"]);
            });

            // Set up dependency injection for controller's logger
            services.AddScoped<ILogger, Logger<UserAccountController>>();
            services.AddScoped<ILogger, Logger<IncidentController>>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NickWebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NickWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
