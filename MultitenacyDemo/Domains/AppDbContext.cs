using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MultitenacyDemo.Domains;

public class AppDbContext : MultiTenantDbContext
{

    public AppDbContext(ITenantInfo tenantInfo) : base(tenantInfo)
    {
    }

    public AppDbContext(ITenantInfo tenantInfo, DbContextOptions options) : base(tenantInfo, options)
    {
    }


    public DbSet<Product> Products { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {


        optionsBuilder.UseSqlite(TenantInfo.ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().IsMultiTenant();
        base.OnModelCreating(modelBuilder);
    }
}
