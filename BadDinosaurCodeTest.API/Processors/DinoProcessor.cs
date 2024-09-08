using BadDinosaurCodeTest.Data.Entity;

namespace BadDinosaurCodeTest.API.Processors;

public class DinoProcessor
{
    
    public static List<DinoDto> Process(List<Dinosaur> data)
    {
        return data.Select(dino => new DinoDto
        {
            Id = dino.Id,
            Name = dino.Name,
            Scores = dino.Scores.Select(score => new DinosaurScoresDto
            {
                Month = score.Date,
                Score = score.Score
            }).ToList()
        }).ToList();
    }
    
}

public class DinoDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<DinosaurScoresDto> Scores { get; set; }
}
public class DinosaurScoresDto
{
    public string Month { get; set; }
    public int? Score { get; set; }
}