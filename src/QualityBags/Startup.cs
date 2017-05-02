using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QualityBags.Data;
using QualityBags.Models;
using QualityBags.Services;
using Microsoft.AspNetCore.Identity;

namespace QualityBags
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<ShoppingContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config=>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }


        //Create super user at the beginning
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            /*Create roles for system*/
            string[] Roles = { "Admin", "Customer" };
            IdentityResult roleResult;
            foreach(var role in Roles)
            {
                var roleExistance = await RoleManager.RoleExistsAsync(role);
                if (!roleExistance)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(role));
                }
            }

            /*Create super user and assign it to role 'Admin'*/
            var _user = await UserManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["Email"]);
            if(_user == null)
            {
                var password = Configuration.GetSection("UserSettings")["Password"];
                var superUser = new ApplicationUser
                {
                    FirstName = "Junlong",
                    LastName = "Wang",
                    UserName = Configuration.GetSection("UserSettings")["Email"],
                    Email = Configuration.GetSection("UserSettings")["Email"],
                    EmailConfirmed = true,
                    Address="Quality Bags New Zealand",
                    PhoneMobile = "021111000",
                    Enabled = true
                };
                var createSuperUserResult = await UserManager.CreateAsync(superUser, password);
                if (createSuperUserResult.Succeeded)
                {
                    await UserManager.AddToRoleAsync(superUser, "Admin");
                }
            }


        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env,
                        ILoggerFactory loggerFactory, ApplicationDbContext context,
                        UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitialiser.Initialise(context);
            await CreateRoles(serviceProvider);

            /*Assign other users to 'Customer' category*/
            ICollection<ApplicationUser> appUsers = context.ApplicationUsers.ToList();
            if (appUsers.Count > 0)
            {
                foreach(var user in appUsers)
                {
                    var roleCount = userManager.GetRolesAsync(user).Result.Count();
                    if (roleCount < 1)//If user doesn't have a role 
                    {
                        await userManager.AddToRoleAsync(user, "Customer");
                    }
                }
            }
        }
    }
}
