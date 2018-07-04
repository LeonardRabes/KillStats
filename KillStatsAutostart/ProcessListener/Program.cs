using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ProcessListener
{
    class Program
    {
        private static Listener ProcessListener;
        private static Process[] StartedProcesses;
        private static string[] Config;

        public static void Main(string[] args)
        {
            Config = File.ReadAllLines("ProcessListener.cfg");
            StartedProcesses = new Process[Config.Length - 1];

            ProcessListener = new Listener(Config[0]);
            ProcessListener.ProcessFound += OnProcessFound;
            ProcessListener.ProcessClosed += OnProcessClosed;
        }

        private static void OnProcessFound(object process, EventArgs e)
        {
            for (int i = 0; i < StartedProcesses.Length; i++)
            {
                StartedProcesses[i] = Process.Start(Config[i + 1]);
            }
        }

        private static void OnProcessClosed(object process, EventArgs e)
        {
            foreach (var p in StartedProcesses)
            {
                if(!p.HasExited)
                {
                    p.CloseMainWindow();
                }
            }

        }

    }
}
