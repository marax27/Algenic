using Algenic.Data;
using Algenic.UnitTests.Setup;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Algenic.Commands.ChangeScore;

namespace Algenic.UnitTests.Commands
{
    public class ChangeScoreCommandTests : BaseDatabaseTest
    {
        protected override ApplicationDbContext PrepareContext(ApplicationDbContext context)
            => context;
        [Fact]
        public void Test1()
        {

        }
    }
}
