using Algenic.Commons.DesignByContract;
using Algenic.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return new ContestEndCommand(contestId);
        }
    }
}
