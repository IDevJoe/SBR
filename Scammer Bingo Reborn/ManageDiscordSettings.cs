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
        public ManageDiscordSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new DiscordTokenIn().ShowDialog();
        }
    }
}
