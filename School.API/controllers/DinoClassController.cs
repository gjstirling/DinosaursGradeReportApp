using School.API.Services;
using School.Shared.Routes;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace School.API.Controllers
{
    [ApiController]
    public class DinoClassController : ControllerBase
    {
        private readonly IDinoClassService _dinoClassService;

        public DinoClassController(IDinoClassService dinoClassService)
        {
            _dinoClassService = dinoClassService;
        }

        [HttpGet(ApiRoutes.AverageScore)]
        public async Task<ActionResult<List<ClassAverageGradeDto>>> GetAverage(bool excludeNull)
        {
            try
            {
                var result = await _dinoClassService.Average(excludeNull);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(ApiRoutes.ScoreRange)]
        public async Task<ActionResult<List<ClassWithGradeRangeDto>>> GetRange(bool excludeNull)
        {
            try
            {
                var result = await _dinoClassService.Range(excludeNull);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}