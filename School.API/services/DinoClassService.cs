using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Data.Entity;
using Shared.Dtos;

namespace School.API.Services;

public interface IDinoClassService
{
    Task<List<ClassAverageGradeDto>> Average(bool excludeNull);
    Task<List<ClassWithGradeRangeDto>> Range(bool excludeNull);
}

public class DinoClassService : IDinoClassService
{
    private readonly IServiceProvider _services;

    public DinoClassService(IServiceProvider services)
    {
        _services = services;
    }

    private async Task<List<DinoClass>> GetClassesAsync(bool excludeDinosWithMissingScores)
    {
        var contextFactory = _services.GetRequiredService<IDbContextFactory<DataContext>>();
        using (var context = contextFactory.CreateDbContext())
        {
            var query = context.DinoClass
                .Include(c => c.Dinosaurs)
                .ThenInclude(d => d.Scores);

            var data = await query.ToListAsync();

            if (excludeDinosWithMissingScores)
            {
                data = data.Select(c => new DinoClass
                {
                    Id = c.Id,
                    Teacher = c.Teacher,
                    Dinosaurs = c.Dinosaurs
                        .Where(d => d.Scores.All(s => s.Score.HasValue))
                        .ToList()
                })
                .Where(c => c.Dinosaurs.Any())
                .ToList();
            }

            return data;
        }
    }

    public async Task<List<ClassAverageGradeDto>> Average(bool excludeNull)
    {
        var data = await GetClassesAsync(excludeNull);

        var filteredData = data.Select(c => new ClassAverageGradeDto
        {
            ClassId = c.Id,
            Teacher = c.Teacher,
            Dinosaurs = c.Dinosaurs
                .Select(d => new DinosaurAverageGradeDto
                {
                    Name = d.Name,
                    AverageScore = Math.Round(d.Scores
                        .Where(s => s.Score.HasValue)
                        .Select(s => s.Score.Value)
                        .DefaultIfEmpty()
                        .Average(), 2)
                }).ToList()
        })
        .Where(c => c.Dinosaurs.Any())
        .ToList();

        return filteredData;
    }

    public async Task<List<ClassWithGradeRangeDto>> Range(bool excludeNull)
    {
        var data = await GetClassesAsync(excludeNull);

        return data.Select(c => new ClassWithGradeRangeDto
        {
            ClassId = c.Id,
            Teacher = c.Teacher,
            Dinosaurs = c.Dinosaurs
                .Select(d => new DinosaurWithGradeRangeDto
                {
                    Name = d.Name,
                    HighestScore = d.Scores.Where(s => s.Score.HasValue).Max(s => s.Score),
                    LowestScore = d.Scores.Where(s => s.Score.HasValue).Min(s => s.Score)
                }).ToList()
        }).ToList();
    }
}
