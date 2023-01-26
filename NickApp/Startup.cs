using Microsoft.Extensions.DependencyInjection;
using System;
using NickApp.Services;
using NickApp.ViewModels;
using System.Net.Http;

namespace NickApp
{
    public static class Startup
    {
        private static IServiceProvider serviceProvider;
        public static void ConfigureServices()
        {
            var services = new ServiceCollection();

            //add services
            services.AddHttpClient<IIncidentService, IncidentService>(c => 
            {
                c.BaseAddress = new Uri("http://localhost:8090/api/");

                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });


            services.AddHttpClient<IUserAccountService, UserAccountService>(c =>
            {
                c.BaseAddress = new Uri("http://localhost:8090/api/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });


            //add viewmodels

            services.AddTransient<SignInPageViewModel>();
            services.AddTransient<SignUpPageViewModel>();
            services.AddTransient<IncidentPageViewModel>();
          

            

            serviceProvider = services.BuildServiceProvider();
        }

        public static T Resolve<T>() => serviceProvider.GetService<T>();
    }
}
