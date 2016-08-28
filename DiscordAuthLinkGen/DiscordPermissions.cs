using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAuthLinkGen
{
    class DiscordPermissions
    {
        private List<string> Keys = new List<string>();
        private List<int> Values = new List<int>();

        public int get(string key)
        {
            int b = 0;
            if(Keys.Contains(key))
            {
                return Values.ToArray()[Keys.IndexOf(key)];
            }
            return b;
        }

        public void put(string key, int val)
        {
            Keys.Add(key);
            Values.Add(val);
        }
    }
}
