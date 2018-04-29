using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillStats
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(ExceptionHandling);

            Application.Run(new MainForm());
        }

        private static void ExceptionHandling(object sender, ThreadExceptionEventArgs t)
        {
            MessageBox.Show("Exception: \n" + t.Exception.ToString());
        }
    }
}
