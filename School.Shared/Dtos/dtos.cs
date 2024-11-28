namespace Shared.Dtos
{
    public class ClassAverageGradeDto
    {
        public int ClassId { get; set; }
        public required string Teacher { get; set; }
        public List<DinosaurAverageGradeDto> Dinosaurs { get; set; }
    }
    
    public class DinosaurAverageGradeDto
    {
        public required string Name { get; set; }
        public Double AverageScore { get; set; }
    }
    
    public class ClassWithGradeRangeDto
    {
        public int ClassId { get; set; }
        public required string Teacher { get; set; }
        public required List<DinosaurWithGradeRangeDto> Dinosaurs { get; set; }
    }
    
    public class DinosaurWithGradeRangeDto
    {
        public required string Name { get; set; }
        public int? HighestScore { get; set; }
        public int? LowestScore { get; set; }
    }
    
    public class DinosaurScoresDto
    {
        public required string Month { get; set; }
        public int? Score { get; set; }
    }
    
    public class DinoDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required List<DinosaurScoresDto> Scores { get; set; }
    }
}

  
