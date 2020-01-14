using Algenic.Commons.DesignByContract;

namespace Algenic.Commands.ChangeScore
{
    public class ChangeScoreCommand
    {
        public int SolutionId { get; }
        public int Score { get; }

        private ChangeScoreCommand(int solutionId, int score)
        {
            SolutionId = solutionId;
            Score = score;
        }

        public static ChangeScoreCommand Create(int solutionId, int score)
        {
            Fail.If(solutionId <= 0);

            return new ChangeScoreCommand(solutionId, score);
        }
    }
}
