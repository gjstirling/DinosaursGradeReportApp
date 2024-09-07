using CsvHelper;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Globalization;
using BadDinosaurCodeTest.Data;
using BadDinosaurCodeTest.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace BadDinosaurCodeTest.API.DataInitializer;

public class DinosaurInitializer
{
    private static string filePath => Path.GetFullPath("wwwroot/dinosaurs.csv");

    public static void Initialize(IServiceProvider services) 
    {
        using (var scope = services.CreateScope())
        {
            using (var reader = new StreamReader(filePath))
            {
                using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var contextFactory = services.GetRequiredService<IDbContextFactory<DataContext>>();
                    using (var context = contextFactory.CreateDbContext())
                    {
                        csv.Read();
                        csv.ReadHeader();

                        while (csv.Read())
                        {
                            // Add to Dino table
                            var dino = new Dinosaur
                            {
                                Name = csv.GetField("Dinosaur Name"),
                                Type = csv.GetField("DinosaurType")
                            };
                            context?.Add(dino);

                            // Add to Class table (check for existing entries to avoid duplication)
                            var classNumber = csv.GetField<int>("ClassNumber");
                            var teacher = csv.GetField<string>("Teacher");
                            
                            var existingDinoClass = context?.DinoClass
                                .FirstOrDefault(dc => dc.Id == classNumber && dc.Teacher == teacher);
                            
                            var dinoClass = new DinoClass
                            {
                                Id = classNumber,
                                Teacher = teacher,
                            };
                            if (existingDinoClass == null) context?.Add(dinoClass);
                            
                            // Add scores to Scores table
                            List<string> months = ["September", "October", "November"];
                            
                            foreach (var month in months)
                            {
                                var score = csv.GetField<int?>(month); 
                                var result = new Scores
                                {
                                    Score = score,
                                    Date = month,
                                    DinoClass = dinoClass,
                                    Dinosaur = dino
                                };
                                
                                Console.WriteLine($"month:{result.Date} - Score:{result.Score} - :Student:{dino.Name} - Teacher:{dinoClass.Teacher}");
                                
                                //context?.Add(result);
                            }
                        }
                        context.SaveChanges();
                    }
                }
            }
        }
    }
} 
