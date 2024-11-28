using Microsoft.EntityFrameworkCore;
using School.Data;
using Shared.Dtos;

namespace School.API.Services;

public interface IDinoService
{
    Task<List<DinoDto>> Get(string dinoName);
}

public class DinoService : IDinoService
{
    private readonly IServiceProvider _services;

    public DinoService(IServiceProvider services)
    {
        _services = services;
    }

    public async Task<List<DinoDto>> Get(string dinoName)
    {
        var nameCaseChecked = char.ToUpper(dinoName[0]) + dinoName.Substring(1).ToLower();
        var contextFactory = _services.GetRequiredService<IDbContextFactory<DataContext>>();
        using (var context = contextFactory.CreateDbContext())
        {
            var data = await context.Dinosaurs
                .Where(d => d.Name.Equals(nameCaseChecked))
                .Include(d => d.Scores)
                .ToListAsync();

            return data.Select(dino => new DinoDto
            {
                Id = dino.Id,
                Name = dino.Name,
                Scores = dino.Scores.Select(score => new DinosaurScoresDto
                {
                    Month = score.Date.ToString(),
                    Score = score.Score
                }).ToList()
            }).ToList();
        }
    }
}