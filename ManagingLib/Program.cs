using ManagingLib.DAL.Context;
using ManagingLib.DAL.Models;
using ManagingLib.Mapping_Profiles;
using MangaingLib.BLL.Interfaces;
using MangaingLib.BLL.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ManagingLib
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ManagingContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));

            });
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Option => {
                Option.Password.RequireNonAlphanumeric = true;
                Option.Password.RequireLowercase = true;
                Option.Password.RequireUppercase = true;
                Option.Password.RequireDigit = true;
            }).AddEntityFrameworkStores<ManagingContext>().AddDefaultTokenProviders();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(Option =>
                {
                    Option.LoginPath = "Account/Login";
                    Option.AccessDeniedPath = "Home/Error";

                });
            builder.Services.AddAutoMapper(M => M.AddProfile(new AuthorProfile()));


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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}