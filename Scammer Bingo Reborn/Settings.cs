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
                }
                if(ar && me && twm && tem)
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
            String[] config = new String[] { "ar=" + autoreset, "me=" + messages, "twm=" + twmessage, "tem=" + temessage };
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
        }
    }
}
