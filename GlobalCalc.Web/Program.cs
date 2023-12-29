using GlobalCalc.Web.Middleware;
using GlobalCalc.DataLayer;

namespace GlobalCalc.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddControllersWithViews()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.Converters.Add(new Converters.JsonDateTimeConverter());
                });
            builder.Services.AddTransient<Services.AuthorizationService>();
            builder.Services.AddScoped<DataContext>();

            var app = builder.Build();
            app.UseMiddleware<AuthorizationMiddleware>();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}