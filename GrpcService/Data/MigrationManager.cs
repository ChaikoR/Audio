using GrpcService.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Data
{
    public static class MigrationManager
    {
        public static void PrepPopulation(IApplicationBuilder app) 
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<MessagesDBConext>());
            }
        }

        public static void SeedData(MessagesDBConext context) 
        {
            System.Console.WriteLine("Запуск Migrations...");

            context.Database.Migrate();

            if (!context.Messages.Any())
            {
                System.Console.WriteLine("Данные добавляем в ДБ...");

                context.Messages.AddRange(
                    new Messages() {Name="Сообщение 1" },    
                    new Messages() {Name="Сообщение 2" },    
                    new Messages() {Name="Сообщение 3" }    
                );
                context.SaveChanges();

                System.Console.WriteLine("Данные добавлены в ДБ...");
            }
            else 
            {
                System.Console.WriteLine("Данные уже загружены...");
            }
        }
        //public static IHost MigrateDatabase(this IHost host)
        //{
        //    using (var scope = host.Services.CreateScope())
        //    {
        //        using (var appContext = scope.ServiceProvider.GetRequiredService<MessagesDBConext>())
        //        {
        //            try
        //            {
        //                appContext.Database.Migrate();
        //            }
        //            catch (Exception ex)
        //            {
        //                //Log errors or do anything you think it's needed
        //               // throw;
        //            }
        //        }
        //    }
        //    return host;
        //}
    }
}
