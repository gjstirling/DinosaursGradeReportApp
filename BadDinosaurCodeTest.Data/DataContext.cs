using BadDinosaurCodeTest.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace BadDinosaurCodeTest.Data;
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
        optionsBuilder.UseSqlite("Data Source=../BadDinosaurCodeChallenge.db", b =>
            b.MigrationsAssembly("BadDinosaurCodeTest.Data"));
    }

    public DbSet<Dinosaur> Dinosaurs { get; set; }
    public DbSet<DinoClass> DinoClass { get; set; }
    public DbSet<Scores> Scores { get; set; }
}
