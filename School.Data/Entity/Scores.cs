namespace School.Data.Entity;

public class Scores
{
    public int Id { get; set; }
    public string Month { get; set; }
    public int? Score { get; set; }
    public int DinosaurId { get; set; } 
    public Dinosaur Dinosaur { get; set; } = null!;
}