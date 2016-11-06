using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
                checkBox3.Checked = backend.connected;
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
                    backend = new BotBackend(new Discord.DiscordClient(), label2.Text);
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

        private void titlebar_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void titlebar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
