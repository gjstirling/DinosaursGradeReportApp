using School.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace School.Data;
public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=../SchoolData.db", b =>
            b.MigrationsAssembly("School.Data"));
    }

    public DbSet<Dinosaur> Dinosaurs { get; set; }
    public DbSet<DinoClass> DinoClass { get; set; }
    public DbSet<Scores> Scores { get; set; }
}
