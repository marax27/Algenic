using Algenic.Commons.DesignByContract;

namespace Algenic.Commands.ContestEnd
{
    public class ContestEndCommand
    {
        public int ContestId { get; }

        private ContestEndCommand(int contestId)
        {
            ContestId = contestId;
        }

        public static ContestEndCommand Create(int contestId)
        {
            Fail.If(contestId <= 0);

            return new ContestEndCommand(contestId);
        }
    }
}
