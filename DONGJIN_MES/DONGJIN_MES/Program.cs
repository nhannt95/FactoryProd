using Office2013StyleSplashScreen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Spire.Barcode;
using AutoUpdaterDotNET;
using DONGJIN_MES.IUserControl;

namespace DONGJIN_MES
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BarcodeSettings.ApplyKey("IEPUJ-H4VXN-DLD2K-8LZAR-HXBCR");
            Process[] processlist = Process.GetProcesses();
            string assamely = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            foreach (Process theprocess in processlist)
            {
                if (theprocess.ProcessName == assamely + ".exe")
                {
                    MessageBox.Show(assamely + " is running", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginV2());
        }
    }
}
