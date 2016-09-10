using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Scammer_Bingo_Reborn
{
    public partial class NumberDialog : Form
    {
        public NumberDialog()
        {
            InitializeComponent();
        }

        private void NumberDialog_Load(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("https://sbr.devjoe.me/api/numbers.php?req=get&limit=50");
            string JSON = "{}";
            using (WebResponse response = request.GetResponse())
            {
                using(Stream data = response.GetResponseStream())
                {
                    using(StreamReader read = new StreamReader(data))
                    {
                        JSON = read.ReadToEnd();
                    }
                }
            }
            Console.WriteLine("Read JSON: " + JSON);
            GETNumberEntries.RootObject l = JsonConvert.DeserializeObject<GETNumberEntries.RootObject>(JSON);
            if(!l.success)
            {
                Console.WriteLine("Error was: " + l.error);
                MessageBox.Show(this, "The numbers were unable to load from the server due to a server error.\n\nDetails:\n" + l.error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
                return;
            }
            GETNumberEntries.Datum[] entries = l.data.ToArray();
            for(int i=0;i<entries.Length;i++)
            {
                listView1.Items.Add(entries[i].id).SubItems.AddRange(new string[] { "+"+entries[i].country+" "+entries[i].number, entries[i].submitted_name, entries[i].submitted_date });
            }
            Control[] toBePainted = new Control[this.Controls.Count];
            int color = Settings.settings.global_background;
            String back = Settings.colors[color];
            this.BackColor = ColorTranslator.FromHtml(back);
            int color2 = Settings.settings.global_foreground;
            String fore = Settings.colors[color2];
            this.ForeColor = ColorTranslator.FromHtml(fore);
            Control[] arr = new Control[this.Controls.Count];
            this.Controls.CopyTo(arr, 0);
            Form1.paintControls(arr, fore, back);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool good1 = false;
            if(textBox1.MaskCompleted)
            {
                good1 = true;
            }
            if(textBox2.MaskCompleted && good1) {

                string toSend = "?req=add&data=%2B"+textBox2.Text.Trim().Substring(1);

                toSend += "&specifiedname=" +textBox1.Text.Trim();

                WebRequest r = WebRequest.Create("https://sbr.devjoe.me/api/numbers.php" + toSend);
                string JSON = "{}";
                using (WebResponse l = r.GetResponse())
                {
                    using(Stream strem = l.GetResponseStream())
                    {
                        using(StreamReader stream = new StreamReader(strem))
                        {
                            JSON = stream.ReadToEnd();
                        }
                    }
                }
                Console.WriteLine("Read: " + JSON);
                ADDNumberEntries.RootObject ANE = JsonConvert.DeserializeObject<ADDNumberEntries.RootObject>(JSON);
                if(!ANE.success)
                {
                    Console.WriteLine("Error: " + ANE.error);
                    MessageBox.Show(this, "The request failed.\n\nDetails: "+ANE.error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show(this, "Success! Here's the summary of what was sent:\n\nIP: " + ANE.summary.ip + "\nNumber: +" + ANE.summary.country + " " + ANE.summary.number + "\nName: " + ANE.summary.name, "Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.ResetText();
                textBox2.ResetText();

            }  else
            {
                MessageBox.Show(this, "Please fill in all the fields correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if(e.IsSelected)
            {
                reportAsNotWorkingToolStripMenuItem.Enabled = true;
            } else
            {
                reportAsNotWorkingToolStripMenuItem.Enabled = false;
            }
        }

        private void reportAsNotWorkingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(this, "WARNING: Abusing this feature WILL get you blacklisted. Are you sure you wish to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(r != DialogResult.Yes)
            {
                return;
            }
            ListView.SelectedListViewItemCollection m = listView1.SelectedItems;
            ListViewItem[] items = new ListViewItem[m.Count];
            m.CopyTo(items, 0);
            int id = Int32.Parse(items[0].Text);
            WebRequest request = WebRequest.Create("https://sbr.devjoe.me/api/numbers.php?req=report&id="+id);
            string JSON = "{}";
            using (WebResponse response = request.GetResponse())
            {
                using(Stream data = response.GetResponseStream())
                {
                    using(StreamReader read = new StreamReader(data))
                    {
                        JSON = read.ReadToEnd();
                    }
                }
            }
            ReportEntries.RootObject re = JsonConvert.DeserializeObject<ReportEntries.RootObject>(JSON);
            if(!re.success)
            {
                MessageBox.Show(this, "An error occurred while sending the request.\n\nDetails:\n" + re.error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                MessageBox.Show(this, "Success! Here's a summary of what was sent:\n\nIP: " + re.summary.ip + "\nType: " + re.summary.type + "\nID: " + re.summary.id, "Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
