using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebApi.Services.FunctionService;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
            FunctionService ass = new FunctionService("host=localhost;db=imdb;uid=postgres;pwd=SarahPalin");
            ass.BestMatchSearch("apple");
            */
            DotNetEnv.Env.Load();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}