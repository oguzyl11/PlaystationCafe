using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
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

            // DevExpress Skin ayarları
            try
            {
                SkinManager.EnableFormSkins();
                SkinManager.EnableMdiFormSkins();

                // Modern skin ayarları - Office 2019 Colorful veya Office 2016 Colorful
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
                // Alternatif: "Office 2016 Colorful", "The Bezier", "Visual Studio 2019 Blue"

                // Form border ve title bar ayarları
                UserLookAndFeel.Default.UseWindowsXPTheme = false;
                UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
            }
            catch
            {
                // Skin ayarları başarısız olursa varsayılan ayarları kullan
            }

            Application.Run(new FrmGiris());
        }
    }
}


