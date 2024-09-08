using BadDinosaurCodeTest.Data.Entity;
using BadDinosaurCodeTest.Data;
using Microsoft.EntityFrameworkCore;

namespace BadDinosaurCodeTest.API.Repository;

public class DinoRepository
{
    public static List<Dinosaur> Find(IServiceProvider services, String name)
    {
        var contextFactory = services.GetRequiredService<IDbContextFactory<DataContext>>();
        using (var context = contextFactory.CreateDbContext())
        {
            var nameCaseChecked = char.ToUpper(name[0]) + name.Substring(1).ToLower();
            
            return context.Dinosaurs
                .Where(d => d.Name == nameCaseChecked)
                .Include(d => d.Scores)
                .ToList();
        }
    }

}