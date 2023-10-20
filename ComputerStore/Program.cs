using ComputerStore.BusinessLogic.Interfaces;
using ComputerStore.BusinessLogic.Services;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Interfaces;
using ComputerStore.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerStore
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            ConfigureHttpRequest(app);
            await SeedRoles(app);
            //await SeedDefaultCategory(app);
            app.Run();
        }

        private static async Task SeedDefaultCategory(WebApplication app)
        {

            using (var scope = app.Services.CreateScope())
            {
                /*
                var categoriesRepository = scope.ServiceProvider.GetRequiredService<IRepository<Category>>();
                var result = await categoriesRepository.Get(category => category.Name == "Other");
                if (result.Count < 1)
                {
                    var defaultCategory = new Category
                    {
                        Name = "Other",
                    };
                    await categoriesRepository.Add(defaultCategory); 
                }
                */
            }
        }

        private static async Task SeedRoles(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { RolesContainer.MANAGER, RolesContainer.ADMINISTRATOR };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static void ConfigureHttpRequest(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("PostgreConnectionString") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<DataAccess.ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataAccess.ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IItemsRepository, ItemsRepository>();
            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
            builder.Services.AddScoped<IUserCartRepository, UserCartRepository>();
            builder.Services.AddScoped<IImagesRepository, ImagesRepository>();

            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<IItemsService, ItemsService>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            builder.Services.AddScoped<ICartService, CartService>();
        }
    }
}
