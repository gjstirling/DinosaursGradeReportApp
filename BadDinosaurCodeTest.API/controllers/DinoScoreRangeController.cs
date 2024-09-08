using BadDinosaurCodeTest.API.Processors;
using BadDinosaurCodeTest.API.Repository;
using Microsoft.AspNetCore.Mvc;
namespace BadDinosaurCodeTest.API.controllers

{
    [ApiController]
    [Route("range")]
    public class DinoScoreRangeController : ControllerBase
    {
        private readonly IServiceProvider _services;

        public DinoScoreRangeController( IServiceProvider services)
        {
            _services = services;
        }

        [HttpGet]
        public ActionResult<List<ClassWithGradeRangeDto>> Welcome()
        {
            var dinosaurs = DinoClassRepository.CollectScores(_services, false);
            var data = HighToLowScoreProcessor.Process(dinosaurs);
            
            return Ok(data);
        }
    }
}

