using BadDinosaurCodeTest.API.DataInitializer;
using BadDinosaurCodeTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Drawing.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<DataContext>(options => 
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

CreateDbIfNotExists(app.Services);
//DinosaurInitializer.Initialize(app.S);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


void CreateDbIfNotExists(IServiceProvider services) 
{
    var contextFactory = services.GetRequiredService<IDbContextFactory<DataContext>>();
    using var context = contextFactory.CreateDbContext();
    context.Database.Migrate();
}