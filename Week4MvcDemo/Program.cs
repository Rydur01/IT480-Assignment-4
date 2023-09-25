using Microsoft.EntityFrameworkCore;
using StoreDatabase.Data;
using System.Text.Json.Serialization;

namespace Week4MvcDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<StoreContext>(optionsBuilder =>
            {
                optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("StoreDB"));
            });

            builder.Services.AddControllersWithViews()
                .AddJsonOptions( jsonOptions =>
                {
                    // System.Text.Json
                    jsonOptions.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

                    //Newtonsoft.Json
                    //jsonOptions => options.SerializerSettings.RefenceLoopHandling = Newtonsoft.Json.Re...
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Customers}/{action=ChooseCustomer}");

            app.Run();
        }
    }
}