using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.ViewModels
{
    public class StartingPageViewModel
    {
        public IEnumerable<(int id, string name)> Contests { get; set; }
    }
}
