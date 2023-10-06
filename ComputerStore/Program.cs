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
                        Description = "AMD Ryzen 5 3600 (100-000000031) - універсальний процесор," +
                        " який добре підійде для створення продуктивних ігрових і робочих систем." +
                        " Він виконаний на основі 7 нм техпроцесу і мікроархітектурі Zen 2." +
                        " Завдяки цьому вдалося підвищити загальну продуктивність AMD Ryzen 5 3600 (100-000000031)," +
                        " а також знизити енергоспоживання і тепловиділення. Процесор має 6 ядер і 12 потоків." +
                        " Їх базова частота сягає 3,6 ГГц і в режимі турбо-буста здатна підвищуватися до 4,2 ГГц." +
                        " Загальний об'єм кеша L3 процесора становить 32 МБ. Все це дозволяє AMD Ryzen 5 3600 (100-000000031)" +
                        " радувати високим рівнем продуктивності як в сучасних ААА-іграх, так і вимогливих робочих завданнях",
                        Price = 3080,
                        ImageUri = "PathHere"
                    });
                    dbContext.Items.Add(new Models.Domains.Item
                    {
                        Category = categoryCPU,
                        Name = "AMD Ryzen 7 5800X3D",
                        Description = @"Загальна кількість ядер: 8
Кількість потоків: 16
Виробнича технологія: 7 нм
Потужність TDP: 105 Вт",
                        Price = 13560,
                        ImageUri = "PathHere"
                    });
                    dbContext.Items.Add(new Models.Domains.Item
                    {
                        Category = categoryCPU,
                        Name = "Intel Core i5-12400F",
                        Description = "Intel Core i5-12400F (BX8071512400F) – процесор середнього рівня в лінійці" +
                        " 12-го покоління рішень компанії Intel, під кодовою назвою Alder Lake і на основі техпроцесу" +
                        " Intel 7. Він має роз'єм Socket 1700 і добре підійде як основа для ігрового або робочого" +
                        " комп'ютера. Індекс F у назві Intel Core i5-12400F (BX8071512400F) означає відсутність" +
                        " вбудованого графічного рішення, що дозволяє зменшити нагрівання при максимальному навантаженні," +
                        " покращити тепловідведення, а також мати трохи більший потенціал для ручного розгону." +
                        " Процесор отримав 6 ядер, що працюють у 12 потоках. Його базова тактова частота становить 2,5 ГГц," +
                        " а в режимі Turbo Boost здатна підвищуватися до 4,4 ГГц. Об'єм кеш-пам'яті третього рівня становить 18 МБ." +
                        " Максимальний об'єм оперативної пам'яті DDR4 та DDR5, який підтримує Intel Core i5-12400F (BX8071512400F)" +
                        " – 128 ГБ",
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
