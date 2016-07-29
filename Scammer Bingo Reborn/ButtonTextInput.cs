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
        
        public ListBox caller;
        public int actual = -1;

        public ButtonTextInput(int _actual, ListBox _caller)
        {
            actual = _actual;
            caller = _caller;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            string t = textBox1.Text;
            Settings.settings.strings[actual] = t; // Dema, please fix this. A index out of range exception occours at this line. Please fix it. -cake2
            Settings.SaveConfig();
            Settings.UpdateList(caller);
            Form1.defaultForm.PrepareButtons(Settings.settings.strings);
            this.Dispose();
            }
            catch (IndexOutOfRangeException) // I have made a temporary fix to this issue. You may remove this once the problem has been fixed and this does not pop up. -cake2
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                MessageBox.Show("You cannot add a button currently, Try again later in the next update. Sorry for any inconvenience.");
                Close();
            }
        }

        private void ButtonTextInput_Load(object sender, EventArgs e)
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
