using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configure data protection to use a common key storage location
        services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(@"D:\ASE\Enterprise-Application-development\cookies"))
            .SetApplicationName("SharedCookieApp");

        // Configure cookie authentication
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = ".AspNet.SharedCookie"; // Must match the first microservice
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Should be consistent with the first microservice
                options.LoginPath = "/Home/Login"; // Adjust if needed
                options.AccessDeniedPath = "/Home/AccessDenied"; // Adjust if needed
                options.SlidingExpiration = true; // Ensure this matches the first microservice

                // Add events for detailed logging
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                        logger.LogInformation("Redirecting to login page: {ReturnUrl}", context.RedirectUri);
                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                        logger.LogInformation("Redirecting to access denied page: {ReturnUrl}", context.RedirectUri);
                        return Task.CompletedTask;
                    },
                    OnValidatePrincipal = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                        logger.LogInformation("Validating principal: {Principal}", context.Principal?.Identity?.Name);
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddControllersWithViews();
        services.AddLogging(); // Ensure logging services are added
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
