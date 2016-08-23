using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scammer_Bingo_Reborn
{
    public partial class DiscordTokenIn : Form
    {

        private ManageDiscordSettings s;

        public DiscordTokenIn(ManageDiscordSettings _s)
        {
            InitializeComponent();
            s = _s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                s.label2.Text = textBox1.Text;
                Settings.settings.discord_token = textBox1.Text;
                Settings.SaveConfig();
            }
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            s.label2.Text = "N/A";
            Settings.settings.discord_token = "";
            Settings.SaveConfig();
            this.Dispose();
        }
    }
}
