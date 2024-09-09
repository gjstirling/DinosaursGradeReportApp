using FluentAssertions;
using Xunit;
using BadDinosaurCodeTest.Data.Entity; 
using BadDinosaurCodeTest.API.Processors; 

using FluentAssertions;
using Xunit;
using BadDinosaurCodeTest.Data.Entity;
using BadDinosaurCodeTest.API.Processors;
using System.Collections.Generic;

using FluentAssertions;
using Xunit;
using BadDinosaurCodeTest.Data.Entity;
using BadDinosaurCodeTest.API.Processors;
using System.Collections.Generic;

namespace ApiUnitTests
{
    public class DinoProcessorTests
    {
        [Fact]
        public void Process_ValidDinosaurList_ReturnsExpectedDinoDtos()
        {
            var dinosaurList = new List<Dinosaur>
            {
                new Dinosaur
                {
                    Id = 1,
                    Name = "Gary",
                    Type = "Tyrannosaurus Rex",
                }
            };
            dinosaurList[0].Scores.Add(new Scores { Date = "January", Score = 85 });
            
            var result = DinoProcessor.Process(dinosaurList);
            
            result.Should().NotBeNull();
            result[0].Id.Should().Be(1);
            result[0].Name.Should().Be("Gary");
            result[0].Scores.Should().HaveCount(1);
            result[0].Scores[0].Month.Should().Be("January");
            result[0].Scores[0].Score.Should().Be(85);
        }
    }
}


