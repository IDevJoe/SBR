using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Scammer_Bingo_Reborn
{
    class GETNumberEntries
    {
        public class Summary
        {
            public int limit { get; set; }
        }

        public class Datum
        {
            public string id { get; set; }
            public string country { get; set; }
            public string number { get; set; }
            public string submitted_name { get; set; }
            public string submitted_date { get; set; }
        }

        public class RootObject
        {
            public bool success { get; set; }
            public Summary summary { get; set; }
            public List<Datum> data { get; set; }
            public string error { get; set; }
        }
    }
}
