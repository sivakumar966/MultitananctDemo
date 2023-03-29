using Finbuckle.MultiTenant;
using MultitenacyDemo.Domains;
using MultitenacyDemo.Dtos;

namespace MultitenacyDemo.Services;

public static class DbMigrationExtensions
{
    public static void SetupDb(this IServiceCollection services, IConfiguration config)
    {


        var options = services.GetOptions<TenantSettings>("Finbuckle:MultiTenant:Stores:ConfigurationStore");


        foreach (var tenant in options.Tenants)
        {
          
            TenantInfo tenantInfo = new TenantInfo();
            tenantInfo.ConnectionString = tenant.ConnectionString == null ? options.Defaults.ConnectionString : tenant.ConnectionString;
            tenantInfo.Name = tenant.Name;
            tenantInfo.Identifier = tenant.Identifier;
            tenantInfo.Id = tenant.Id;

            using (AppDbContext dbContext = new AppDbContext(tenantInfo))
            {
                dbContext.Database.EnsureCreated();
                switch (tenant.Identifier)
                {
                    case "apple":
                        if (!dbContext.Products.Any(x => x.Name == "IPhone 13"))
                        {
                            Product p = new Product() { Name = "IPhone 13" };
                            dbContext.Products.Add(p);
                            dbContext.SaveChanges();
                        }
                        break;
                    case "samsung":
                        if (!dbContext.Products.Any(x => x.Name == "Galaxy S23"))
                        {
                            Product p = new Product() { Name = "Galaxy S23" };
                            dbContext.Products.Add(p);
                            dbContext.SaveChanges();
                        }

                        break;
                    case "mi":
                        if (!dbContext.Products.Any(x => x.Name == "Redmi Note 10"))
                        {
                            Product p = new Product() { Name = "Redmi Note 10" };
                            dbContext.Products.Add(p);
                            dbContext.SaveChanges();
                        }
                        break;
                    case "nokia":
                        if (!dbContext.Products.Any(x => x.Name == "Nokia 1100"))
                        {
                            Product p = new Product() { Name = "Nokia 1100" };
                            dbContext.Products.Add(p);
                            dbContext.SaveChanges();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        //using var scope = services.BuildServiceProvider().CreateScope();
        //var dbContext = scope.ServiceProvider.<AppDbContext>( options => { options.UseSqlite(tenant.ConnectionString); });
        //dbContext.Database.SetConnectionString(tenant.ConnectionString);
        //if (dbContext.Database.GetMigrations().Count() > 0)
        //{
        //    dbContext.Database.Migrate();
        //}


        //    var ti = new TenantInfo { Id = "finbuckle", ConnectionString = "Data Source=Data/ToDoList.db" };
        //using (var db = new ToDoDbContext(ti))
        //{
        //    db.Database.EnsureDeleted();
        //    db.Database.EnsureCreated();
        //    db.ToDoItems.Add(new ToDoItem { Title = "Call Lawyer ", Completed = false });
        //    db.ToDoItems.Add(new ToDoItem { Title = "File Papers", Completed = false });
        //    db.ToDoItems.Add(new ToDoItem { Title = "Send Invoices", Completed = true });
        //    db.SaveChanges();
        //}

        //ti = new TenantInfo { Id = "megacorp", ConnectionString = "Data Source=Data/ToDoList.db" };
        //using (var db = new ToDoDbContext(ti))
        //{
        //    db.Database.EnsureCreated();
        //    db.ToDoItems.Add(new ToDoItem { Title = "Send Invoices", Completed = true });
        //    db.ToDoItems.Add(new ToDoItem { Title = "Construct Additional Pylons", Completed = true });
        //    db.ToDoItems.Add(new ToDoItem { Title = "Call Insurance Company", Completed = false });
        //    db.SaveChanges();
        //}

        //ti = new TenantInfo { Id = "initech", ConnectionString = "Data Source=Data/Initech_ToDoList.db" };
        //using (var db = new ToDoDbContext(ti))
        //{
        //    db.Database.EnsureDeleted();
        //    db.Database.EnsureCreated();
        //    db.ToDoItems.Add(new ToDoItem { Title = "Send Invoices", Completed = false });
        //    db.ToDoItems.Add(new ToDoItem { Title = "Pay Salaries", Completed = true });
        //    db.ToDoItems.Add(new ToDoItem { Title = "Write Memo", Completed = false });
        //    db.SaveChanges();
        //}
    }


    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var section = configuration.GetSection(sectionName);
        var options = new T();
        section.Bind(options);
        return options;
    }


    //public static IServiceCollection AddAndMigrateTenantDatabases(this IServiceCollection services, IConfiguration config)
    //{
    //    var options = services.GetOptions<TenantSettings>("Finbuckle:MultiTenant:Stores:ConfigurationStore");
    //    var defaultConnectionString = options.Defaults?.ConnectionString;

    //    services.AddDbContext<AppDbContext>(m => m.UseSqlite(e => e.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

    //    var tenants = options.Tenants;
    //    foreach (var tenant in tenants)
    //    {
    //        string connectionString;
    //        if (string.IsNullOrEmpty(tenant.ConnectionString))
    //        {
    //            connectionString = defaultConnectionString;
    //        }
    //        else
    //        {
    //            connectionString = tenant.ConnectionString;
    //        }
    //        using var scope = services.BuildServiceProvider().CreateScope();
    //        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //        dbContext.Database.SetConnectionString(connectionString);
    //        if (dbContext.Database.GetMigrations().Count() > 0)
    //        {
    //            dbContext.Database.Migrate();
    //        }
    //    }
    //    return services;
    //}

}
