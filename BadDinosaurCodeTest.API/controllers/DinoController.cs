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
        public ActionResult<string> Welcome()
        {
            return Ok("Hello world");
        }
    }
}
