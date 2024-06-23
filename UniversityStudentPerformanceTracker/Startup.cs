using Microsoft.AspNetCore.Authentication.Cookies;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "YourAppCookieName";
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Adjust expiration time as needed
            options.LoginPath = "/Home/Login"; // Specify the login path
            options.AccessDeniedPath = "/Home/AccessDenied"; // Specify the access denied path
            options.SlidingExpiration = true;
        });
    
        services.AddControllersWithViews();
        // Add other services as needed
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
