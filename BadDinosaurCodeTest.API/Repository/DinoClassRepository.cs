using BadDinosaurCodeTest.Data;
using BadDinosaurCodeTest.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace BadDinosaurCodeTest.API.Repository;

public class DinoClassRepository
{
    private static List<DinoClass> GetData(DataContext dbContext, Boolean excludeNull) {
        var data = dbContext.DinoClass
            .Include(c => c.Dinosaurs)
            .ThenInclude(d => d.Scores)
            .ToList();
        
        if (excludeNull) {
            return data.Select(c => new DinoClass
            {
                Id = c.Id,
                Teacher = c.Teacher,
                Dinosaurs = c.Dinosaurs
                    .Where(d => d.Scores.All(s => s.Score != null)) 
                    .ToList()
            })
            .Where(c => c.Dinosaurs.Any()) 
            .ToList();
            
        } return data;
    }
    
    public static List<DinoClass> CollectScores(IServiceProvider services, Boolean excludeNull)
    {
            var contextFactory = services.GetRequiredService<IDbContextFactory<DataContext>>();
            using (var context = contextFactory.CreateDbContext())
            {
                return GetData(context, excludeNull);
            }
    }
}



