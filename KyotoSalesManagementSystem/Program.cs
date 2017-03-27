using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KyotoSalesManagementSystem.LoginUI;
using KyotoSalesManagementSystem.UI;

namespace KyotoSalesManagementSystem
{
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
            //Application.Run(new MainUI());
            //Application.Run(new frmLogin());
Application.Run(new Invoice());
        }
    }
}
