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
            textBox1.Text = Settings.temessage;
            textBox2.Text = Settings.twmessage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.temessage = textBox1.Text;
            Settings.twmessage = textBox2.Text;
            Settings.SaveConfig();
            this.Dispose();
        }
    }
}
