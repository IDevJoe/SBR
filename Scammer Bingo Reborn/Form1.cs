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
    public partial class Form1 : Form
    {
        //Global Variables
        private static int score = 0;
        public static Form1 defaultForm = null;
        Button[,] btns;

        //Offset from the top of the GroupBox (needed or the first button will go on top of the GroupBox's text)
        private const int offsetY = 10;

        public Form1()
        {
            InitializeComponent();
            defaultForm = this;
            Settings.settings.cversion = "1.4.0.0";
            Settings.SaveConfig();
            if (Directory.Exists("update")) Directory.Delete("update", true);
        }

        private void aboutSBRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PrepareButtons(Settings.LoadStringArray());
            buttonReset.Focus();
            int color = Settings.settings.global_background;
            String back = Settings.colors[color];
            this.BackColor = ColorTranslator.FromHtml(back);
            int color2 = Settings.settings.global_foreground;
            String fore = Settings.colors[color2];
            this.ForeColor = ColorTranslator.FromHtml(fore);
            Control[] arr = new Control[this.Controls.Count];
            this.Controls.CopyTo(arr, 0);
            paintControls(arr, fore, back);
            if(Settings.settings.cfuos)
            UpdateBackend.checkForUpdates(this);
        }

        public static void paintControls(Control[] arr, String fore, String back)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].GetType().Name.ToLower() != "menustrip" && arr[i].GetType().Name.ToLower() != "toolstripmenuitem")
                {
                    if (arr[i].HasChildren)
                    {
                        arr[i].ForeColor = ColorTranslator.FromHtml(fore);
                        arr[i].BackColor = ColorTranslator.FromHtml(back);
                        Control[] cls = new Control[arr[i].Controls.Count];
                        arr[i].Controls.CopyTo(cls, 0);
                        paintControls(cls, fore, back);
                    }
                    else {
                        arr[i].ForeColor = ColorTranslator.FromHtml(fore);
                        arr[i].BackColor = ColorTranslator.FromHtml(back);
                        if(arr[i] is LinkLabel)
                        {
                            LinkLabel b = (LinkLabel)arr[i];
                            b.LinkColor = ColorTranslator.FromHtml(fore);
                        }
                    }
                }
            }
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        { 
            ResetScoreAndButtons(false);
        }

        private void BingoButton_Click(object sender, EventArgs e)
        {
            buttonClick(sender, false);
        }

        //Initialize and set button names 
        public void PrepareButtons(string[] strings)
        {
            GroupBox container = groupBox_BingoBoard;

            //Remove existing buttons before creating new ones
            if (btns!=null)
            {
                foreach (Button btn in btns)
                {
                    if (container.Controls.Contains(btn))
                    {
                        container.Controls.Remove(btn);
                        btn.Dispose();
                    }
                }
            }

            string[,] buttonText = SelectButtonNames(Settings.settings.sizeX, Settings.settings.sizeY, strings);

            btns = new Button[buttonText.GetLength(0), buttonText.GetLength(1)];

            for (int i = 0; i < buttonText.GetLength(0); i++)
            {
                for (int j = 0; j < buttonText.GetLength(1); j++)
                {
                    btns[i, j] = new Button();
                    btns[i, j].Name = "btn" + i + "." + j;
                    btns[i, j].Text = buttonText[i, j];
                    btns[i, j].Click += BingoButton_Click;
                }
            }
            ArrangeButtons(btns, container);
        }

        private string[,] SelectButtonNames(int X, int Y, string[] buttonTextPool)
        {
            string[,] stringArray = new string[X, Y];
            bool[] stringAlreadyPicked = new bool[buttonTextPool.Length];
            for (int i = 0; i < stringAlreadyPicked.Length; i++)
            {
                stringAlreadyPicked[i] = false;
            }
            Random RNG = new Random();

            for (int i = 0; i < Settings.settings.sizeX; i++)
            {
                for (int j = 0; j < Settings.settings.sizeY; j++)
                {
                    int ti;
                    do
                    {
                        ti = RNG.Next(0, buttonTextPool.Length);


                    } while (stringAlreadyPicked[ti]);
                    stringArray[i, j] = buttonTextPool[ti];
                    stringAlreadyPicked[ti] = true;
                }
            }

            return stringArray;
        }

        private void ArrangeButtons(Button[,] btns, GroupBox container)
        {
            int nX = btns.GetLength(0), nY = btns.GetLength(1);
            int maxX = container.Width, maxY = container.Height - offsetY;
            for (int i = 0; i < nX; i++)
            {
                for (int j = 0; j <nY; j++)
                {
                    int posX = Convert.ToInt32(((float)maxX / nX) * (i + Settings.settings.whitespaceX/2));
                    int posY = Convert.ToInt32(((float)maxY / nY) * ( j + Settings.settings.whitespaceY/2)) + offsetY;
                    btns[i, j].Location = new Point(posX, posY);

                    int width, heigth;
                    width = Convert.ToInt32((float)maxX / nX * (1 - Settings.settings.whitespaceX));
                    heigth = Convert.ToInt32((float)maxY / nY * (1 -Settings.settings.whitespaceY));

                    btns[i, j].Size = new Size(width, heigth);

                    this.groupBox_BingoBoard.Controls.Add(btns[i, j]);
                }
            }
        }

        public void buttonClick(object sender, bool l)
        {
            if (BotBackend.last != null && BotBackend.last.connected && !l)
            {
                MessageBox.Show("This option is not available right now as Discord is controlling the board.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            score++;
            string newScore = score + "/" + btns.Length;
            label3.Text = newScore;
            ((Button)sender).Enabled = false;
            buttonReset.Focus();
            if(score == btns.Length/2 && Settings.settings.messages && !l)
            {
                MessageBox.Show(Settings.settings.temessage + "\n\n(You can disable these messages in SBR -> Settings)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else if(score == btns.Length && Settings.settings.messages && !l)
            {
                MessageBox.Show(Settings.settings.twmessage + "\n\n(You can disable these messages in SBR -> Settings)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(Settings.settings.autoreset)
                {
                    ResetScoreAndButtons(false);
                }
            }
        }

        public void ResetScoreAndButtons(bool l)
        {

            if (BotBackend.last != null && BotBackend.last.connected && !l)
            {
                MessageBox.Show("This option is not available right now as Discord is controlling the board.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            score = 0;
            label3.Text = "0/" + btns.Length;
            foreach (Button btn in btns)
            {
                btn.Enabled = true;
            }
            PrepareButtons(Settings.LoadStringArray());
            buttonReset.Focus();
        }

        private void contributorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/JoeTheHuman/Scammer-Bingo-Reborn/graphs/contributors");
        }

        private void learnHowToContributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/JoeTheHuman/Scammer-Bingo-Reborn/wiki/How-to-contribute");
        }

        private void releasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/JoeTheHuman/Scammer-Bingo-Reborn/releases");
        }

        private void reportAnIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://support.hexxiumcreations.com");
        }

        private void contactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/JoeTheHuman/Scammer-Bingo-Reborn/wiki/Support");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/JoeTheHuman/Scammer-Bingo-Reborn/wiki/How-to-contribute");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.fakeaddressgenerator.com/World/us_address_generator");
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Settings g = new Settings()) g.ShowDialog();
        }

        private void androidScammerBingoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://play.google.com/store/apps/details?id=com.xelitexirish.scammerbingo");
        }

        private void youTubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/user/LewissTech");
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool res = UpdateBackend.checkForUpdates(this);
            if(!res)
            {
                MessageBox.Show(null, "You're already on the latest update!", "No update available", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Settings.settings.discord_token))
            {
                MessageBox.Show(null, "It appears you don't have a token setup yet, so let me guide you through how to use this.\n\nTo start off, you need to generate a bot token. This can be done by going to [discordapp.com], hovering over the More option, then clicking Developers. Now on the left side, you should see My Applications, click that, and create a new application. Name it, then click Create Application near the bottom. A new page should come up, and you should click \"Create Bot User\". A token option should appear, so you should click reveal, and paste it into the form.\n\nTo add the bot to the server, use the Auth link generator found on [Discord -> Auth Link Gen]", "Discord Integration", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            using(ManageDiscordSettings s = new ManageDiscordSettings()) s.ShowDialog();
        }

        private void authLinkGenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiscordAuthLinkGen.Form1 f = new DiscordAuthLinkGen.Form1();
            Control.ControlCollection l = f.Controls;
            int color = Settings.settings.global_background;
            String back = Settings.colors[color];
            f.BackColor = ColorTranslator.FromHtml(back);
            int color2 = Settings.settings.global_foreground;
            String fore = Settings.colors[color2];
            f.ForeColor = ColorTranslator.FromHtml(fore);
            Control[] arr = new Control[l.Count];
            l.CopyTo(arr, 0);
            paintControls(arr, fore, back);
            f.ShowDialog();
        }
    }
}
