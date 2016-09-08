using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scammer_Bingo_Reborn
{
    class ADDNumberEntries
    {
        public class Summary
        {
            public string country { get; set; }
            public string number { get; set; }
            public string ip { get; set; }
            public string name { get; set; }
        }

        public class RootObject
        {
            public bool success { get; set; }
            public Summary summary { get; set; }
            public string error { get; set; }
        }
    }
}
