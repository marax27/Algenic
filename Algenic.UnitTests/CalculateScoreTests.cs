using System;
using Algenic.Commons.DesignByContract;
using Algenic.Data.Models;
using Algenic.Queries.CalculateScore;
using Xunit;
using FluentAssertions;

namespace Algenic.UnitTests
{
    public class CalculateScoreTests
    {
        [Theory]
        [InlineData(0.0, 0)]
        [InlineData(0.49, 0)]
        [InlineData(0.5, 1)]
        [InlineData(0.51, 1)]
        [InlineData(0.9, 2)]
        [InlineData(0.91, 2)]
        [InlineData(1.0, 2)]
        public void CalculateScoreQuery_TwoThresholdScorePolicy_ResultsMeetExpectation(decimal percentage, int expectedScore)
        {
            var givenScorePolicy = new ScorePolicy
            {
                ScoreRules = new[]
                {
                    new ScoreRule {Threshold = 0.5m, Score = 1},
                    new ScoreRule {Threshold = 0.9m, Score = 2}
                }
            };

            CalculateScoreQueryHandler.GetScore(percentage, givenScorePolicy)
                .Should().Be(expectedScore);
        }

        [Theory]
        [InlineData(0.0, 0)]
        [InlineData(0.49, 0)]
        [InlineData(0.5, 5)]
        [InlineData(0.51, 5)]
        [InlineData(1, 5)]
        public void CalculateScoreQuery_SingleThresholdScorePolicy_ResultsMeetExpectation(decimal percentage,
            int expectedScore)
        {
            var givenScorePolicy = new ScorePolicy
            {
                ScoreRules = new[]
                {
                    new ScoreRule {Threshold = 0.5m, Score = 5}
                }
            };

            CalculateScoreQueryHandler.GetScore(percentage, givenScorePolicy)
                .Should().Be(expectedScore);
        }

        [Fact]
        public void CalculateScoreQuery_ScorePolicyWithoutThresholds_Throw()
        {
            var givenScorePolicy = new ScorePolicy
            {
                ScoreRules = System.Array.Empty<ScoreRule>()
            };
            var givenPercentage = 0.5m;

            Action act = () => CalculateScoreQueryHandler.GetScore(givenPercentage, givenScorePolicy);

            act.Should().Throw<DesignByContractException>();
        }
    }
}
