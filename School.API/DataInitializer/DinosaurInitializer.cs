using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using School.Data;
using School.Data.Entity;
using School.Shared.Enum;

namespace School.API.DataInitializer;
public class DinosaurInitializer
{
    private static string filePath => Path.GetFullPath("wwwroot/dinosaurs.csv");

    private class DinoCsvItem
    {
        public string DinosaurName { get; set; }
        public string DinosaurType { get; set; }
        public int ClassNumber { get; set; }
        public string Teacher { get; set; }
        public int? January { get; set; }
        public int? February { get; set; }
        public int? March { get; set; }
        public int? April { get; set; }
        public int? May { get; set; }
        public int? June { get; set; }
        public int? July { get; set; }
        public int? August { get; set; }
        public int? September { get; set; }
        public int? October { get; set; }
        public int? November { get; set; }
        public int? December { get; set; }
    }

    private static List<Month> months;

    public static async void Initialize(IServiceProvider services)
    {
        try
        {
            using var scope = services.CreateScope();

            var contextFactory = services.GetRequiredService<IDbContextFactory<DataContext>>();

            using var reader = new StreamReader(filePath);
            using CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var csvItems = csv
                    .GetRecords<DinoCsvItem>()
                    .ToList();

            using var context = contextFactory.CreateDbContext();

            var classes = await context.DinoClass.ToListAsync();
            var dinosaurs = await context.Dinosaurs
                .Include(x => x.Scores)
                .ToListAsync();

            foreach (var item in csvItems)
            {
                // Update CLASS table based on Class Number - Using the class number property and not its id (primary key) 
                var existingClass = classes.FirstOrDefault(x => x.Id == item.ClassNumber);

                if (existingClass is null)
                {
                    existingClass = new DinoClass
                    {
                        Number = item.ClassNumber,
                        Teacher = item.Teacher
                    };

                    context.DinoClass.Add(existingClass);
                    classes.Add(existingClass);
                }

                // Update DINO table based on Dinosaur Name
                var existingDino = dinosaurs.FirstOrDefault(x => x.Name == item.DinosaurName);
                if (existingDino is null)
                {
                    existingDino = new Dinosaur
                    {
                        Name = item.DinosaurName
                    };

                    existingDino.Type = item.DinosaurType;
                    existingDino.DinoClass = existingClass;
                    context.Dinosaurs.Add(existingDino);

                }
                // Assign a Class relationship to dinosaur
                existingDino.DinoClass = existingClass;

                // Update SCORES table 
                foreach (Month month in Enum.GetValues(typeof(Month)))
                {
                    var scoreVal = month switch
                    {
                        Month.January => item.January,
                        Month.February => item.February,
                        Month.March => item.March,
                        Month.April => item.April,
                        Month.May => item.May,
                        Month.June => item.June,
                        Month.July => item.July,
                        Month.August => item.August,
                        Month.September => item.September,
                        Month.October => item.October,
                        Month.November => item.November,
                        Month.December => item.December,
                        _ => throw new NullReferenceException("Could not match month to score.")
                    };

                    var score = existingDino.Scores.FirstOrDefault(x => x.Month == month.ToString());

                    if (score is null)
                    {
                        score = new Scores
                        {
                            Month = month.ToString(),
                        };
                        existingDino.Scores.Add(score);
                    }

                    score.Score = scoreVal;
                }
            }

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
