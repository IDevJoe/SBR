using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace Scammer_Bingo_Reborn
{
    class UpdateBackend
    {

        public static object[] updateAvail()
        {
            HttpWebRequest r = WebRequest.CreateHttp("http://hexxiumcreations.com/community/ver.txt");
            r.UserAgent = "Scammer Bingo Auto-Update";
            WebResponse resp = r.GetResponse();
            Stream data = resp.GetResponseStream();
            StreamReader read = new StreamReader(data);
            string ver = read.ReadToEnd();
            read.Close();
            resp.Close();
            return new object[]{ (ver != Settings.settings.cversion), ver, Settings.settings.cversion };
        }

        public static void checkForUpdates(Form1 m)
        {
            object[] response = UpdateBackend.updateAvail();
            bool av = (bool)response[0];
            string latest = (string)response[1];
            string current = (string)response[2];
            if (av)
            {
                DialogResult res = MessageBox.Show(null, "An update is available!\nInstalled version: " + current + "\nLatest version: " + latest + "\n\nWould you like to install this update now?", "Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(res == DialogResult.Yes)
                {
                    update();
                }
            }
        }

        public static void update()
        {
            WebClient l = new WebClient();
            l.DownloadFile("https://hexxiumcreations.com/community/latestVersion.zip", "update.zip");
            ZipFile.ExtractToDirectory(@"update.zip", @"update");
            File.Delete(@"update.zip");
            System.Diagnostics.Process.Start(@"update/setup.exe");
        }

    }
}
