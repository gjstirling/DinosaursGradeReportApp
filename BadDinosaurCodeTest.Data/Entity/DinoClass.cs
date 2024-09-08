namespace BadDinosaurCodeTest.Data.Entity;

public class DinoClass
{ 
    public int Id { get; set; }
    public string Teacher { get; set; }
    public ICollection<Dinosaur> Dinosaurs { get; set; } = new List<Dinosaur>();
}