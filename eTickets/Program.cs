using eTickets.Data;
using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        //DbContext configuration
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Data Source=HESHAM-PC\\HSQLSERVER;Initial Catalog=EcommerceDB;Integrated Security=True;TrustServerCertificate=True"));
        //builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Data Source=SQL8006.site4now.net;Initial Catalog=db_aaa4cc_eticketsdb;User Id=db_aaa4cc_eticketsdb_admin;Password=01017424710Ha"));


        //services
        builder.Services.AddScoped<IActorService, ActorService>();
        builder.Services.AddScoped<IProducersService, ProducersService>();
        builder.Services.AddScoped<ICinemasService, CinemasService>();
        builder.Services.AddScoped<IMoviesService, MoviesService>();
        builder.Services.AddScoped<IOrdersService, OrdersService>();


        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

        //Authentication && Authorization
        builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
        builder.Services.AddMemoryCache();
        builder.Services.AddSession();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme=CookieAuthenticationDefaults.AuthenticationScheme;
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

        //Authentication && Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        AppDbInitializer.Seed(app);
        AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
        app.Run();


    }
}