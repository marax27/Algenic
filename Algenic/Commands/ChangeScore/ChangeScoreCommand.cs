using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return new ChangeScoreCommand(solutionId, score);
        }
    }
}
