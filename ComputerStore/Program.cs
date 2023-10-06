using ComputerStore.Data;
using ComputerStore.Models;
using ComputerStore.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
            //await SeedRoles(app); (done)
            await SeedDefaultCategory(app);
            //SeedDataToDb(app); (undone)
            app.Run();
        }

        private static async Task SeedDefaultCategory(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var categoriesRepository = scope.ServiceProvider.GetRequiredService<IRepository<Category>>();
                var result = await categoriesRepository.Get(category => category.Name == "Other");
                if (result.Count < 1)
                {
                    var defaultCategory = new Category
                    {
                        Name = "Other",
                        ThumbnailImageUri = "none"
                    };
                    await categoriesRepository.Add(defaultCategory); 
                }
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
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<IRepository<Item>, ItemsRepository>();
            builder.Services.AddScoped<IRepository<Category>, CategoriesRepository>();

            builder.Services.AddRazorPages();

            builder.Services.AddControllersWithViews();
        }
        private static void SeedDataToDb(WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var cat1 = new Models.Domains.Category
                {
                    Name = "RAM",
                    ThumbnailImageUri = "PathHere"
                };
                if (dbContext.Categories.Where(c => c.Name == cat1.Name).ToList().Count <= 0)
                {
                    dbContext.Categories.Add(cat1);
                }

                var cat2 = new Models.Domains.Category
                {
                    Name = "CPU",
                    ThumbnailImageUri = "PathHere"
                };
                if (dbContext.Categories.Where(c => c.Name == cat2.Name).ToList().Count <= 0)
                {
                    dbContext.Categories.Add(cat2);
                }


                // get category from db
                var categoryRAM = dbContext.Categories.Where(c => c.Name == "RAM").FirstOrDefault();
                var categoryCPU = dbContext.Categories.Where(c => c.Name == "CPU").FirstOrDefault();

                // seed CPU category
                if(categoryCPU != null)
                {
                    dbContext.Items.Add(new Models.Domains.Item
                    {
                        Category = categoryCPU,
                        Name = "AMD Ryzen 5 3600",
                        Description = "AMD Ryzen 5 3600 (100-000000031) - ������������ ��������," +
                        " ���� ����� ����� ��� ��������� ������������ ������� � ������� ������." +
                        " ³� ��������� �� ����� 7 �� ���������� � �������������� Zen 2." +
                        " ������� ����� ������� �������� �������� ������������� AMD Ryzen 5 3600 (100-000000031)," +
                        " � ����� ������� ���������������� � �������������. �������� �� 6 ���� � 12 ������." +
                        " �� ������ ������� ���� 3,6 ��� � � ����� �����-����� ������ ������������ �� 4,2 ���." +
                        " ��������� ��'�� ���� L3 ��������� ��������� 32 ��. ��� �� �������� AMD Ryzen 5 3600 (100-000000031)" +
                        " �������� ������� ����� ������������� �� � �������� ���-�����, ��� � ���������� ������� ���������",
                        Price = 3080,
                        ImageUri = "PathHere"
                    });
                    dbContext.Items.Add(new Models.Domains.Item
                    {
                        Category = categoryCPU,
                        Name = "AMD Ryzen 7 5800X3D",
                        Description = @"�������� ������� ����: 8
ʳ������ ������: 16
��������� ���������: 7 ��
��������� TDP: 105 ��",
                        Price = 13560,
                        ImageUri = "PathHere"
                    });
                    dbContext.Items.Add(new Models.Domains.Item
                    {
                        Category = categoryCPU,
                        Name = "Intel Core i5-12400F",
                        Description = "Intel Core i5-12400F (BX8071512400F) � �������� ���������� ���� � �����" +
                        " 12-�� �������� ����� ������ Intel, �� ������� ������ Alder Lake � �� ����� ����������" +
                        " Intel 7. ³� �� ���'�� Socket 1700 � ����� ����� �� ������ ��� �������� ��� ��������" +
                        " ����'�����. ������ F � ���� Intel Core i5-12400F (BX8071512400F) ������ ���������" +
                        " ����������� ���������� ������, �� �������� �������� ��������� ��� ������������� �����������," +
                        " ��������� ��������������, � ����� ���� ����� ������ ��������� ��� ������� �������." +
                        " �������� ������� 6 ����, �� �������� � 12 �������. ���� ������ ������� ������� ��������� 2,5 ���," +
                        " � � ����� Turbo Boost ������ ������������ �� 4,4 ���. ��'�� ���-���'�� �������� ���� ��������� 18 ��." +
                        " ������������ ��'�� ���������� ���'�� DDR4 �� DDR5, ���� ������� Intel Core i5-12400F (BX8071512400F)" +
                        " � 128 ��",
                        Price = 6604,
                        ImageUri = "PathHere"
                    });
                }

                // seed RAM category
                if(categoryRAM != null)
                {
                    dbContext.Items.Add(new Models.Domains.Item
                    {
                        Category = categoryRAM,
                        Name = "Corsair VENGEANCE RGB PRO DDR4 16GB (2x8GB) 3200MHz",
                        Description = @"Brand	Corsair
Computer Memory Size	16 GB
RAM Memory Technology	DDR4
Memory Speed	3200 MHz",
                        Price = 2572
                    });
                    dbContext.Items.Add(new Models.Domains.Item
                    {
                        Category = categoryRAM,
                        Name = "TEAMGROUP T-Force Delta RGB DDR4 32GB (2x16GB) 3600MHz Desktop Memory (Black)",
                        Description = @"Brand	TEAMGROUP
Computer Memory Size	32 GB
RAM Memory Technology	DDR4
Memory Speed	3600 MHz",
                        Price = 4152
                    });
                }
            }
        }
    }
}
