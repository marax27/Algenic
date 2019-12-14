using System;
using Algenic.Commons.DesignByContract;
using Xunit;
using FluentAssertions;

namespace Algenic.UnitTests.DesignByContract
{
    public class FailTests
    {
        [Fact]
        public void If_GivenTrue_ThrowDesignByContractException()
        {
            Action act = () => Fail.If(true);

            act.Should().Throw<DesignByContractException>();
        }

        [Fact]
        public void If_GivenFalse_NoExceptionThrown()
        {
            Action act = () => Fail.If(false);

            act.Should().NotThrow();
        }

        [Fact]
        public void IfNull_GivenNullString_ThrowDesignByContractException()
        {
            Action act = () => Fail.IfNull<string>(null);

            act.Should().Throw<DesignByContractException>();
        }

        [Fact]
        public void IfNull_GivenSampleString_NoExceptionThrown()
        {
            Action act = () => Fail.IfNull("sample string");

            act.Should().NotThrow();
        }

        [Fact]
        public void IfNullOrEmpty_GivenNullCollection_ThrowDesignByContractException()
        {
            Action act = () => Fail.IfNullOrEmpty<int[]>(null);

            act.Should().Throw<DesignByContractException>();
        }

        [Fact]
        public void IfNullOrEmpty_GivenEmptyCollection_ThrowDesignByContractException()
        {
            Action act = () => Fail.IfNullOrEmpty(Array.Empty<int>());

            act.Should().Throw<DesignByContractException>();
        }

        [Fact]
        public void IfNullOrEmpty_GivenSampleCollection_NoExceptionThrown()
        {
            Action act = () => Fail.IfNullOrEmpty(new int[] { 1, 2, 3, 4 });

            act.Should().NotThrow();
        }
    }
}
