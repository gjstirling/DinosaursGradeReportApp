using BadDinosaurCodeTest.Data.Entity;

namespace BadDinosaurCodeTest.API.Processors;

public class AverageScoreProcessor
{
    public static List<ClassAverageGradeDto> Process(List<DinoClass> data)
    {
        return data.Select(c => new ClassAverageGradeDto
        {
            ClassId = c.Id,
            Teacher = c.Teacher,
            Dinosaurs = c.Dinosaurs.Select(d => new DinosaurAverageGradeDto
            {
                DinosaurId = d.Id,
                Name = d.Name,
                Type = d.Type,
                AverageScore = Math.Round(d.Scores
                    .Where(s => s.Score.HasValue)
                    .Select(s => s.Score.Value)
                    .DefaultIfEmpty()
                    .Average(), 2)
            }).ToList()
        }).ToList();
    }
}


public class ClassAverageGradeDto
{
    public int ClassId { get; set; }
    public string Teacher { get; set; }
    public List<DinosaurAverageGradeDto> Dinosaurs { get; set; }
}

public class DinosaurAverageGradeDto
{
    public int DinosaurId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public Double AverageScore { get; set; }
}