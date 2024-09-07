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
                            
                            // Adding classes
                            var classNumber = csv.GetField<int?>("ClassNumber");
                            var teacher = csv.GetField<string>("Teacher");
                            
                            if (classNumber.HasValue)
                            {
                                var dinoClass = context?.DinoClass
                                                .Local.FirstOrDefault(dc => dc.Id == classNumber && dc.Teacher == teacher)
                                            ?? context?.DinoClass
                                                .FirstOrDefault(dc => dc.Id == classNumber && dc.Teacher == teacher); 
    
                                if (dinoClass == null)
                                {
                                    dinoClass = new DinoClass
                                    {
                                        Id = classNumber.Value, 
                                        Teacher = teacher ?? "Unknown"  
                                    };
                                    context?.Add(dinoClass);
                                }
                            }
                            
                            // Add scores to Scores table
                            List<string> months = ["September", "October", "November", "December", "January", 
                                "February", "March", "April", "May", "June", "July", "August"];
                            
                            foreach (var month in months)
                            {
                                var score = csv.GetField<int?>(month); 
                                var result = new Scores
                                {
                                    Score = score,
                                    Date = month,
                                    Dinosaur = dino
                                };
                                
                                Console.WriteLine($"month:{result.Date} - Score:{result.Score} - :Student:{dino.Name}");
                                
                                context?.Add(result);
                            }
                        }
                        context.SaveChanges();
                    }
                }
            }
        }
    }
} 
