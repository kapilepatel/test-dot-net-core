using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using log4net;
using log4net.Config;
using System.Reflection;

namespace MS_Authentication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //configure log4net file
            //add file log4net.config.xml in each microservice api project
            var logRepo = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepo, new FileInfo("log4net.config.xml"));

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("appsettings.json");
                    config.AddEnvironmentVariables();
                })
                .UseStartup<Startup>();
    }
}