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
        //Joe is awesome :D
        private static int score = 0;

        //
        private string[,] buttonText = 
            {
                { "these","are","just" },
                { "as1","a23123s","a345s" },
                { "these","are354345","just" },
                { "5345as","a354s","as" }
            };

        //Percentage of white space between buttons
        private float whitespaceX = 0.1f, whitespaceY = 0.1f;
        //Offset from the top of the GroupBox (needed or the first button will go on top of the GroupBox's text)
        private int offsetY = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void aboutSBRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonReset.Focus();

            Button[,] btns = new Button[buttonText.GetLength(0),buttonText.GetLength(1)];


            for (int i = 0; i < buttonText.GetLength(0); i++)
            {
                for (int j = 0; j < buttonText.GetLength(1); j++)
                {
                    btns[i,j] = new Button();
                    btns[i,j].Name = "btn" + i + "." + j;
                    btns[i,j].Text = buttonText[i,j];
                }
            }
            ArrangeButtons(btns, groupBox_BingoBoard);

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
            string newScore = score + "/20";
            label3.Text = newScore;
            ((Button)sender).Enabled = false;
            buttonReset.Focus();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {


            score = 0;
            label3.Text = "0/20";
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
    }
}
