using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Examenapplicatie.Properties;

namespace Examenapplicatie
{
    public partial class windowMain : Form
    {
        public windowMain()
        {
            InitializeComponent();
        }



        private void achtergrondHerstellenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //
        //  Background methods
        //

        private void changeBackground(bool OK)
        {
            if (OK)
            {
                try
                {
                    ActiveForm.BackgroundImage = Resources.backgroundOK;
                }
                catch (System.IO.FileNotFoundException)
                {
                    MessageBox.Show("Backgroundimage not found");
                }
            }
            else
            {
                try
                {
                    ActiveForm.BackgroundImage = Resources.backgroundNOK;
                }
                catch (System.IO.FileNotFoundException)
                {
                    MessageBox.Show("Backgroundimage not found");
                }
                
            }
        }

        // http://stackoverflow.com/questions/7162834/determine-if-current-application-is-activated-has-focus
        /// <summary>Returns true if the current application has focus, false otherwise</summary>
        private static bool ApplicationIsActivated()
        {
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false;       // No window is currently activated
            }

            var procId = Process.GetCurrentProcess().Id;
            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == procId;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        private void testknop1_Click(object sender, EventArgs e)
        {
            changeBackground(true);
        }

        private void testknop2_Click(object sender, EventArgs e)
        {
            changeBackground(false);
        }
    }
}
