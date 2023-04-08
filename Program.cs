using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace obs_client
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application run 
            XmlConfigurator.Configure();
            FormLogin formLogin = new FormLogin();
            formLogin.ShowDialog();
            if (formLogin.DialogResult == DialogResult.OK) 
            {
                Application.Run(new obs_client());
            }
        }
    }
}
