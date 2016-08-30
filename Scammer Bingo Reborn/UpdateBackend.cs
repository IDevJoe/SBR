using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Diagnostics;
using System.Text.RegularExpressions;

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
                    HttpWebRequest r = (HttpWebRequest)WebRequest.Create("https://hexxiumcreations.com/community/ver.txt");
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
                    ver = RemoveWhitespace(ver);
                }
                catch (Exception)
                {
                    return updateAvail(1);
                }
            } else
            {
                server = "Backup";
                try
                {
                    HttpWebRequest r = (HttpWebRequest)WebRequest.Create("https://joethehuman.github.io/projectupdates/sbr/ver.txt");
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
                    ver = RemoveWhitespace(ver);
                }
                catch (Exception)
                {
                    MessageBox.Show("The update servers are unavailable at this time. No updating is not available.", "Updates", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new object[] { (false), Settings.settings.cversion, Settings.settings.cversion, serv };
                }
            }

            if(!checkContent(ver))
            {
                if (serv != 0)
                {
                    MessageBox.Show("The update servers are unavailable at this time. No updating is not available.", "Updates", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new object[] { (false), Settings.settings.cversion, Settings.settings.cversion, serv };
                } else
                {
                    return updateAvail(1);
                }
            }
                
            return new object[]{ (ver != Settings.settings.cversion), ver, Settings.settings.cversion, server, serv };
        }

        public static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        public static async Task<bool> checkForUpdates(Form1 m)
        {
            object[] response = await Task<object[]>.Run(() => UpdateBackend.updateAvail(0));
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

        public static bool checkContent(string l)
        {
            string pattern = @"^\d*\.\d*\.\d*\.\d*$";
            return Regex.IsMatch(l, pattern);
        }

        public static void update(int serv)
        {
            if (serv == 0)
            {
                try
                {
                    System.Diagnostics.Process.Start("https://hexxiumcreations.com/community/latestVersion.zip");
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
                catch (Exception)
                {
                }
            }
        }

    }
}
