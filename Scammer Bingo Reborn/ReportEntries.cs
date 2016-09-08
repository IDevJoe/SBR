using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scammer_Bingo_Reborn
{
    class ReportEntries
    {
        public class Summary
        {
            public string ip { get; set; }
            public string type { get; set; }
            public int id { get; set; }
        }

        public class RootObject
        {
            public bool success { get; set; }
            public Summary summary { get; set; }
            public string error { get; set; }
        }
    }
}
