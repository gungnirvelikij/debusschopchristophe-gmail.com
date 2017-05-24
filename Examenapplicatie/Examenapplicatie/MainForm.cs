﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Examenapplicatie.Properties;

namespace Examenapplicatie
{
    public partial class windowMain : Form
    {
        //
        // Fields
        //

        private int teller = 0;

        private bool applicationRunning = true;

        private byte stateBackground; // 0 = OK, 1 = NOK, 2 = really NOK  

        private string tempPath;
        private string clientPath = "\\App\\" + Resources.applicationFileName; // create client path name

        private Thread checkThread;

        //
        // Constructor
        //

        public windowMain()
        {
            // create temppath
            tempPath = Path.GetTempPath(); // create temp path string 
            tempPath = Path.Combine(tempPath, DateTime.Now.ToString("yyyyMdHHmmss"));
                // create unique folder in tempfolder
            Directory.CreateDirectory(tempPath);

            // close services
                // disable internet: create vbs file in temp folder
            File.WriteAllBytes(tempPath + "\\disableip.vbs", Resources.disableInternet); // copy zip into temp folder
                // disable internet: execute vbs file in temp folder
            //Process scriptProc = new Process();
            //scriptProc.StartInfo.FileName = tempPath + "\\disableip.vbs";
            //scriptProc.Start();
            //scriptProc.WaitForExit();
            //scriptProc.Close();

            // initialize main window

            InitializeComponent();
            Cursor.Current = Cursors.AppStarting; // create loading cursor

            File.WriteAllBytes(tempPath + "\\app.zip", Resources.clientapplication); // copy zip into temp folder
            string installpath = Path.Combine(tempPath, "App"); // create a folder to store host application in
            ZipFile.ExtractToDirectory(tempPath + "\\app.zip", installpath);
                // extract zipfile with exe to tempfolder\App   exe has to be in upper directory of zipfile

            checkThread = new Thread(new ThreadStart(ConstantLoop));
            checkThread.Start(); // create checking loop for checking if application or child has focus

            changeBackground(1); // start at 1 to prefent student from closing and reopening
            Cursor.Current = Cursors.Default;
        }

        //
        // UI methods
        //

        private void achtergrondHerstellenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stateBackground == 1 || stateBackground == 2)
                // only show dialog when background has been changed to NOK or really NOK
            {
                if (showPasswordInputBox())
                {
                    changeBackground(0); // password is OK - background back to normal
                }
                else
                {
                    changeBackground(2); // student tried to cheat fill in password - background to double danger
                }
            }
        }

        private void ApplicatieOpstarten_Click(object sender, EventArgs e)
        {
            if (stateBackground == 0)
            {
                startClientApplication();
            }
            
            ForceToBackground();
                // make sure the main app is forced to the background to prevent previous instances of child app from being hidden
        }

        private void afsluitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeApplication();
        }

        //
        //  Background methods
        //

        // http://stackoverflow.com/questions/7162834/determine-if-current-application-is-activated-has-focus
        /// Returns true if the current application has focus, false otherwise
        private static bool ApplicationIsActivated()
        {
            bool activated = false;
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false; // No window is currently activated
            }

            int procId = Process.GetCurrentProcess().Id;
            Process[] childProcesses = Process.GetProcessesByName(Resources.applicationProcessName);
                // all instances of the 

            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            activated = activeProcId == procId; // check if host application is the active window

            for (int i = 0; i < childProcesses.Length; i++)
                // if not host, check if instance of host application is active window
            {
                if (childProcesses[i].Id == activeProcId)
                {
                    activated = true;
                }
            }

            //test IF   -> toont welk process actief is wanneer achtergrond verandert
            //if (!activated)
            //{
            //    MessageBox.Show(activeProcId.ToString());
            //}
            return activated;
        }

        public void changeBackground(byte state)
        {
            switch (state)
            {
                case 0: // state is OK -> normal background
                    try
                    {
                        BackgroundImage = Resources.backgroundOK;
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Backgroundimage not found");
                    }
                    ForceToBackground();
                    break;
                case 1: // state is NOK -> NOK background
                    try
                    {
                        BackgroundImage = Resources.backgroundNOK;
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Backgroundimage not found");
                    }
                    break;
                case 2: // state is really NOK (wrong password entered) -> really NOK background
                    try
                    {
                        BackgroundImage = Resources.backgroundReallyNOK;
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Backgroundimage not found");
                    }
                    break;
            }
            stateBackground = state;
        }

        private void closeApplication()
        {
            applicationRunning = false;

            // close all instances of the child application
            Process[] childProcesses = Process.GetProcessesByName(Resources.applicationProcessName);
            for (int i = 0; i < childProcesses.Length; i++)
                // if not host, check if instance of host application is active window
            {
                childProcesses[i].Kill();
            }

            //restart services
            WindowsTaskbarEnable();

            // enable internet: create vbs file in temp folder
            File.WriteAllBytes(tempPath + "\\enableipv6.vbs", Resources.enableInternet);

            // enable internet: execute vbs file in temp folder
            Process scriptProc = new Process();
            scriptProc.StartInfo.FileName = tempPath + "\\enableipv6.vbs";
            scriptProc.Start();
            scriptProc.WaitForExit();
            scriptProc.Close();

            try
            {
                Directory.Delete(tempPath, true);  // delete temp folder
            }
            catch (Exception)
            {
                
                throw;
            }
            
            Application.Exit();
        }

        private void ConstantLoop()
        {
            while (applicationRunning)  // constant checking
            {
                if (!ApplicationIsActivated())
                {
                    changeBackground(1);  // change background to NOK
                }
                Thread.Sleep(Int32.Parse(Resources.loopDuration) * 1000); // sleep for amount of seconds determined in resources to avoid computer overcharge
            }
        }

        private void ForceToBackground()  // force host application to background
        {
            if (stateBackground == 0)
            {
                SetWindowPos(Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
            }
        }

        // http://stackoverflow.com/questions/1112981/how-do-i-launch-application-one-from-another-in-c
        private void startClientApplication()
        {
            try
            {
                Process clientProc = new Process();
                clientProc.StartInfo.FileName = tempPath+clientPath;
                clientProc.EnableRaisingEvents = true;

                clientProc.Start();
                WindowsTaskbarDisable();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred!!!: " + ex.Message);
            }
        }

        //http://stackoverflow.com/questions/97097/what-is-the-c-sharp-version-of-vb-nets-inputdialog
        private static bool showPasswordInputBox()
        {
            Size size = new Size(200, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = FormBorderStyle.None;
            inputBox.ClientSize = size;
            inputBox.Text = "Wachtwoord";

            TextBox textBox = new TextBox();
            textBox.Size = new Size(size.Width - 10, 23);
            textBox.Location = new Point(5, 5);
            textBox.UseSystemPasswordChar = true; // hide password when typing
            inputBox.Controls.Add(textBox);

            Button btn_passwordinputOK = new Button();
            btn_passwordinputOK.DialogResult = DialogResult.OK;
            btn_passwordinputOK.Name = "okButton";
            btn_passwordinputOK.Size = new Size(75, 23);
            btn_passwordinputOK.Text = "&OK";
            btn_passwordinputOK.Location = new Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(btn_passwordinputOK);

            inputBox.AcceptButton = btn_passwordinputOK;

            DialogResult result = inputBox.ShowDialog();
            if (textBox.Text.Equals(Resources.password))
            {
                return true;
            }
            return false;
        }

        private void windowMain_Activated(object sender, EventArgs e)
        {
            ForceToBackground();
        }

        private void WindowsTaskbarDisable()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", ""), SW_HIDE);
        }

        private void WindowsTaskbarEnable()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", ""), SW_SHOW);
        }

        //
        //  DLL Imports
        //

        // DLL imports for finding active process
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        // import DLL for forcing window to background
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOACTIVATE = 0x0010;
    }
}
