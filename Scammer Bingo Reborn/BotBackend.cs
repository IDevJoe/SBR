using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Scammer_Bingo_Reborn
{
    class BotBackend
    {
        // Static
        public static BotBackend last;

        // Object
        public DiscordClient c;
        public string token;

        public BotBackend(DiscordClient _c, string token)
        {
            c = _c;
            c.MessageReceived += C_MessageReceived;
        }

        private void C_MessageReceived(object sender, MessageEventArgs e)
        {
            if (!e.Message.Text.ToLower().StartsWith(">")) return;
            string command = e.Message.Text.Substring(1);
            if(string.IsNullOrWhiteSpace(command)) { return; }
            if(!e.Message.Channel.IsPrivate && Settings.settings.discord_pmonly)
            {
                e.Message.Delete();
                e.Channel.SendMessage("SBR commands can only be ran from PMs based on the current configuration.");
                return;
            }
            string[] args = genargs(command);
            string act = command.Split(' ')[0];
        }

        private string[] genargs(string s)
        {
            string[] a = new string[] { };
            if(s.Split(' ').Length > 1)
            {
                string[] a1 = s.Split(' ');
                string[] neww = new string[a1.Length-1];
                for(int i=1;i<a1.Length;i++)
                {
                    neww[i - 1] = a1[i];
                }
                a = neww;
            }
            return a;
        }

        public void disconnect()
        {
            c.Disconnect();
        }

        public void reconnect()
        {
            c.Connect(token);
        }
    }
}
