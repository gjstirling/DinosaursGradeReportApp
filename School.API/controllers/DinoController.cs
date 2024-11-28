using Microsoft.AspNetCore.Mvc;
using School.API.Services;
using School.Shared.Routes;
using Shared.Dtos;

namespace School.API.Controllers
{
    [ApiController]
    public class DinoController : ControllerBase
    {
        private readonly IDinoService _dinoService;

        public DinoController(IDinoService dinoService)
        {
            _dinoService = dinoService;
        }

        [HttpGet(ApiRoutes.DinoByName)]
        public async Task<ActionResult<List<DinoDto>>> GetDinosaurs([FromQuery] string dinoName)
        {
            try
            {
                var result = await _dinoService.Get(dinoName);

                if (result == null || result.Count == 0)
                {
                    return NotFound("No dino was found with the name " + dinoName);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}