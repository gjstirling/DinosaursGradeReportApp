namespace BadDinosaurCodeTest.Data.Entity;

public class Scores
{
    public int Id { get; set; }
    public string Date { get; set; }
    public int? Score { get; set; }
    public int DinosaurId { get; set; } // Required foreign key property
    public Dinosaur Dinosaur { get; set; } = null!;
}