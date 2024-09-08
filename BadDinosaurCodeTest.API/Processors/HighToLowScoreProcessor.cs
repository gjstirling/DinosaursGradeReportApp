using BadDinosaurCodeTest.Data.Entity;

namespace BadDinosaurCodeTest.API.Processors;
public class HighToLowScoreProcessor
{
    public static List<ClassWithGradeRangeDto> Process(List<DinoClass> data)
    {
        return data.Select(c => new ClassWithGradeRangeDto
        {
            ClassId = c.Id,
            Teacher = c.Teacher,
            Dinosaurs = c.Dinosaurs.Select(d => new DinosaurWithGradeRangeDto
            {
                DinosaurId = d.Id,
                Name = d.Name,
                Type = d.Type,
                HighestScore = d.Scores.Max(s => s.Score),  
                LowestScore = d.Scores.Min(s => s.Score)   
            }).ToList()
        }).ToList();
    }
    
}

public class ClassWithGradeRangeDto
{
    public int ClassId { get; set; }
    public string Teacher { get; set; }
    public List<DinosaurWithGradeRangeDto> Dinosaurs { get; set; }
}

public class DinosaurWithGradeRangeDto
{
    public int DinosaurId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int? HighestScore { get; set; }
    public int? LowestScore { get; set; }
}