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
    public partial class ManageDiscordSettings : Form
    {

        private static BotBackend backend = null;

        public ManageDiscordSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new DiscordTokenIn(this).ShowDialog();
            if(string.IsNullOrWhiteSpace(Settings.settings.discord_token))
            {
                checkBox3.Enabled = false;
            } else
            {
                checkBox3.Enabled = true;
            }
            if (checkBox3.Checked && backend != null) { backend.disconnect(); checkBox3.Checked = false; } 
        }

        private void ManageDiscordSettings_Load(object sender, EventArgs e)
        {
            string token = Settings.settings.discord_token;
            if(string.IsNullOrWhiteSpace(token))
            {
                label2.Text = "N/A";
                checkBox3.Enabled = false;
            } else
            {
                label2.Text = token;
                checkBox3.Enabled = true;
            }
            checkBox2.Checked = Settings.settings.discord_pmonly;
            if(backend != null)
            {
                backend.mds = this;
                checkBox3.Checked = backend.connected;
                if (backend.connected)
                {
                    label5.Text = "Online";
                    label7.Text = backend.servers + "";
                }
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.settings.discord_pmonly = ((CheckBox)sender).Checked;
            Settings.SaveConfig();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                if (backend == null)
                {
                    backend = new BotBackend(new Discord.DiscordClient(), label2.Text, this);
                    backend.reconnect();
                } else
                {
                    backend.reconnect();
                }
            } else
            {
                backend.disconnect();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.Checked = false;
                MessageBox.Show("This feature is not available in the current release.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
