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
    public partial class Form1 : Form
    {
        //Global Variables
        private static int score = 0;
        public static Form1 defaultForm = null;
        Button[,] btns;

        //// "SETTINGS"

        //Button text
        private string[] buttonTextPool = 
            {
                "Run","netstat","Stopped Services", "I can't understand you sir","eventvwr","Secure Server","msconfig","The scammer knows...","cmd","Do One Thing","Microsoft Certified", "Corrupted Drivers","tree","Network Security","syskey","Trying to stick to the script","Fuck off", "hh h", "support.me", "$$$"
            };

        int sizeX = 5, sizeY = 4;

        //Percentage of white space between buttons
        private float whitespaceX = 0.1f, whitespaceY = 0.1f;

        //Offset from the top of the GroupBox (needed or the first button will go on top of the GroupBox's text)
        private int offsetY = 10;
        ////

        public Form1()
        {
            InitializeComponent();
            defaultForm = this;
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
                    }
                }
            }
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        { 
            label3.Text = "0/" + btns.Length;

            buttonReset.Focus();
            PrepareButtons(Settings.LoadStringArray());

            ResetScoreAndButtons();

        }

        private void BingoButton_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
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

            string[,] buttonText = SelectButtonNames(sizeX, sizeY, strings);

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

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
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
                    int posX = Convert.ToInt32(((float)maxX / nX) * (i + whitespaceX/2));
                    int posY = Convert.ToInt32(((float)maxY / nY) * ( j + whitespaceY/2)) + offsetY;
                    btns[i, j].Location = new Point(posX, posY);

                    int width, heigth;
                    width = Convert.ToInt32((float)maxX / nX * (1 - whitespaceX));
                    heigth = Convert.ToInt32((float)maxY / nY * (1 -whitespaceY));

                    btns[i, j].Size = new Size(width, heigth);

                    this.groupBox_BingoBoard.Controls.Add(btns[i, j]);
                }
            }
        }

        private void buttonClick(object sender)
        {
            score++;
            string newScore = score + "/" + btns.Length;
            label3.Text = newScore;
            ((Button)sender).Enabled = false;
            buttonReset.Focus();
            if(score == btns.Length/2 && Settings.settings.messages)
            {
                MessageBox.Show(Settings.settings.temessage + "\n\n(You can disable these messages in SBR -> Settings)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else if(score == btns.Length && Settings.settings.messages)
            {
                MessageBox.Show(Settings.settings.twmessage + "\n\n(You can disable these messages in SBR -> Settings)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(Settings.settings.autoreset)
                {
                    ResetScoreAndButtons();
                }
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetScoreAndButtons();
        }

        public void ResetScoreAndButtons()
        {
            score = 0;
            label3.Text = "0/" + btns.Length;
            foreach (Button btn in btns)
            {
                btn.Enabled = true;
            }
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
            System.Diagnostics.Process.Start("https://github.com/JoeTheHuman/Scammer-Bingo-Reborn/issues/new");
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
            new Settings().ShowDialog();
        }

        private void androidScammerBingoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://play.google.com/store/apps/details?id=com.xelitexirish.scammerbingo");
        }
    }
}
