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

        private static int score = 0;

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
            button13.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void buttonClick(object sender)
        {
            score++;
            string newScore = score + "/12";
            label3.Text = newScore;
            ((Button)sender).Enabled = false;
            button13.Focus();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;
            score = 0;
            label3.Text = "0/12";
            button13.Focus();
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
    }
}
