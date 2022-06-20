using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Time_tracking_system.DAL;
using Time_tracking_system.DAL.Interfaces;
using Time_tracking_system.DAL.Repositories;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.Mappings;
using Time_tracking_system.Service.Implementations;
using Time_tracking_system.Service.Interfaces;

namespace Time_tracking_system
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IAttributes_TypeRequestRepository, Attributes_TypeRequestRepository>();
            services.AddScoped<IAttributesRepository, AttributesRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<ITypeRequestRepository, TypeRequestRepository>();
            services.AddScoped<ITypeVocationRepository, TypeVocationRepository>();
            services.AddScoped<IValueRequestAttributesRepository, ValueRequestAttributesRepository>();
            services.AddScoped<IStatusRequestRepository, StatusRequestRepository>();
            services.AddScoped<IHolidayCalendarRepository, HolidayCalendarRepository>();
            services.AddScoped<IYearsRepository, YearsRepository>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<ICalendarService, CalendarService>();

            services.AddAutoMapper(typeof(Maps));

            services.AddDefaultIdentity<Employee>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            SeedData.Seed(userManager, roleManager, db);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Employee}/{action=Edit}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
