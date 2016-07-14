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
    public partial class ButtonTextInput : Form
    {

        public Button settings = null;
        public int actual = -1;

        public ButtonTextInput()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String t = textBox1.Text;
            if(actual == 0)
            {
                Settings.b1 = t;
            } else if(actual == 1)
            {
                Settings.b2 = t;
            }
            else if (actual == 2)
            {
                Settings.b3 = t;
            }
            else if (actual == 3)
            {
                Settings.b4 = t;
            }
            else if (actual == 4)
            {
                Settings.b5 = t;
            }
            else if (actual == 5)
            {
                Settings.b6 = t;
            }
            else if (actual == 6)
            {
                Settings.b7 = t;
            }
            else if (actual == 7)
            {
                Settings.b8 = t;
            }
            else if (actual == 8)
            {
                Settings.b9 = t;
            }
            else if (actual == 9)
            {
                Settings.b10 = t;
            }
            else if (actual == 10)
            {
                Settings.b11 = t;
            }
            else if (actual == 11)
            {
                Settings.b12 = t;
            }
            else if (actual == 12)
            {
                Settings.b13 = t;
            }
            else if (actual == 13)
            {
                Settings.b14 = t;
            }
            else if (actual == 14)
            {
                Settings.b15 = t;
            }
            else if (actual == 15)
            {
                Settings.b16 = t;
            }
            else if (actual == 16)
            {
                Settings.b17 = t;
            }
            else if (actual == 17)
            {
                Settings.b18 = t;
            }
            else if (actual == 18)
            {
                Settings.b19 = t;
            }
            else if (actual == 19)
            {
                Settings.b20 = t;
            }
            settings.Text = t;
            Settings.SaveConfig();
            Settings.updateButtons();
            this.Dispose();
        }

        private void ButtonTextInput_Load(object sender, EventArgs e)
        {
            int color = Settings.global_background;
            String back = Settings.colors[color];
            this.BackColor = ColorTranslator.FromHtml(back);
            int color2 = Settings.global_foreground;
            String fore = Settings.colors[color2];
            this.ForeColor = ColorTranslator.FromHtml(fore);
            Control[] arr = new Control[this.Controls.Count];
            this.Controls.CopyTo(arr, 0);
            Form1.paintControls(arr, fore, back);
        }
    }
}
