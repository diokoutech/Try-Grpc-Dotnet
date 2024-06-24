using Microsoft.EntityFrameworkCore;

namespace GrpcService.Database
{
    public class GrpcContext : DbContext
    {

    public DbSet<Models.Customer> Customers { get; set; }

    public string DbPath { get; }

    public GrpcContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "GrpcData.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    }
}
