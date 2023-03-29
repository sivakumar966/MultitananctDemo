namespace MultitenacyDemo.Dtos;

public class TenantSettings
{
    public Configuration Defaults { get; set; }
    public List<Tenant> Tenants { get; set; }
}

public class Configuration
{
    public string ConnectionString { get; set; }
}

public class Tenant
{
    public string Id { get; set; }
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string ConnectionString { get; set; }
}