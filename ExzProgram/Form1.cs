using System;
using Microsoft.Win32;
using System.IO;
using System.Management;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography;
using System.Drawing;
using System.Linq;
using System.Text;
using Discord;
using Discord.API;
using Discord.Net;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Windows.Forms;
using System.Runtime.InteropServices;

// Fuck you if you have Dnspy, what u looking at here?
namespace ExzProgram
{
    public partial class ExzGamingProgram : Form
    {
        // Whole kit and kaboodle
        public ExzGamingProgram()
        {
            // Yes i am using panels for this, it's eh... Easy i guess
            InitializeComponent();
            panel3.Visible = false;
            panel2.Visible = false;
            timer1.Start();
            pnlInfo.Visible = false;
            Task.Delay(2000);
            panel3.Visible = false;
            panel2.Visible = true;
            pnlInfo.Visible = false;
            Notify1.BalloonTipIcon = ToolTipIcon.Info;
            Notify1.BalloonTipText = "Welcome to Exzyte Program (Version Alpha)";
            Notify1.BalloonTipTitle = "Welcome (Alpha)";
            Notify1.ShowBalloonTip(0);
            MessageBox.Show("Opening...Press OK To open");

            // ------------------ Discord -------------------------------------------------- //

            // The discord presence is still somewhat being worked on.

            // ------------------ Main Registries -------------------------------------------------- //

            // Set the value of the "TestValue" registry key under the "HKEY_CURRENT_USER\TestKey" key
            Registry.SetValue("HKEY_CURRENT_USER\\TestKey", "TestValue", "Test");
            Registry.SetValue("HKEY_CURRENT_USER\\TestKey", "ProgramData", "69v4uhhhxr");

            // Create a random number generator
            Random rand = new Random();

            // Generate a random string
            string randomString = Guid.NewGuid().ToString();

            // Set the value of the "SavedData" registry key under the "HKEY_CURRENT_USER\TestKey" key
            Registry.SetValue("HKEY_CURRENT_USER\\TestKey", "SavedData", randomString);

            // Open the "TestKey" registry key under the "HKEY_CURRENT_USER" key
            RegistryKey key = Registry.CurrentUser.OpenSubKey("TestKey", true);

            // Set the value of the "DataValue" registry key to 0722472557
            key.SetValue("DataValue", (object)0722472557, RegistryValueKind.DWord);

            // ------------------ AppData Folder Stuff -------------------------------------------------- //

            // Get the path to the AppData folder
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Create the "Exzyte" folder in the AppData folder
            string myProgramPath = Path.Combine(appDataPath, "Exzyte");
            Directory.CreateDirectory(myProgramPath);

            // Get the Exzyte folder path
            string exzyteFolder = Path.Combine(appDataPath, "Exzyte");

            // Create the Exzyte folder if it does not exist
            if (!Directory.Exists(exzyteFolder))
            {
                Directory.CreateDirectory(exzyteFolder);
            }

            // --------------- Administrator Permissions --------------------------------------------- //

            // Get the current user's identity
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            // Check if the user has administrator permissions
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                // The user does not have administrator permissions
                MessageBox.Show("This program requires administrator permissions. Please run the program as an administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Close the program
                Environment.Exit(0);
            }

            // --------------- Config Setup --------------------------------------------- //

            // Get the config.ini file path
            string configFilePath = Path.Combine(exzyteFolder, "config.ini");

            // Write the window size to the config.ini file
            File.WriteAllText(configFilePath, $"Width={this.Width}\nHeight={this.Height}");

            // Read the config.ini file into a string array
            string[] lines = File.ReadAllLines(configFilePath);

            // Parse the width and height values from the config.ini file
            int width = 0;
            int height = 0;
            foreach (string line in lines)
            {
                if (line.StartsWith("Width="))
                {
                    width = int.Parse(line.Substring(6));
                }
                else if (line.StartsWith("Height="))
                {
                    height = int.Parse(line.Substring(7));
                }
            }

            // Set the window size to the width and height values from the config.ini file
            this.Width = width;
            this.Height = height;


            // ------------------ SAVING -------------------------------------------------- //

            if (key.GetValue("FirstRun") == null)
            {
                byte[] data = { 0x049, 0x06d, 0x070, 0x06f, 0x072, 0x074, 0x061, 0x06e, 0x074, 0x044, 0x061, 0x074, 0x061 };

                // Set the value of the "BinaryValue" registry key under the "HKEY_CURRENT_USER\TestKey" key
                Registry.SetValue("HKEY_CURRENT_USER\\TestKey", "BinaryValue", data, RegistryValueKind.Binary);

                // The user is new to the program
                MessageBox.Show("Installing necessary program files and saving config, this will only be shown once.");

                // Save the current configs as the "FirstRun" registry key
                key.SetValue("FirstRun", 6278830506, RegistryValueKind.String);
                key.SetValue("VersionData", (object)0.5, RegistryValueKind.String);
            }
            else
            {
                // The user has saved data
                MessageBox.Show("Saved and Loaded.");
                // Set the value of the "ImportantData" registry key to 0722472557
                key.SetValue("ImportantData", (object)0503911, RegistryValueKind.DWord);
            }

            // ------------------ Missing Registry Values -------------------------------------------------- //

            if (key != null)
            {
                // Check if the TestValue value exists in the TestKey key
                if (key.GetValue("TestValue") == null)
                {
                    MessageBox.Show("Important Registry Values missing for the program to work! Re-install the program to fix this problem.");
                    panel3.Visible = false;
                    panel2.Visible = false;
                    pnlInfo.Visible = false;
                    panel3.Visible = false;
                    panel2.Visible = false;
                    pnlInfo.Visible = false;
                    MessageBox.Show("If re-installing the program doesn't work, contact the program's developer.");
                    // The TestValue value does not exist, create the missingValue value
                    int missingCount = 0;
                    if (key.GetValue("missingValue") != null)
                    {
                        // The missingValue value already exists, get the current missing count
                        missingCount = (int)key.GetValue("missingValue");
                    }
                    missingCount++;

                    // Set the missingValue value to the updated missing count
                    key.SetValue("missingValue", missingCount);
                }
            }

            // ------------------ OTHER -------------------------------------------------- //

            // Currently no other stuff here for now
            // This closes the key after all the checks have been completed
            key.Close();
        }

        // timer currently not working
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Check if the required registry key exists
            if (Registry.CurrentUser.OpenSubKey("TestKey") == null)
            {
                // Show the message
                MessageBox.Show("Checking For Required Registry key, this is normal, this timer was made to check if the registry key required for this program is there.");
            }
        }

            private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to exit?", "Close Box", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            pnlInfo.Visible = false;
            panel3.Visible = false; ;
        }

        private void btnTunning_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            MessageBox.Show("The Tunning Is Currently Disabled Until The Program Gets Re-done");
            pnlInfo.Visible = false;
            panel3.Visible = true;
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            MessageBox.Show("The Anti-virus might flag this as a false positive, until this is fixed, this message box will pop up!");
            pnlInfo.Visible = true;
        }

        private void Notify1_MouseClick(object sender, MouseEventArgs e)
        {
            // Show the form when Dblclicked on Notifyicon (DOESNT WORK)
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Soon... New Easter eggs!
            MessageBox.Show("Logo, there's no reason for you to click it multiple times right?");
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            // Lol Easter Egg
            MessageBox.Show("Why are you clicking it?");
        }

        private void label10_Click(object sender, EventArgs e)
        {
            // Pressing this makes the program exit
            MessageBox.Show("Press OK To Exit");
            Environment.Exit(0);
        }

        private void label13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Version Of The Program is BASIC, You can buy it but it is currently WIP");
        }

        private void label12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I know, i will fix this soon!");
        }

        private void eula_Click(object sender, EventArgs e)
        {
            // Create a new form for the EULA page
            Form eulaForm = new Form();

            // Set the form properties
            eulaForm.Text = "EULA";
            eulaForm.Width = 800;
            eulaForm.Height = 600;
            eulaForm.FormBorderStyle = FormBorderStyle.Sizable;

            // Add a WebBrowser control to the form
            WebBrowser eulaBrowser = new WebBrowser();
            eulaBrowser.Dock = DockStyle.Fill;
            eulaForm.Controls.Add(eulaBrowser);

            // Navigate to the EULA page
            eulaBrowser.Navigate("https://www.termsfeed.com/live/aff453bc-40cf-446c-b32f-ce2692f32ee1");

            // Show the form
            eulaForm.Show();
        }
    }
}
