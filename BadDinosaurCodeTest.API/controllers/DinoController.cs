using Microsoft.AspNetCore.Mvc;

namespace BadDinosaurCodeTest.API.controllers;

[ApiController]
[Route("home")]
public class DinoController : ControllerBase
{
    private readonly ILogger<DinoController> _logger;

    public DinoController(ILogger<DinoController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "")]
    public string welcome()
    {
        return "Hello World";
    }
            
    //     Implement an endpoint that provides a list of each class, along with the highest and lowest grade.
    //     Implement an endpoint that provides a list of each class, along with the average score for each dinosaur.
    //     Some dinosaurs were unable to provide scores for every test, provide the option to exclude these dinosaurs from the above lists.
    //     Teachers have requested the ability to find a dinosaur by name (including their scores) - implement an endpoint that does this.
}

