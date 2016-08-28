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
using Microsoft.Win32;
using System.Reflection;
using System.Diagnostics;
using System.IO.Compression;
using IWshRuntimeLibrary;

namespace SBR_Installer
{
    public partial class Form1 : Form
    {

        private bool Update = false;
        public static Guid GUID = Guid.Parse("1a30eea6-b8a9-4931-925a-ba9b471f9755");

        public Form1()
        {
            InitializeComponent();
        }

        private string InstallPath()
        {
            return PF() + "\\SBR";
        }

        private string PF()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            if(Update)
            {
                label9.Text = "Killing all SBR Tasks";
                foreach (var process in Process.GetProcessesByName("Scammer Bingo Reborn"))
                {
                    process.Kill();
                }
                System.Threading.Thread.Sleep(2000);
                label9.Text = "Preparing directory for new files...";
                System.IO.DirectoryInfo di = new DirectoryInfo(InstallPath());

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            } else
            {
                label9.Text = "Creating Directory..";
                Directory.CreateDirectory(InstallPath());
            }
            label9.Text = "Extracting files...";
            ZipFile.ExtractToDirectory("appFiles.zip", InstallPath());
            label9.Text = "Registering application with Windows..";
            System.IO.File.Copy("setup.exe", InstallPath() + "\\Installer.exe");
            CreateUninstaller();
            label9.Text = "Creating start menu shortcut...";
            AddShortcut();
            label9.Text = "Installed!";
            DialogResult l = MessageBox.Show(null, "Installation Complete. Would you like to open what you just installed?", "Install Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(l == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(InstallPath() + "\\Scammer Bingo Reborn.exe");
            }
            this.Dispose();
        }

        private int perc(int i)
        {
            return i / 6;
        }

        private void AddShortcut()
        {
            string pathToExe = @""+InstallPath()+"\\Scammer Bingo Reborn.exe";
            string commonStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
            string appStartMenuPath = Path.Combine(commonStartMenuPath, "Programs", "Scammer Bingo Reborn");

            if (!Directory.Exists(appStartMenuPath))
                Directory.CreateDirectory(appStartMenuPath);

            string shortcutLocation = Path.Combine(appStartMenuPath, "Scammer Bingo Reborn" + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "DevJoe's version of Scammer Bingo";
            //shortcut.IconLocation = @"C:\Program Files (x86)\TestApp\TestApp.ico"; //uncomment to set the icon of the shortcut
            shortcut.TargetPath = pathToExe;
            shortcut.Save();
        }

        private void RemoveShortcut()
        {
            string pathToExe = @"" + InstallPath() + "\\Scammer Bingo Reborn.exe";
            string commonStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
            string appStartMenuPath = Path.Combine(commonStartMenuPath, "Programs", "Scammer Bingo Reborn");


            System.IO.DirectoryInfo di = new DirectoryInfo(appStartMenuPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
            System.IO.Directory.Delete(appStartMenuPath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(System.IO.File.Exists(InstallPath()+"\\Scammer Bingo Reborn.exe"))
            {
                Update = true;
                button1.Text = "Update";
                button3.Enabled = true;
            }
            if(Directory.Exists(InstallPath()) && !Update)
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(InstallPath());

                foreach (FileInfo file in di.GetFiles())
                {
                     file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                Directory.Delete(InstallPath());
            }
        }

        private void CreateUninstaller()
        {
            using (RegistryKey parent = Registry.LocalMachine.OpenSubKey(
                         @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true))
            {
                if (parent == null)
                {
                    throw new Exception("Uninstall registry key not found.");
                }
                try
                {
                    RegistryKey key = null;

                    try
                    {
                        string guidText = GUID.ToString("B");
                        key = parent.OpenSubKey(guidText, true) ??
                              parent.CreateSubKey(guidText);

                        if (key == null)
                        {
                            throw new Exception(String.Format("Unable to create uninstaller '{0}\\{1}'", @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", guidText));
                        }

                        Assembly asm = GetType().Assembly;
                        Version v = asm.GetName().Version;
                        string exe = InstallPath()+"\\Installer.exe";

                        key.SetValue("DisplayName", "Scammer Bingo Reborn");
                        key.SetValue("ApplicationVersion", v.ToString());
                        key.SetValue("Publisher", "DevJoe");
                        key.SetValue("DisplayIcon", exe);
                        key.SetValue("DisplayVersion", v.ToString(2));
                        key.SetValue("URLInfoAbout", "http://sbr.devjoe.me");
                        key.SetValue("Contact", "devjoebusiness@gmail.com");
                        key.SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"));
                        key.SetValue("UninstallString", exe + "");
                    }
                    finally
                    {
                        if (key != null)
                        {
                            key.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "An error occurred writing uninstall information to the registry.  The service is fully installed but can only be uninstalled manually through the command line.",
                        ex);
                }
            }
        }

        private void RemoveUninstaller()
        {
            using (RegistryKey parent = Registry.LocalMachine.OpenSubKey(
                         @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true))
            {
                if (parent == null)
                {
                    throw new Exception("Uninstall registry key not found.");
                }
                try
                {
                    RegistryKey key = null;

                    try
                    {
                        string guidText = GUID.ToString("B");
                        key = parent.OpenSubKey(guidText, true) ??
                              parent.CreateSubKey(guidText);

                        if (key == null)
                        {
                            throw new Exception(String.Format("Unable to create uninstaller '{0}\\{1}'", @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", guidText));
                        }

                        parent.DeleteSubKey(guidText);
                    }
                    finally
                    {
                        if (key != null)
                        {
                            key.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "An error occurred writing uninstall information to the registry.  The service is fully installed but can only be uninstalled manually through the command line.",
                        ex);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label9.Text = "Killing all SBR Tasks";
            foreach (var process in Process.GetProcessesByName("Scammer Bingo Reborn"))
            {
                process.Kill();
            }
            System.Threading.Thread.Sleep(2000);
            label9.Text = "Removing registry key..";
            RemoveUninstaller();
            label9.Text = "Removing from start menu...";
            RemoveShortcut();
            label9.Text = "Removing directory..";
            System.IO.DirectoryInfo di = new DirectoryInfo(InstallPath());

            foreach (FileInfo file in di.GetFiles())
            {
                if (!file.Name.ToLower().Contains("installer"))
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
            label9.Text = "Done!";
            MessageBox.Show(null, "Uninstall completed", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }
    }
}
