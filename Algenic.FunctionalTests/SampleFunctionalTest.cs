using Algenic.FunctionalTests.Setup;
using FluentAssertions;
using Xunit;

namespace Algenic.FunctionalTests
{
    public class SampleFunctionalTest : BaseFunctionalTest
    {
        public SampleFunctionalTest(DriverFixture driverFixture)
            : base(driverFixture) { }

        [Fact]
        public void IndexTitleTest()
        {
            var expectedTitle = "Home page - Algenic";

            _driver.Navigate().GoToUrl(_indexUrl);

            _driver.Title.Should().Be(expectedTitle);
        }
    }
}
