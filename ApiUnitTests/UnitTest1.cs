using FluentAssertions;
using Xunit;

namespace ApiUnitTests
{
    public class UnitTests
    {
        [Fact]
        public void Test1()
        {
            var test = 1;

            test.Should().Be(1);
        }
    }
}