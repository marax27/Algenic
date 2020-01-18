using System;
using Algenic.Data;
using Microsoft.EntityFrameworkCore;

namespace Algenic.UnitTests.Setup
{
    public abstract class BaseDatabaseTest : IDisposable
    {
        protected ApplicationDbContext Context;

        public BaseDatabaseTest() => InitContext();

        public void Dispose() => Context.Dispose();

        protected abstract ApplicationDbContext PrepareContext(ApplicationDbContext context);

        private void InitContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test-" + Guid.NewGuid());

            var context = new ApplicationDbContext(builder.Options);
            Context = PrepareContext(context);
        }
    }
}
