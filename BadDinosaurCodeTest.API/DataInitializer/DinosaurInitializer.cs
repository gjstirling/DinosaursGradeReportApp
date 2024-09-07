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
                        var dino = new Dinosaur
                        {
                            Id = 1,
                            Name = "gary"
                        };

                        context?.Add(dino);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
} 
