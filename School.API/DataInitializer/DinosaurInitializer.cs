using CsvHelper;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Globalization;
using School.Data;
using School.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace School.API.DataInitializer;

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
                            // ADD CLASS DATA
                            var classNumber = csv.GetField<int>("ClassNumber");
                            var teacher = csv.GetField<string>("Teacher");
                            var dinoClass = context.DinoClass
                                                .Local
                                                .FirstOrDefault(dc => dc.Id == classNumber && dc.Teacher == teacher) 
                                            ?? context.DinoClass
                                                .FirstOrDefault(dc => dc.Id == classNumber && dc.Teacher == teacher);

                            if (dinoClass == null)
                            {
                                dinoClass = new DinoClass
                                {
                                    Id = classNumber,
                                    Teacher = teacher ?? "Unknown"
                                };
                                context.DinoClass.Add(dinoClass);
                            }
                            
                            // ADD DINO DATA
                            var dino = new Dinosaur
                            {
                                Name = csv.GetField("Dinosaur Name") ?? "Unknown",
                                Type = csv.GetField("DinosaurType") ?? "Unknown", 
                                DinoClass = dinoClass
                            };
                            
                            // ADD SCORES
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
                                context?.Add(result);
                            }
                            context?.Dinosaurs.Add(dino);
                        }
                        context?.SaveChanges();
                    }
                }
            }
        }
    }
} 
