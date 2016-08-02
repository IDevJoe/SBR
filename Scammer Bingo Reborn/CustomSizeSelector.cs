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
    public partial class CustomSizeSelector : Form
    {
        int pIndex;
        ComboBox cb;
        bool result = false;

        public CustomSizeSelector(int prevIndex, ComboBox _cb)
        {
            pIndex = prevIndex;
            cb = _cb;
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            cb.SelectedIndex = pIndex;
            this.Dispose();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            int X = (int)numUD_X.Value, Y = (int)numUD_Y.Value;
            if (Settings.settings.strings.Length >= X*Y)
            {
                Settings.settings.sizeX = X;
                Settings.settings.sizeY = Y;
                result = true;
                this.Dispose();
            }
            else
            {
                MessageBox.Show("You need to have at least " + X * Y + " strings to use " + X + " columns and " + Y +"rows, add " + (X * Y - Settings.settings.strings.Length) + " more!");
                if (numUD_X.Value>1) numUD_X.Value--;
                if (numUD_Y.Value > 1) numUD_Y.Value--;
            }
        }

        private void CustomSizeSelector_Load(object sender, EventArgs e)
        {
            this.FormClosing += delegate { if(!result) cb.SelectedIndex = pIndex; };
        }
    }
}
