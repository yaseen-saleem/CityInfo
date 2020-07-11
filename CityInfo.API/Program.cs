using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Contexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace CityInfo.API
{
    public class Program
    {
        //Responsible for configuring and running app
        public static void Main(string[] args)
        {
            
            try
            {
                var host = CreateWebHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    try
                    {
                        var context = scope.ServiceProvider.GetService<CityInfoContext>();
                    }

                    catch (Exception)
                    {
                        throw new Exception("Context Not Found");
                    }
                }

                // run the web app
                host.Run();
            }
            catch (Exception)
            {
                throw new Exception("Application stopped because of exception.");
                
            }
            

        }

        //as it is is a Web App so we are hosting this app using IWebHostBuilder
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args) //It uses kestrel as a web server
                .UseStartup<Startup>();
                
    }
}
