using BadDinosaurCodeTest.API.Processors;
using BadDinosaurCodeTest.API.Repository;
using BadDinosaurCodeTest.Data.Entity;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<List<Dinosaur>> GetDinosaurs([FromQuery] string? dinoName = null)
        {
                if (dinoName != null)
                {
                    var nameResult = DinoRepository.Find(_services, dinoName);
                    if (nameResult.Count == 0) return NotFound("No dino was found with the name " + dinoName);
                    var result = DinoProcessor.Process(nameResult);
                    
                    return Ok(result);
                }
                
                return BadRequest("Dino name needed to fetch data");
        }
    }
}
