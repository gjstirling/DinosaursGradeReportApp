using BadDinosaurCodeTest.API.Processors;
using BadDinosaurCodeTest.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BadDinosaurCodeTest.API.controllers

{
    [ApiController]
    [Route("average")]
    public class DinoAverageScoreController : ControllerBase
    {
        private readonly IServiceProvider _services;

        public DinoAverageScoreController(IServiceProvider services)
        {
            _services = services;
        }

        [HttpGet]
        public ActionResult<List<ClassAverageGradeDto>> Welcome()
        {
            var dinosaurs = DinoClassRepository.CollectScores(_services, true);
            var result = AverageScoreProcessor.Process(dinosaurs);
            
            return Ok(result);
        }
    }
}