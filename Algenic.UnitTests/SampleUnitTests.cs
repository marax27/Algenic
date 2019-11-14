using System;
using FluentAssertions;
using Xunit;

namespace Algenic.UnitTests
{
    public class SampleUnitTests
    {
        [Fact]
        public void ExampleTest()
        {
            Func<int, int> action = x => x * x;
            var givenValue = 42;
            var expectedValue = givenValue * givenValue;

            var actualValue = action(givenValue);

            actualValue.Should().Be(expectedValue);
        }
    }
}
