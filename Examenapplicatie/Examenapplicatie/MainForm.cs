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
        //
        // Fields
        //

        private byte stateBackground;  // 0 = OK, 1 = NOK, 2 = really NOK

        private string clientApplicationPath = "..\\..\\..\\Geogebra\\GeoGebra.exe";  // 3 niveau's terug: Examenapplicatie\Examenapplicatie\bin\Debug\'app'.exe

        //
        // Constructor
        //

        public windowMain()
        {
            InitializeComponent();
        }

        //
        // UI methods
        //

        private void achtergrondHerstellenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(stateBackground==1 || stateBackground == 2)  // only show dialog when background has been changed to NOK or really NOK
            {
                if (showPasswordInputBox())
                {
                    changeBackground(0);  // password is OK - background back to normal
                }
                else
                {
                    changeBackground(2);  // student tried to cheat fill in password - background to double danger
                }
            }
        }

        private void testknop1_Click(object sender, EventArgs e)
        {
            changeBackground(0);  // OK
        }

        private void testknop2_Click(object sender, EventArgs e)
        {
            changeBackground(1);   //  NOK
        }

        private void testknop3_Click(object sender, EventArgs e)
        {
            startClientApplication();
        }

        private void afsluitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeApplication();
        }

        //
        //  Background methods
        //

        public void changeBackground(byte state)
        {
            switch (state)
            {
                case 0:  // state is OK -> normal background
                    try
                    {
                        ActiveForm.BackgroundImage = Resources.backgroundOK;
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Backgroundimage not found");
                    }
                    break;
                case 1:  // state is OK -> normal background
                    try
                    {
                        ActiveForm.BackgroundImage = Resources.backgroundNOK;
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Backgroundimage not found");
                    }
                    break;
                case 2:  // state is OK -> normal background
                    try
                    {
                        ActiveForm.BackgroundImage = Resources.backgroundReallyNOK;
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
            WindowsKey.Enable();
            Application.Exit();
        }

        // http://stackoverflow.com/questions/1112981/how-do-i-launch-application-one-from-another-in-c
        private void startClientApplication()
        {
            try
            {

                Process clientProc = new Process();
                clientProc.StartInfo.FileName = clientApplicationPath;
                clientProc.EnableRaisingEvents = true;

                clientProc.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred!!!: " + ex.Message);
                return;
            }
            WindowsKey.Disable();
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
    }
}
