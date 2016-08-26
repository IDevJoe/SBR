using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using System.Windows.Forms;
using System.Threading;

namespace Scammer_Bingo_Reborn
{
    class BotBackend
    {
        // Static
        public static BotBackend last;

        // Object
        public DiscordClient c;
        public string token;
        public bool connected = false;
        delegate void setDisabledCallback(Control l);
        delegate void resetBoardCallback();

        public BotBackend(DiscordClient _c, string token)
        {
            c = _c;
            this.token = token;
            c.MessageReceived += C_MessageReceived;
            last = this;
        }

        private void C_MessageReceived(object sender, MessageEventArgs e)
        {
            if (!e.Message.Text.ToLower().StartsWith(">")) return;
            string command = e.Message.Text.Substring(1);
            if(string.IsNullOrWhiteSpace(command)) { return; }
            if (!e.Message.Channel.IsPrivate && Settings.settings.discord_pmonly)
            {
                e.Message.Delete();
                e.Channel.SendMessage("SBR commands can only be ran from PMs based on the current configuration.");
                return;
            }
            else
            {
                string[] args = genargs(command);
                string act = command.Split(' ')[0];
                if (act.ToLower() == "play")
                {
                    commandPlay(args, e.User, e.Channel);
                } else if(act.ToLower() == "board")
                {
                    commandBoard(args, e.User, e.Channel);
                } else if(act.ToLower() == "reset")
                {
                    commandReset(args, e.User, e.Channel);
                } else if(act.ToLower() == "help")
                {
                    commandHelp(args, e.User, e.Channel);
                }
                else
                {
                    e.Channel.SendMessage("Unknown command.");
                }
            }
        }

        private void commandHelp(string[] args, User u, Channel c)
        {
            c.SendMessage("```>help - this...\n>board - Shows the board\n>play - Makes a play on the board\n>reset - Resets the board```");
        }

        private void commandReset(string[] args, User u, Channel c)
        {
            if (args.Length == 0)
            {
                resetBoard();
                c.SendMessage("```" + genBoard() + "```");
                c.SendMessage("The board was reset.");
            }
            else
            {
                c.SendMessage("Syntax: `>reset`");
            }
        }

        private void commandPlay(string[] args, User u, Channel c)
        {
            if(args.Length == 1)
            {
                int num = 0;
                bool l = Int32.TryParse(args[0], out num);
                if(!l)
                {
                    c.SendMessage("The box number must be a valid number.");
                    return;
                }
                Control g = getControlFromInt(num);
                if(g == null)
                {
                    c.SendMessage("Enter a valid box number.");
                    return;
                }
                if(!g.Enabled)
                {
                    c.SendMessage("```" + genBoard() + "```");
                    c.SendMessage("`" + g.Text + "` was already played. Your play has no effect on the board.");
                    return;
                }
                setDisabled(g);
                c.SendMessage("```" + genBoard() + "```");
                c.SendMessage("Played `" + g.Text + "` successfully. The board has been updated.");
            } else
            {
                c.SendMessage("Syntax: `>play <boxnum>`");
            }
        }

        private void setDisabled(Control l)
        {
            if(Form1.defaultForm.InvokeRequired)
            {
                setDisabledCallback d = new setDisabledCallback(setDisabled);
                Form1.defaultForm.Invoke(d, new object[] { l });
            } else
            {
                Form1.defaultForm.buttonClick(l, true);
            }
        }

        private void resetBoard()
        {
            if(Form1.defaultForm.InvokeRequired)
            {
                resetBoardCallback g = new resetBoardCallback(resetBoard);
                Form1.defaultForm.Invoke(g, new object[] { });
            } else
            {
                Form1.defaultForm.ResetScoreAndButtons();
            }
        }

        private Control getControlFromInt(int i)
        {
            Control.ControlCollection c = Form1.defaultForm.groupBox_BingoBoard.Controls;
            Control[] ctrls = new Control[c.Count];
            c.CopyTo(ctrls, 0);
            for(int ii=0;ii<ctrls.Length;ii++)
            {
                if((ii+1) == i)
                {
                    return ctrls[ii];
                }
            }
            return null;
        }

        private void commandBoard(string[] args, User u, Channel c)
        {
            if(args.Length == 0)
            {
                c.SendMessage("```" + genBoard() + "```");
            } else
            {
                c.SendMessage("Syntax: `>board`");
            }
        }

        private string genBoard()
        {
            int xL = Settings.settings.sizeX;
            int yL = Settings.settings.sizeY;
            int amount = xL * yL;
            Control.ControlCollection c = Form1.defaultForm.groupBox_BingoBoard.Controls;
            Control[] ctrls = new Control[c.Count];
            c.CopyTo(ctrls, 0);
            int maxrow = xL;
            int amountt = 0;
            string all = "Current Board:\n";
            string line = "";
            int number = 1;
            int crow = 0;
            List<string> disablednums = new List<string>();

            for(int i=0;i<ctrls.Length;i++)
            {
                if (!ctrls[i].Enabled)
                {
                    disablednums.Add((i+1) + "");
                }
            }

            for(int i=0;i<amount;i++)
            {
                string toput = "" + number;
                if(disablednums.Contains(number+""))
                {
                    toput = "///";
                }
                if(toput.ToCharArray().Length == 1)
                {
                    toput += "--";
                } else if(toput.ToCharArray().Length == 2)
                {
                    toput += "-";
                }
                if(string.IsNullOrWhiteSpace(line))
                {
                    line = toput;
                } else
                {
                    line += "|" + toput;
                }
                number += yL;
                amountt++;
                if(amountt >= maxrow)
                {
                    string eqgen = "";
                    for(int ii=0;ii<line.Length;ii++)
                    {
                        eqgen += "=";
                    }
                    amountt = 0;
                    if (crow != 0)
                    {
                        all += "\n" + eqgen + "\n" + line;
                    }
                    else
                    {
                        all += "\n" + line;
                    }
                    line = "";
                    crow++;
                    number = crow+1;
                }
            }

            all += "\n\n";
            for(int i=0;i<ctrls.Length;i++)
            {
                if(i == 0)
                {
                    all += (i + 1) + " = " + ctrls[i].Text;
                } else
                {
                    all += " | " + (i + 1) + " = " + ctrls[i].Text;
                }
            }
            all += "\n\nScore: " + Form1.defaultForm.label3.Text;
            return all;
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
            connected = false;
        }

        public void reconnect()
        {
            Console.WriteLine("Attempting connection with token " + token);
            c.Connect(token);
            connected = true;
        }
    }
}
