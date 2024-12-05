namespace School.Data.Entity;

public class DinoClass
{ 
    public int Id { get; set; }
    public int Number { get; set; }
    public string Teacher { get; set; }
    public ICollection<Dinosaur> Dinosaurs { get; set; } = new List<Dinosaur>();
}