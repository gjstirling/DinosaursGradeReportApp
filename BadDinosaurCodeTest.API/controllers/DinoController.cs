using BadDinosaurCodeTest.Data;
using BadDinosaurCodeTest.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadDinosaurCodeTest.API.controllers

{
    [ApiController]
    [Route("find")]
    public class DinoController : ControllerBase
    {
        private readonly IServiceProvider _services;

        public DinoController(IServiceProvider services)
        {
            _services = services;
        }

        [HttpGet]
        public ActionResult<string> GetDinosaurs([FromQuery] string? dinoName = null)
        {
            var contextFactory = _services.GetRequiredService<IDbContextFactory<DataContext>>();
            using (var context = contextFactory.CreateDbContext())
            {
                if (dinoName != null)
                {
                    var nameResult = context.Dinosaurs
                        .Where(d => d.Name == dinoName)
                        .Include(d => d.Scores)
                        .ToList();
                    
                    if (nameResult.Count == 0) return NotFound("No dino was found with the name " + dinoName);
                
                    return Ok(nameResult);
                }
                
                var allDinos = context.Dinosaurs
                    .Include(d => d.Scores)
                    .ToList();
                
                return Ok(allDinos);
            }
        }
    }
}
