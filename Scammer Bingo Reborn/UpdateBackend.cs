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

        public static object[] updateAvail(int serv)
        {
            string ver = "";
            string server = "";
            if (serv == 0)
            {
                server = "Primary";
                try
                {
                    HttpWebRequest r = WebRequest.CreateHttp("http://www.hexxiumcreations.com/community/ver.txt");
                    r.UserAgent = "Scammer Bingo Auto-Update";
                    using (WebResponse resp = r.GetResponse())
                    {
                        using (Stream data = resp.GetResponseStream())
                        {
                            using (StreamReader read = new StreamReader(data))
                            {
                                ver = read.ReadToEnd();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    return updateAvail(1);
                }
            } else
            {
                server = "Backup";
                try
                {
                    HttpWebRequest r = WebRequest.CreateHttp("http://joethehuman.github.io/projectupdates/sbr/ver.txt");
                    r.UserAgent = "Scammer Bingo Auto-Update";
                    using (WebResponse resp = r.GetResponse())
                    {
                        using (Stream data = resp.GetResponseStream())
                        {
                            using (StreamReader read = new StreamReader(data))
                            {
                                ver = read.ReadToEnd();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    return new object[] { (false), Settings.settings.cversion, Settings.settings.cversion };
                }
            }

                
            return new object[]{ (ver != Settings.settings.cversion), ver, Settings.settings.cversion, server, serv };
        }

        public static bool checkForUpdates(Form1 m)
        {
            object[] response = UpdateBackend.updateAvail(0);
            bool av = (bool)response[0];
            string latest = (string)response[1];
            string current = (string)response[2];
            if (av)
            {
                DialogResult res = MessageBox.Show(null, "An update is available!\nInstalled version: " + current + "\nLatest version: " + latest + "\n\nWould you like to install this update now?", "Update available ("+((string)response[3])+" server)", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(res == DialogResult.Yes)
                {
                    update(0);
                }
            }
            return av;
        }

        public static void update(int serv)
        {
            if (serv == 0)
            {
                try
                {
                    WebClient l = new WebClient();
                    l.DownloadFile("https://www.hexxiumcreations.com/community/latestVersion.zip", "latestVersion.zip");

                    ZipFile.ExtractToDirectory("latestVersion.zip", "update");
                    File.Delete("update.zip");
                    Console.WriteLine("Starting...");
                    System.Diagnostics.Process.Start("update\\setup.exe");
                } catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    update(1);
                }
            } else
            {
                try
                {
                    WebClient l = new WebClient();
                    l.DownloadFile("https://joethehuman.github.io/projectupdates/sbr/lastestVersion.txt", "latestVersion.zip");

                    ZipFile.ExtractToDirectory("latestVersion.zip", "update");
                    File.Delete("update.zip");
                    System.Diagnostics.Process.Start("update\\setup.exe");
                }
                catch (Exception e)
                {
                }
            }
        }

    }
}
