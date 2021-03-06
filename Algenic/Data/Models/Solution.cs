﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Data.Models
{
    public class Solution
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string SourceCode { get; set; }
        public string Language { get; set; }
        public decimal? PointCount { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual Task Task { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
        public virtual ICollection<CompilationResult> CompilationResults { get; set; } = new HashSet<CompilationResult>();
        public virtual ICollection<Log> Logs { get; set; } = new HashSet<Log>();
    }
}
