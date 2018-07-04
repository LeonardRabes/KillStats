using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace ProcessListener
{
    class Listener
    {
        private Thread mainListenerThread;
        private Process[] allProcesses;
        private Process targetProcess;
        private string targetProcessName;
        private bool active;
        private bool started;

        public Listener(string target)
        {
            active = true;
            started = false;
            targetProcessName = target;
            mainListenerThread = new Thread(new ThreadStart(Listen));
            mainListenerThread.Start();
        }

        public event EventHandler ProcessFound;
        protected virtual void OnProcessFound(Process target)
        {
            ProcessFound?.Invoke(target, EventArgs.Empty);
        }
        public event EventHandler ProcessClosed;
        protected virtual void OnProcessClosed(Process target)
        {
            ProcessClosed?.Invoke(null, EventArgs.Empty);
        }

        public void Listen()
        {
            while (active)
            {
                if (!started)
                {
                    allProcesses = Process.GetProcesses();
                    GC.Collect();

                    foreach (var process in allProcesses)
                    {
                        if (process.ProcessName == targetProcessName && !started)
                        {
                            OnProcessFound(process);
                            targetProcess = process;
                            targetProcess.Exited += OnExited;
                            targetProcess.EnableRaisingEvents = true;
                            started = true;
                        }
                    }
                }
                Thread.Sleep(5000);
            }

            void OnExited(object sender, EventArgs e)
            {
                OnProcessClosed(sender as Process);
                started = false;
            }
        }

        public void Stop()
        {
            mainListenerThread.Abort();
        }

        public void Suspend()
        {
            mainListenerThread.Suspend();
        }

        public void Resume()
        {
            mainListenerThread.Resume();
        }
    }
}
