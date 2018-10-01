using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CRTerm
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

            if (true)
            {
                Application.Run(new MainWindow());
            }
            else
            {
                TextConsole t = new TextConsole();
                t.Setup();
                t.Loop();
            }
        }

    }
}
