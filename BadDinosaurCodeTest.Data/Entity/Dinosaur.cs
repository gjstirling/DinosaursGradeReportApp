﻿namespace BadDinosaurCodeTest.Data.Entity;
public class Dinosaur
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public ICollection<Scores> Scores { get; } = new List<Scores>();
}
