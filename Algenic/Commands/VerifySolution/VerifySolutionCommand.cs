
using Algenic.Commons.DesignByContract;

namespace Algenic.Commands.VerifySolution
{
    public class VerifySolutionCommand
    {
        public int SolutionId { get; }

        private VerifySolutionCommand(int solutionId)
        {
            SolutionId = solutionId;
        }

        public static VerifySolutionCommand Create(int solutionId)
        {
            return new VerifySolutionCommand(solutionId);
        }
    }
}
