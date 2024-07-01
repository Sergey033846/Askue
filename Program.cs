using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;

namespace askue3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.FirstChanceException += (sender, e) => {
                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                msg.AppendLine(e.Exception.GetType().FullName);
                msg.AppendLine(e.Exception.Message);
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                msg.AppendLine(st.ToString());
                msg.AppendLine();
                String desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string logFilePath = String.Format("{0}\\{1}", desktopPath, "logfile.txt");
                //Console.WriteLine(msg.ToString());
                //System.Diagnostics.Debug.WriteLine(msg.ToString());
                System.IO.File.AppendAllText(logFilePath, msg.ToString());
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            
            Application.Run(new FormMain());
        }
    }
}
