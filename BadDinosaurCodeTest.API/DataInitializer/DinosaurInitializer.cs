using CsvHelper;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Globalization;

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
                // Now, how do I get that CSV in here?
            }
        }
    }
} 
