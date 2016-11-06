using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DiscordAuthLinkGen
{
    public partial class Form1 : Form
    {

        private DiscordPermissions dp = new DiscordPermissions();
        private int sum = 0;

        public Form1()
        {
            InitializeComponent();
            dp.put("checkBox4", 0x00000008);
            dp.put("checkBox1", 0x00000001);
            dp.put("checkBox2", 0x00000002);
            dp.put("checkBox3", 0x00000004);
            dp.put("checkBox5", 0x00000010);
            dp.put("checkBox6", 0x00000020);
            dp.put("checkBox7", 0x00000400);
            dp.put("checkBox8", 0x00000800);
            dp.put("checkBox9", 0x00001000);
            dp.put("checkBox10", 0x00002000);
            dp.put("checkBox11", 0x00004000);
            dp.put("checkBox12", 0x0008000);
            dp.put("checkBox13", 0x00010000);
            dp.put("checkBox14", 0x00020000);
            dp.put("checkBox15", 0x00100000);
            dp.put("checkBox16", 0x00200000);
            dp.put("checkBox17", 0x00400000);
            dp.put("checkBox18", 0x00800000);
            dp.put("checkBox19", 0x01000000);
            dp.put("checkBox20", 0x02000000);
            dp.put("checkBox21", 0x04000000);
            dp.put("checkBox22", 0x08000000);
            dp.put("checkBox23", 0x10000000);
        }

        // https://discordapp.com/oauth2/authorize?client_id=[CLID]&scope=bot&permissions=0

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox s = (CheckBox)sender;
            string name = s.Name;
            if(s.Checked)
            {
                int add = dp.get(name);
                Console.WriteLine("added " + add + " to the sum");
                sum += add;
            } else
            {
                int add = dp.get(name);
                Console.WriteLine("removed " + add + " from the sum");
                sum -= add;
            }
            updateURL();
        }

        private void updateURL()
        {
            textBox2.Text = "https://discordapp.com/oauth2/authorize?client_id="+textBox1.Text+"&scope=bot&permissions="+sum;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            updateURL();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
            MessageBox.Show(null, "Copied!", "Discord Bot Authorization Link Generator", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
