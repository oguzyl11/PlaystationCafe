using System;
using System.Windows.Forms;
using GameCenterAI.WinForms;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Main entry point for the application.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmGiris());
        }
    }
}


