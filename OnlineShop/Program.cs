using System;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineShop.Controllers;

namespace OnlineShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<Context>();
                    var userMgr = services.GetRequiredService<UserManager<AppUser>>();
                    var roleMgr = services.GetRequiredService<RoleManager<AppRole>>();
                    var signInMgr = services.GetRequiredService<SignInManager<AppUser>>();
                    context.Database.Migrate();

                    var controller = new AccountController(context, userMgr, signInMgr, roleMgr);
                    var task = Task.Run(() => controller.CreateRolesOnce());
                    task.Wait();

                    Console.WriteLine("Users added");

                    if (DataFill.LoadData(context) && DataFill.LoadOglasi(context))
                    {
                        Console.WriteLine("Data added");
                    }
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "Desila se greska prilikom ubacivanja podataka u bazu.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
