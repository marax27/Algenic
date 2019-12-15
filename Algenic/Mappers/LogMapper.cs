using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Data.Models;
using Algenic.Compilation.Outputs;

namespace Algenic.Mappers
{
    public class LogMapper
    {
        private readonly JDoodleError _error;
        public LogMapper(JDoodleError error)
        {
            _error = error;
        }
        public Log Map()
            => new Log
            {
            ErrorMessage = _error.Error,
            StatusCode = _error.StatusCode
            };
    }
}
