namespace BadDinosaurCodeTest.Data.Entity;

public class Scores
{
    public int Id { get; set; }
    public string Date { get; set; }
    public int? Score { get; set; }
    public Dinosaur Dinosaur { get; set; }
    public DinoClass DinoClass { get; set; }
}