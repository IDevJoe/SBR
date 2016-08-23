﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Scammer_Bingo_Reborn
{
    public partial class Settings : Form

    {
        static string savepath = "config.ini";

        //Default values
        public static SavedSettings settings = new SavedSettings(false, false, "You have successfully found a scammer!", "Halfway there! You can probably rat out the scammer now.", 10, 9, new string[] { "Run", "netstat", "Stopped Services", "I can't understand you sir", "eventvwr", "Secure Server", "msconfig", "The scammer knows...", "cmd", "Do One Thing", "Microsoft Certified", "Corrupted Drivers", "tree", "Network Security", "syskey", "Trying to stick to the script", "Fuck off", "hh h", "support.me", "$$$" }, 5, 4, 0.1f, 0.1f, "1.3.1.0", true, "", false, true);

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

        public static void SaveConfig()
        {
            savepath = GetSavePath();
            int color = Settings.settings.global_background;
            String back = Settings.colors[color];
            Form1.defaultForm.BackColor = ColorTranslator.FromHtml(back);
            int color2 = Settings.settings.global_foreground;
            String fore = Settings.colors[color2];
            Form1.defaultForm.ForeColor = ColorTranslator.FromHtml(fore);
            Control[] arr = new Control[Form1.defaultForm.Controls.Count];
            Form1.defaultForm.Controls.CopyTo(arr, 0);
            Form1.paintControls(arr, fore, back);
            FileIO.SaveFile(savepath, settings);
        }

        public static void ReadConfig()
        {
            savepath = GetSavePath();
            if(File.Exists(savepath))
            settings = FileIO.LoadFile(savepath);
        }

        private static string GetSavePath()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            s += @"\ScammerBingoReborn\config.ini";
            return s;
        }

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
            Form1.defaultForm.ResetScoreAndButtons();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveConfig();
            Form1.defaultForm.ResetScoreAndButtons();
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
            checkBox3.Checked = settings.cfuos;
            checkBox2.Checked = settings.autoreset;
            checkBox1.Checked = settings.messages;
            if (!settings.messages)
            {
                checkBox2.Enabled = false;
            }
            comboBox1.SelectedIndex = settings.global_background;
            comboBox2.SelectedIndex = settings.global_foreground;

            comboBoxDifficulty.SelectedIndexChanged -= comboBoxDifficulty_SelectedIndexChanged; //Detach event method
            if (settings.sizeX == 4 && settings.sizeY == 3)
                comboBoxDifficulty.SelectedIndex = 0;

            else if (settings.sizeX == 5 && settings.sizeY == 4)
                comboBoxDifficulty.SelectedIndex = 1;

            else if (settings.sizeX == 6 && settings.sizeY == 5)
                comboBoxDifficulty.SelectedIndex = 2;

            else
                comboBoxDifficulty.SelectedIndex = 3;

            diffSelectedIndex = comboBoxDifficulty.SelectedIndex;
            comboBoxDifficulty.SelectedIndexChanged += comboBoxDifficulty_SelectedIndexChanged; //Reattach event method
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
            button_Remove.Enabled = false;
            button_Edit.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == comboBox2.SelectedIndex)
            {
                MessageBox.Show("It is not recommended to set the same color as both options. ");
            }
            ComboBox b = (ComboBox)sender;
            settings.global_background = b.SelectedIndex;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == comboBox2.SelectedIndex)
            {
                MessageBox.Show("It is not recommended to set the same color as both options. ");
            }
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
            public int sizeX, sizeY;
            public float whitespaceX, whitespaceY;
            public int[] scoreHistory; //May be implemented in the future
            public string cversion;
            public bool cfuos = true;
            public string discord_token;
            public bool discord_acceptfr;
            public bool discord_pmonly;

            public void AddString(string toAdd)
            {
                string[] newS = new string[strings.Length + 1];
                for (int i = 0; i < strings.Length; i++)
                {
                    newS[i] = strings[i];
                }
                newS[strings.Length] = toAdd;
                strings = newS;
            }

            public void RemoveString(int index)
            {
                int l0 = settings.strings.Length;
                string[] tsarray = new string[l0 - 1];
                for (int i = 0, c = 0; i < l0; i++)
                {
                    if (i == index)
                        continue;

                    tsarray[c] = settings.strings[i];
                    c++;
                }
                settings.strings = tsarray;
            }

            internal void EditString(int i, string str)
            {
                strings[i] = str;
            }

            public SavedSettings(bool _autoreset,bool _messages, string _twmessage, string _temessage, int _background, int _foreground, string[] _strings,int _sizeX, int _sizeY, float _whitespaceX, float _whitespaceY, string _cversion, bool _cfuos, string _token, bool _discordaccfr, bool _discordpmonly)
            {
                autoreset = _autoreset;
                messages = _messages;
                twmessage = _twmessage;
                temessage = _temessage;
                global_background = _background;
                global_foreground = _foreground;
                strings = _strings;
                sizeX = _sizeX;
                sizeY = _sizeY;
                whitespaceX = _whitespaceX;
                whitespaceY = _whitespaceY;
                cversion = _cversion;
                cfuos = _cfuos;
                discord_token = _token;
                discord_acceptfr = _discordaccfr;
                discord_pmonly = _discordpmonly;
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            Form1.defaultForm.ResetScoreAndButtons();
            ButtonTextInput inp = new ButtonTextInput(-1, listBoxStrings);
            inp.ShowDialog();
            button_Remove.Enabled = false;
            button_Edit.Enabled = false;
        }

        private void button_Remove_Click(object sender, EventArgs e)
        {
            if (settings.strings.Length > settings.sizeX * settings.sizeY)
            {
                settings.RemoveString(listBoxStrings.SelectedIndex);
                SaveConfig();
                UpdateList(listBoxStrings);
                button_Remove.Enabled = false;
                button_Edit.Enabled = false;
            }
            else
            {
                MessageBox.Show(null, "You can't remove anymore buttons due to the set difficulty.", "Rejected!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxStrings_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_Edit.Enabled = true;
            button_Remove.Enabled = true;
        }

        private int diffSelectedIndex;

        private void comboBoxDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBoxDifficulty.SelectedIndex)
            {
                case 0:
                    if(settings.strings.Length >= 4*3)
                    {
                        settings.sizeX = 4;
                        settings.sizeY = 3;
                    }
                    else
                    {
                        MessageBox.Show("You need to have at least " + 4 * 3 + " strings to select this, difficulty, add " + (4*3- settings.strings.Length) + " more!");
                    }

                    
                    break;
                case 1:
                    if (settings.strings.Length >= 5 * 4)
                    {
                        settings.sizeX = 5;
                        settings.sizeY = 4;
                    }
                    else
                    {
                        comboBoxDifficulty.SelectedIndex = 0;
                        MessageBox.Show("You need to have at least " + 5 * 4 + " strings to select this, difficulty, add " + (5 * 4 - settings.strings.Length) + " more!");
                    }
                    break;
                case 2:
                    if (settings.strings.Length >= 6 * 5)
                    {
                        settings.sizeX = 6;
                        settings.sizeY = 5;
                    }
                    else
                    {
                        comboBoxDifficulty.SelectedIndex = 1;
                        MessageBox.Show("You need to have at least " + 6 * 5 + " strings to select this, difficulty, add " + (6 * 5 - settings.strings.Length) + " more!");
                    }
                    break;
                case 3:
                    new CustomSizeSelector(diffSelectedIndex,comboBoxDifficulty).ShowDialog();
                    break;
            }
            diffSelectedIndex = comboBoxDifficulty.SelectedIndex;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            settings.cfuos = checkBox3.Checked;
        }

        private void button_Default_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to reset all of the settings to default?(requires restart)", "Reset to default", MessageBoxButtons.OKCancel);
            if(result == DialogResult.OK)
            {
                if(File.Exists(GetSavePath()))
                {
                    File.Delete(GetSavePath());
                }
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Process.GetCurrentProcess().Kill();

            }
        }
    } 
}