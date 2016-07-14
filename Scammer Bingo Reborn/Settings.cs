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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Scammer_Bingo_Reborn
{
    public partial class Settings : Form

    {
        const string savepath = "config.ini";

                                                                  //Default values
        public static SavedSettings settings = new SavedSettings(false,false, "You have successfully found a scammer!", "Halfway there! You can probably rat out the scammer now.",10,9, new string[] {"Run", "netstat", "Stopped Services", "I can't understand you sir", "eventvwr", "Secure Server", "msconfig", "The scammer knows...", "cmd", "Do One Thing", "Microsoft Certified", "Corrupted Drivers", "tree", "Network Security", "syskey", "Trying to stick to the script", "Fuck off", "hh h", "support.me", "$$$"});

        public static string[] colors = new string[] { "#006600", "#009900", "#00e600", "#003399", "#0033cc", "#0099ff", "#ff3399", "#ff3300", "#ff5c33", "#ffffff", "#737373", "#000000" };

        public Settings()
        {
            InitializeComponent();
        }

        public static void UpdateList(ListBox lb)
        {
            for (int i = lb.Items.Count-1; i >= 0; i--)
            {
                lb.Items.RemoveAt(i);
            }
            lb.Items.AddRange(settings.strings);
        }

        public static string[] LoadStringArray()
        {
            ReadConfig();
            return settings.strings;
        }


        //private static bool isConfigParsable(String path)
        //{
        //    if(File.Exists(path))
        //    {
        //        string[] lines = File.ReadAllLines(path);
        //        bool ar = false;
        //        bool me = false;
        //        bool twm = false;
        //        bool tem = false;
        //        bool gb = false;
        //        bool gf = false;
        //        bool b = false;
        //        for(int i=0;i<lines.Length;i++)
        //        {
        //            if(lines[i].StartsWith("ar=") && (lines[i].ToLower() == "ar=false" || lines[i].ToLower() == "ar=true"))
        //            {
        //                ar = true;
        //            }
        //            else if (lines[i].StartsWith("me=") && (lines[i].ToLower() == "me=false" || lines[i].ToLower() == "me=true"))
        //            {
        //                me = true;
        //            }
        //            else if (lines[i].StartsWith("twm="))
        //            {
        //                twm = true;
        //            }
        //            else if (lines[i].StartsWith("tem="))
        //            {
        //                tem = true;
        //            }
        //            else if (lines[i].StartsWith("gb="))
        //            {
        //                gb = true;
        //            }
        //            else if (lines[i].StartsWith("gf="))
        //            {
        //                gf = true;
        //            } else if(lines[i].StartsWith("b"))
        //            {
        //                b = true;
        //            }
        //        }
        //        if(ar && me && twm && tem && gb && gf && b)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public static void SaveConfig()
        {
            FileIO.SaveFile(savepath, settings);
        }
        //{
        //    String path = "config.ini";
        //    if(File.Exists(path))
        //    {
        //        File.Delete(path);
        //    }
        //    String[] config = new String[] { "ar=" + autoreset, "me=" + messages, "twm=" + twmessage, "tem=" + temessage , "gb="+global_background, "gf="+global_foreground, "b1="+b1, "b2=" + b2, "b3=" + b3, "b4=" + b4, "b5=" + b5, "b6=" + b6, "b7=" + b7, "b8=" + b8, "b9=" + b8, "b10=" + b10, "b11=" + b11, "b12=" + b12, "b13=" + b13, "b14=" + b14, "b15=" + b15, "b16=" + b16, "b17=" + b17, "b18=" + b18, "b19=" + b19, "b20=" + b20 };
        //    File.WriteAllLines(path, config);
        //}

        public static void ReadConfig()
        {
            if(File.Exists(savepath))
            settings = FileIO.LoadFile(savepath);
        }
        //{
        //    string path = "config.ini";
        //    if(!isConfigParsable(path))
        //    {
        //        SaveConfig();
        //        return;
        //    } else
        //    {
        //        string[] lines = File.ReadAllLines(path);
        //        for (int i = 0; i < lines.Length; i++)
        //        {
        //            if (lines[i].StartsWith("ar=") && (lines[i].ToLower() == "ar=false" || lines[i].ToLower() == "ar=true"))
        //            {
        //                string[] str = lines[i].Split('=');
        //                if(str[1].ToLower() == "true")
        //                {
        //                    Console.WriteLine("Read true..");
        //                    autoreset = true;
        //                }
        //            }
        //            else if (lines[i].StartsWith("me=") && (lines[i].ToLower() == "me=false" || lines[i].ToLower() == "me=true"))
        //            {
        //                string[] str = lines[i].Split('=');
        //                if (str[1].ToLower() == "true")
        //                {
        //                    messages = true;
        //                }
        //            }
        //            else if (lines[i].StartsWith("twm="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                twmessage = str[1];
        //            }
        //            else if (lines[i].StartsWith("tem="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                temessage = str[1];
        //            }
        //            else if (lines[i].StartsWith("gb="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                int.TryParse(str[1], out global_background);
        //                if(global_background < 0 || global_background > 11)
        //                {
        //                    global_background = 10;
        //                }
        //            }
        //            else if (lines[i].StartsWith("gf="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                int.TryParse(str[1], out global_foreground);
        //                if (global_foreground < 0 || global_foreground > 11)
        //                {
        //                    global_foreground = 10;
        //                }
        //            } else if(lines[i].StartsWith("b1="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b1 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b2="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b2 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b3="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b3 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b4="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b4 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b5="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b5 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b6="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b6 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b7="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b7 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b8="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b8 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b9="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b9 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b10="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b10 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b11="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b11 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b12="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b12 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b13="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b13 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b14="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b14 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b15="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b15 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b16="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b16 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b17="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b17 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b18="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b18 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b19="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b19 = str[1];
        //            }
        //            else if (lines[i].StartsWith("b20="))
        //            {
        //                string[] str = lines[i].Split('=');
        //                b20 = str[1];
        //            }
        //        }
        //    }
        //}

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            if(box.Checked)
            {
                checkBox2.Enabled = true;
                settings.messages = true;
            } else if (!box.Checked)
            {
                checkBox2.Checked = false;
                checkBox2.Enabled = false;
                settings.autoreset = false;
                settings.messages = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            settings.autoreset = box.Checked;
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
            checkBox2.Checked = settings.autoreset;
            checkBox1.Checked = settings.messages;
            if (!settings.messages)
            {
                checkBox2.Enabled = false;
            }
            comboBox1.SelectedIndex = settings.global_background;
            comboBox2.SelectedIndex = settings.global_foreground;

            int color = settings.global_background;
            String back = Settings.colors[color];
            this.BackColor = ColorTranslator.FromHtml(back);
            int color2 = settings.global_foreground;
            String fore = Settings.colors[color2];
            this.ForeColor = ColorTranslator.FromHtml(fore);
            Control[] arr = new Control[this.Controls.Count];
            this.Controls.CopyTo(arr, 0);
            Form1.paintControls(arr, fore, back);

            UpdateList(listBoxStrings);
        }

        private void button_Edit_Click(object sender, EventArgs e)
        {
            Form1.defaultForm.ResetScoreAndButtons();
            ButtonTextInput inp = new ButtonTextInput(listBoxStrings.SelectedIndex, listBoxStrings);
            inp.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox b = (ComboBox)sender;
            settings.global_background = b.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox b = (ComboBox)sender;
            settings.global_foreground = b.SelectedIndex;
        }

        [Serializable]
        public class SavedSettings
        {
            public bool autoreset, messages;
            public string twmessage, temessage;
            public int global_background, global_foreground;
            public string[] strings;

            public SavedSettings(bool _autoreset,bool _messages, string _twmessage, string _temessage, int _background, int _foreground, string[] _strings)
            {
                autoreset = _autoreset;
                messages = _messages;
                twmessage = _twmessage;
                temessage = _temessage;
                global_background = _background;
                global_foreground = _foreground;
                strings = _strings;
            }
        }

        private static class FileIO
        {
            public static void SaveFile(string path, SavedSettings toSave)
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    MemoryStream ms = Serialize(toSave);
                    ms.WriteTo(fs);
                }
            }

            public static SavedSettings LoadFile(string path)
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    return Deserialize<SavedSettings>(fs);
                }
            }

            static private MemoryStream Serialize(object toSerialize)
            {
                if (toSerialize != null)
                {
                    MemoryStream stream = new MemoryStream();
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, toSerialize);
                    stream.Position = 0;
                    return stream;
                }
                else return new MemoryStream();
            }

            static private T Deserialize<T>(Stream stream)
            {
                if (stream.Length > 0)
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream.Seek(0, SeekOrigin.Begin);
                    object o = formatter.Deserialize(stream);
                    return (T)o;
                }
                else return default(T);
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {

        }
    }


    
}
