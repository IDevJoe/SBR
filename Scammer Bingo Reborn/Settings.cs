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
        public static string b1 = "Run";
        public static string b2 = "netstat";
        public static string b3 = "Stopped Services";
        public static string b4 = "I can't understand you sir";
        public static string b5 = "eventvwr";
        public static string b6 = "Secure Server";
        public static string b7 = "msconfig";
        public static string b8 = "The scammer knows..";
        public static string b9 = "cmd";
        public static string b10 = "Do one thing";
        public static string b11 = "Mirosoft certified";
        public static string b12 = "Corrupted Drivers";
        public static string b13 = "tree";
        public static string b14 = "Network Security";
        public static string b15 = "syskey";
        public static string b16 = "Trying to stick to the script";
        public static string b17 = "Fuck off";
        public static string b18 = "hh h";
        public static string b19 = "support.me";
        public static string b20 = "One time charge";

        public static string[] colors = new string[] { "#006600", "#009900", "#00e600", "#003399", "#0033cc", "#0099ff", "#ff3399", "#ff3300", "#ff5c33", "#ffffff", "#737373", "#000000" };

        public Settings()
        {
            InitializeComponent();
        }

        public static void updateButtons()
        {
            Button[] btns = Form1.defaultForm.tochangenamesof.ToArray();

            Form1.defaultForm.PrepareButtons(StringsToStringArray());
        }

        public static string[] LoadStringArray()
        {
            ReadConfig();
            return StringsToStringArray();
        }

        private static string[] StringsToStringArray()
        {
            string[] stringsToPass = new string[20];


            stringsToPass[0] = b1;
            stringsToPass[1] = b2;
            stringsToPass[2] = b3;
            stringsToPass[3] = b4;
            stringsToPass[4] = b5;
            stringsToPass[5] = b6;
            stringsToPass[6] = b7;
            stringsToPass[7] = b8;
            stringsToPass[8] = b9;
            stringsToPass[9] = b10;
            stringsToPass[10] = b11;
            stringsToPass[11] = b12;
            stringsToPass[12] = b13;
            stringsToPass[13] = b14;
            stringsToPass[14] = b15;
            stringsToPass[15] = b16;
            stringsToPass[16] = b17;
            stringsToPass[17] = b18;
            stringsToPass[18] = b19;
            stringsToPass[19] = b20;

            return stringsToPass;
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
                bool b = false;
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
                    } else if(lines[i].StartsWith("b"))
                    {
                        b = true;
                    }
                }
                if(ar && me && twm && tem && gb && gf && b)
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
            String[] config = new String[] { "ar=" + autoreset, "me=" + messages, "twm=" + twmessage, "tem=" + temessage , "gb="+global_background, "gf="+global_foreground, "b1="+b1, "b2=" + b2, "b3=" + b3, "b4=" + b4, "b5=" + b5, "b6=" + b6, "b7=" + b7, "b8=" + b8, "b9=" + b8, "b10=" + b10, "b11=" + b11, "b12=" + b12, "b13=" + b13, "b14=" + b14, "b15=" + b15, "b16=" + b16, "b17=" + b17, "b18=" + b18, "b19=" + b19, "b20=" + b20 };
            File.WriteAllLines(path, config);
        }

        public static void ReadConfig()
        {
            string path = "config.ini";
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
                    } else if(lines[i].StartsWith("b1="))
                    {
                        string[] str = lines[i].Split('=');
                        b1 = str[1];
                    }
                    else if (lines[i].StartsWith("b2="))
                    {
                        string[] str = lines[i].Split('=');
                        b2 = str[1];
                    }
                    else if (lines[i].StartsWith("b3="))
                    {
                        string[] str = lines[i].Split('=');
                        b3 = str[1];
                    }
                    else if (lines[i].StartsWith("b4="))
                    {
                        string[] str = lines[i].Split('=');
                        b4 = str[1];
                    }
                    else if (lines[i].StartsWith("b5="))
                    {
                        string[] str = lines[i].Split('=');
                        b5 = str[1];
                    }
                    else if (lines[i].StartsWith("b6="))
                    {
                        string[] str = lines[i].Split('=');
                        b6 = str[1];
                    }
                    else if (lines[i].StartsWith("b7="))
                    {
                        string[] str = lines[i].Split('=');
                        b7 = str[1];
                    }
                    else if (lines[i].StartsWith("b8="))
                    {
                        string[] str = lines[i].Split('=');
                        b8 = str[1];
                    }
                    else if (lines[i].StartsWith("b9="))
                    {
                        string[] str = lines[i].Split('=');
                        b9 = str[1];
                    }
                    else if (lines[i].StartsWith("b10="))
                    {
                        string[] str = lines[i].Split('=');
                        b10 = str[1];
                    }
                    else if (lines[i].StartsWith("b11="))
                    {
                        string[] str = lines[i].Split('=');
                        b11 = str[1];
                    }
                    else if (lines[i].StartsWith("b12="))
                    {
                        string[] str = lines[i].Split('=');
                        b12 = str[1];
                    }
                    else if (lines[i].StartsWith("b13="))
                    {
                        string[] str = lines[i].Split('=');
                        b13 = str[1];
                    }
                    else if (lines[i].StartsWith("b14="))
                    {
                        string[] str = lines[i].Split('=');
                        b14 = str[1];
                    }
                    else if (lines[i].StartsWith("b15="))
                    {
                        string[] str = lines[i].Split('=');
                        b15 = str[1];
                    }
                    else if (lines[i].StartsWith("b16="))
                    {
                        string[] str = lines[i].Split('=');
                        b16 = str[1];
                    }
                    else if (lines[i].StartsWith("b17="))
                    {
                        string[] str = lines[i].Split('=');
                        b17 = str[1];
                    }
                    else if (lines[i].StartsWith("b18="))
                    {
                        string[] str = lines[i].Split('=');
                        b18 = str[1];
                    }
                    else if (lines[i].StartsWith("b19="))
                    {
                        string[] str = lines[i].Split('=');
                        b19 = str[1];
                    }
                    else if (lines[i].StartsWith("b20="))
                    {
                        string[] str = lines[i].Split('=');
                        b20 = str[1];
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

            button5.Text = b1;
            button6.Text = b2;
            button7.Text = b3;
            button8.Text = b4;
            button9.Text = b5;
            button10.Text = b6;
            button11.Text = b7;
            button12.Text = b8;
            button13.Text = b9;
            button14.Text = b10;
            button15.Text = b11;
            button16.Text = b12;
            button17.Text = b13;
            button18.Text = b14;
            button19.Text = b15;
            button20.Text = b16;
            button21.Text = b17;
            button22.Text = b18;
            button23.Text = b19;
            button24.Text = b20;

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

        private void btnclick(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            if(s.Name == "button5")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 0;
                inp.settings = s;
                inp.ShowDialog();
            } else if(s.Name == "button6")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 1;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button7")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 2;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button8")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 3;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button9")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 4;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button10")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 5;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button11")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 6;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button12")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 7;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button13")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 8;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button14")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 9;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button15")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 10;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button16")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 11;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button17")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 12;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button18")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 13;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button19")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 14;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button20")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 15;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button21")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 16;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button22")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 17;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button23")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 18;
                inp.settings = s;
                inp.ShowDialog();
            }
            else if (s.Name == "button24")
            {
                ButtonTextInput inp = new ButtonTextInput();
                inp.actual = 19;
                inp.settings = s;
                inp.ShowDialog();
            }
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
