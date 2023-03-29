using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using MultitenacyDemo.Domains;
using MultitenacyDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMultiTenant<TenantInfo>().WithHeaderStrategy("tenant").WithConfigurationStore();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration["Finbuckle:MultiTenant:Stores:ConfigurationStore:Defaults:ConnectionString"]);
});

// builder.Services.AddDbContext<AppDbContext>();

builder.Services.SetupDb(builder.Configuration);

var app = builder.Build();

app.UseMultiTenant();

app.UseAuthorization();

app.MapGet("/", () => { return "Welcome to multitenant demo. Please use tenant={tanantid} in header. tanantid are [apple,samaung,mi,nokia]"; });
app.MapControllers();

app.Run();






 

