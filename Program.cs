using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using RazorAPI.Services;

namespace RazorAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();//.SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => options.JsonSerializerOptions = new DefaultContractResolver()) ;
            
            builder.Services.AddScoped<IServicesAPI, ServicesAPI>();
            builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}");

            app.Run();
        }
    }
}