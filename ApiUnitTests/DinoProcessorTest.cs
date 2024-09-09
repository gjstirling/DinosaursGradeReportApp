using FluentAssertions;
using Xunit;
using BadDinosaurCodeTest.Data.Entity; 
using BadDinosaurCodeTest.API.Processors; 

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
                    Type = "T Rex"
        
                },
                new Dinosaur
                {
                    Id = 2,
                    Name = "Barry",    
                    Type = "Velociraptor"
                }
            };
            
            var result = DinoProcessor.Process(dinosaurList);
                        
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}