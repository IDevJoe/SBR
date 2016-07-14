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
    public partial class MessageSettings : Form
    {
        public MessageSettings()
        {
            InitializeComponent();
            textBox1.Text = Settings.settings.temessage;
            textBox2.Text = Settings.settings.twmessage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.settings.temessage = textBox1.Text;
            Settings.settings.twmessage = textBox2.Text;
            Settings.SaveConfig();
            this.Dispose();
        }

        private void MessageSettings_Load(object sender, EventArgs e)
        {
            int color = Settings.settings.global_background;
            String back = Settings.colors[color];
            this.BackColor = ColorTranslator.FromHtml(back);
            int color2 = Settings.settings.global_foreground;
            String fore = Settings.colors[color2];
            this.ForeColor = ColorTranslator.FromHtml(fore);
            Control[] arr = new Control[this.Controls.Count];
            this.Controls.CopyTo(arr, 0);
            Form1.paintControls(arr, fore, back);
        }
    }
}
