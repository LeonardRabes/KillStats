using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace KillStats
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        private void StartUpd_button_Click(object sender, EventArgs e)
        {
            KillStats.Update.Download(Update_DownloadProgressChanged, Update_DownloadCompleted);
            Status_label.Text = "Downloading Updater...";
        }

        private void CancelUpd_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Update_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Download_progressBar.Value = e.ProgressPercentage;
        }

        private void Update_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            TaskCompletionSource<object> tskComplSrc = e.UserState as TaskCompletionSource<object>;
            if(tskComplSrc.Task.AsyncState.ToString() == "https://github.com/TechnicPlay/KillStats/archive/Bin.zip")
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\update\KillStatsUpdater.exe");
                Application.Exit();
            }
            else
            {
                Status_label.Text = "Downloading Files...";
            }
        }
    }
}
