using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Scammer_Bingo_Reborn
{
    public partial class Settings : Form

    {

        public static bool autoreset = false;
        public static bool messages = false;
        public static string twmessage = "You have successfully found a scammer!";
        public static string temessage = "Halfway there! You can probably rat out the scammer now.";
        public static int global_background = 10;
        public static int global_foreground = 9;

        public static string[] colors = new string[] { "#006600", "#009900", "#00e600", "#003399", "#0033cc", "#0099ff", "#ff3399", "#ff3300", "#ff5c33", "#ffffff", "#737373", "#000000" };

        public Settings()
        {
            InitializeComponent();
        }

        private static bool isConfigParsable(String path)
        {
            if(File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                bool ar = false;
                bool me = false;
                bool twm = false;
                bool tem = false;
                bool gb = false;
                bool gf = false;
                for(int i=0;i<lines.Length;i++)
                {
                    if(lines[i].StartsWith("ar=") && (lines[i].ToLower() == "ar=false" || lines[i].ToLower() == "ar=true"))
                    {
                        ar = true;
                    }
                    else if (lines[i].StartsWith("me=") && (lines[i].ToLower() == "me=false" || lines[i].ToLower() == "me=true"))
                    {
                        me = true;
                    }
                    else if (lines[i].StartsWith("twm="))
                    {
                        twm = true;
                    }
                    else if (lines[i].StartsWith("tem="))
                    {
                        tem = true;
                    }
                    else if (lines[i].StartsWith("gb="))
                    {
                        gb = true;
                    }
                    else if (lines[i].StartsWith("gf="))
                    {
                        gf = true;
                    }
                }
                if(ar && me && twm && tem && gb && gf)
                {
                    return true;
                }
            }
            return false;
        }

        public static void SaveConfig()
        {
            String path = "config.ini";
            if(File.Exists(path))
            {
                File.Delete(path);
            }
            String[] config = new String[] { "ar=" + autoreset, "me=" + messages, "twm=" + twmessage, "tem=" + temessage , "gb="+global_background, "gf="+global_foreground};
            File.WriteAllLines(path, config);
        }

        public static void ReadConfig()
        {
            String path = "config.ini";
            if(!isConfigParsable(path))
            {
                SaveConfig();
                return;
            } else
            {
                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("ar=") && (lines[i].ToLower() == "ar=false" || lines[i].ToLower() == "ar=true"))
                    {
                        string[] str = lines[i].Split('=');
                        if(str[1].ToLower() == "true")
                        {
                            Console.WriteLine("Read true..");
                            autoreset = true;
                        }
                    }
                    else if (lines[i].StartsWith("me=") && (lines[i].ToLower() == "me=false" || lines[i].ToLower() == "me=true"))
                    {
                        string[] str = lines[i].Split('=');
                        if (str[1].ToLower() == "true")
                        {
                            messages = true;
                        }
                    }
                    else if (lines[i].StartsWith("twm="))
                    {
                        string[] str = lines[i].Split('=');
                        twmessage = str[1];
                    }
                    else if (lines[i].StartsWith("tem="))
                    {
                        string[] str = lines[i].Split('=');
                        temessage = str[1];
                    }
                    else if (lines[i].StartsWith("gb="))
                    {
                        string[] str = lines[i].Split('=');
                        int.TryParse(str[1], out global_background);
                        if(global_background < 0 || global_background > 11)
                        {
                            global_background = 10;
                        }
                    }
                    else if (lines[i].StartsWith("gf="))
                    {
                        string[] str = lines[i].Split('=');
                        int.TryParse(str[1], out global_foreground);
                        if (global_foreground < 0 || global_foreground > 11)
                        {
                            global_foreground = 10;
                        }
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            if(box.Checked)
            {
                checkBox2.Enabled = true;
                messages = true;
            } else if (!box.Checked)
            {
                checkBox2.Checked = false;
                checkBox2.Enabled = false;
                autoreset = false;
                messages = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            autoreset = box.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReadConfig();
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveConfig();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReadConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new MessageSettings().ShowDialog();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            ReadConfig();
            checkBox2.Checked = autoreset;
            checkBox1.Checked = messages;
            if (!messages)
            {
                checkBox2.Enabled = false;
            }
            comboBox1.SelectedIndex = global_background;
            comboBox2.SelectedIndex = global_foreground;

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox b = (ComboBox)sender;
            global_background = b.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox b = (ComboBox)sender;
            global_foreground = b.SelectedIndex;
        }
    }
}
