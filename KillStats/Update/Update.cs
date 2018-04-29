using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json.Linq;
using SteamWebAPI;
using System.IO;

namespace KillStats
{
    class Update
    {
        static public bool Check()
        {
            JObject webVersion = JObject.Parse(Json.GET("https://raw.githubusercontent.com/TechnicPlay/KillStats/Bin/update/version.json"));
            JObject localVersion = JObject.Parse(System.IO.File.ReadAllText(Application.StartupPath + @"\update\version.json"));
            bool isUptodate = webVersion["version"].ToString() == localVersion["version"].ToString();
            return isUptodate;
        }

        static public async void Download(DownloadProgressChangedEventHandler DownloadProgressChanged, System.ComponentModel.AsyncCompletedEventHandler DownloadCompleted)
        {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgressChanged;
            client.DownloadFileCompleted += DownloadCompleted;

            await client.DownloadFileTaskAsync(new System.Uri(@"https://github.com/TechnicPlay/KillStats/raw/Update/KillStatsUpdater.exe"),
            Application.StartupPath + @"\update\KillStatsUpdater.exe");

            await client.DownloadFileTaskAsync(new System.Uri(@"https://github.com/TechnicPlay/KillStats/archive/Bin.zip"),
            Application.StartupPath + @"\update\KillStats.zip");
            
            string updateLog = Json.GET("https://raw.githubusercontent.com/TechnicPlay/KillStats/Update/UpdateLog.log");
            File.WriteAllText(Application.StartupPath + @"\update\UpdateLog.log", updateLog);
        }
    }
}
