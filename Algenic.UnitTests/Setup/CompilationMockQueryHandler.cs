using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Commons.DesignByContract;
using Algenic.Queries.Compilation;

namespace Algenic.UnitTests.Setup
{
    public class CompilationMockQueryHandler : IQueryHandler<CompilationQuery, CompilationQueryResult>
    {
        private IDictionary<CompilationQuery, CompilationQueryResult> _expectedBehaviors
            = new Dictionary<CompilationQuery, CompilationQueryResult>();

        private CompilationQuery _onQuery = null;

        public CompilationMockQueryHandler On(CompilationQuery query)
        {
            _onQuery = query;
            return this;
        }

        public CompilationMockQueryHandler Returns(CompilationQueryResult result)
        {
            Fail.IfNull(_onQuery);
            _expectedBehaviors[_onQuery] = result;
            return this;
        }

        public Task<CompilationQueryResult> HandleAsync(CompilationQuery query)
        {
            var key = GetKey(query);
            if (key == null)
                throw new ArgumentException("Provided query not registered in a mock object.");
            return Task.FromResult(_expectedBehaviors[key]);
        }

        private CompilationQuery GetKey(CompilationQuery query)
        {
            return _expectedBehaviors.Keys
                .SingleOrDefault(k => k.Input.Equals(query.Input)
                                   && k.ProgrammingLanguage.Equals(query.ProgrammingLanguage)
                                   && k.SourceCode.Equals(query.SourceCode));
        }
    }
}
