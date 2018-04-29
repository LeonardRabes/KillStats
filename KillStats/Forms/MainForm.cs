using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using SteamWebAPI;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace KillStats
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponent();
        }

        public string SteamID64;
        public string ProfileURL;
        public string APIkey;
        public string ActivePreset;
        public List<string> WeaponIDs = new List<string>();
        public List<byte> WeaponParts = new List<byte>();

        Stats TrackStats;

        public ItemViewList ItemViewList;

        List<string[]> ItemProperties;
        string ItemPropertiesJson;
        List<string[]> AllItems;
        string[] PlayerSummary;

        SettingsForm Settings;

        private void MainForm_Load(object sender, EventArgs e)
        {
            int[] test = new int[1];
            //int x = test[10];
            //Load settings.json and apply values
            string config = File.ReadAllText(Application.StartupPath + @"\config\settings.json");
            JObject APIsettings = (JObject)JObject.Parse(config)["APIsettings"];
            JArray weaponIDsJson = (JArray)APIsettings["weaponIDs"];

            SteamID64 = (string)APIsettings["steamID64"];
            ProfileURL = (string)APIsettings["profileURL"];
            APIkey = (string)APIsettings["APIkey"];
            ItemViewList = new ItemViewList(this);
            ItemViewList.ItemViewRemoved += RemoveItem;
            ItemViewList.GetItemColor += GetItemColor;
            Settings = new SettingsForm(this);

            //Get all files located in preset folder
            foreach (string filePath in Directory.GetFiles(Application.StartupPath + @"\config\presets"))
            {
                string fileName = Path.GetFileName(filePath);
                //check if file extention equals json
                if (Path.GetExtension(filePath) == ".json")
                {
                    fileName = fileName.Replace(".json", string.Empty);
                    WeaponPresets_presetList.Items.Add(new PresetItem(fileName, filePath));
                }
            }

            //On first startup, prevent errors
            if (SteamID64 == "" || APIkey == "" || ProfileURL == "")
            {
                return;
            }

            //Thread: download json files from steam api
            Thread LoadSteamData = new Thread(new ThreadStart(loadSteamData));
            LoadSteamData.Start();

            void loadSteamData()
            {
                try
                {
                    AllItems = Inventory.GetAllItems(SteamID64, "strange");
                    PlayerSummary = Client.PlayerSummary(SteamID64, APIkey);
                    ItemPropertiesJson = Json.GET(String.Format("http://steamcommunity.com/inventory/{0}/440/2?count=5000", SteamID64));

                    this.Invoke((MethodInvoker)delegate
                    {
                        applySteamData();
                    });
                }
                catch
                {

                    try
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Text = "KillStats [No Connection]";
                        });

                        DialogResult dial = MessageBox.Show(Json.GET(String.Format("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?steamids={0}&key={1}", SteamID64, APIkey)), "Network Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                        if (dial == DialogResult.Cancel)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.Close();
                            });
                            Thread.CurrentThread.Abort();
                        }

                        Thread.Sleep(5000);
                        loadSteamData();
                    }
                    catch (Exception)
                    {
                        Thread.CurrentThread.Abort();
                    }
                }
            }

            void applySteamData()
            {
                List<string> allWeaponIDs = new List<string>();

                //get every item id from inventory
                foreach (string[] itemInfo in AllItems)
                {
                    allWeaponIDs.Add(itemInfo[itemInfo.Length - 1]);
                }

                TrackStats = new Stats(this, allWeaponIDs.ToArray(), APIkey, SteamID64, 30000);
                TrackStats.StatsRefreshed += Stats_Refreshed;

                Settings.AllItems = AllItems;

                //apply player information: 0:Name; 1:ProfPicture; 2:Game; 3:ServerIP; 4:CountryCode
                ProfileName_label.Text = PlayerSummary[0];
                ProfilePicture_pictureBox.ImageLocation = PlayerSummary[1];


                WeaponPresets_presetList.SelectItem(WeaponPresets_presetList.Items[0]);
                OpenStats_button.Enabled = true;

                checkData();
            }

            void checkData()
            {
                //check if update is available
                if (!KillStats.Update.Check())
                {
                    UpdateForm update = new UpdateForm();
                    update.Show();
                }
                //check if update log is available
                else if (File.Exists(Application.StartupPath + @"\update\UpdateLog.log"))
                {
                    string path = Application.StartupPath + @"\update\UpdateLog.log";
                    UpdateLogForm log = new UpdateLogForm(File.ReadAllText(path), path);
                    log.Show();
                }
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (SteamID64 == "" || APIkey == "" || ProfileURL == "")
            {
                Settings.Show();
                Settings_button.Enabled = false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            Thread saveThread = new Thread(new ThreadStart(save));
            saveThread.Start();

            void save()
            {

                int iterations = -1;
                string[] weaponIDs = new string[0];
                uint[][][] points = new uint[0][][];
                float[][][][] stats = new float[0][][][];
                bool created = true;
                try
                {
                    iterations = TrackStats.Iterations;
                    weaponIDs = TrackStats.WeaponIDs;
                    points = TrackStats.ItemPoints.ToArray();
                    stats = TrackStats.ItemStats.ToArray();
                    TrackStats.StopTracking();
                }
                catch
                {
                    created = false;
                }

                if (created)
                {
                    if (TrackStats.Iterations > -1)
                    {
                        JValue date = new JValue(DateTime.Now);
                        JArray wpIDsJArray = JArray.FromObject(weaponIDs);
                        JArray ptJArray = JArray.FromObject(points);
                        JArray stJArray = JArray.FromObject(stats);

                        JObject currentSave = new JObject();
                        currentSave.Add("Date", date);
                        currentSave.Add("Iterations", iterations);
                        currentSave.Add("WeaponIDs", wpIDsJArray);
                        currentSave.Add("Points", ptJArray);
                        currentSave.Add("Stats", stJArray);

                        string saveStr = string.Empty;
                        JArray saveJArray = new JArray();

                        if (!Directory.Exists(@"saves"))
                        {
                            Directory.CreateDirectory(@"saves");
                        }

                        string filepath = String.Format(@"saves\saves{0}.kss", SteamID64);
                        if (File.Exists(filepath))
                        {
                            FileInfo file = new FileInfo(filepath);
                            if (file.Length > 0)
                            {
                                saveStr = Compress.UnZip(File.ReadAllText(filepath));
                                saveJArray = JArray.Parse(saveStr);
                            }
                        }
                        else
                        {
                            File.Create(String.Format(@"saves\saves{0}.kss", SteamID64)).Close();
                        }

                        saveJArray.Add(currentSave);
                        //File.WriteAllText(Application.StartupPath + String.Format(@"\saves\savesUncompressed{0}.kss", SteamID64), saveJArray.ToString());
                        File.WriteAllText(Application.StartupPath + String.Format(@"\saves\saves{0}.kss", SteamID64), Compress.Zip(saveJArray.ToString()));
                    }
                }
            }
        }

        private void MainForm_Changed(object sender, EventArgs e)
        {
            if (Settings != null)
            {
                Settings.Location = new Point(this.Location.X + this.Width - 15, this.Location.Y);
                Settings.Height = this.Height;
            }
        }

        private void MainForm_MouseEnter(object sender, EventArgs e)
        {
            WeaponPresets_presetList.PresetPanel.Visible = false;
        }

        private void Stats_Refreshed(object sender, EventArgs e)
        {
            Stats trackStats = sender as Stats;
            ApplyStats(trackStats.FilterPoints(WeaponIDs.ToArray()), trackStats.FilterStats(WeaponIDs.ToArray()));
        }

        private void WeaponPresets_presetList_Added(object targetItem, EventArgs e)
        {
            //create file in preset folder 
            PresetItem item = targetItem as PresetItem;
            string itemPath = Application.StartupPath + @"\config\presets\" + item.Name + ".json";
            File.Create(itemPath).Close();
            File.WriteAllText(itemPath, "{\"weaponIDs\": []}");
        }

        private void WeaponPresets_presetList_Selected(object targetItem, EventArgs e)
        {
            Thread reload_thread = new Thread(new ThreadStart(reload));
            reload_thread.Start();

            void reload()
            {
                PresetItem presetItem = targetItem as PresetItem;
                string presetString = File.ReadAllText(presetItem.FilePath);
                JArray presetJArr = (JArray)JObject.Parse(presetString)["weaponIDs"];

                ActivePreset = presetItem.FilePath;

                //reset WeaponIDs and WeaponParts
                WeaponIDs = new List<string>();
                WeaponParts = new List<byte>();

                //parse WeaponIDs and WeaponParts from preset file
                foreach (JObject jobj in presetJArr)
                {
                    WeaponIDs.Add(jobj["weaponID"].ToString());
                    WeaponParts.Add(Convert.ToByte(jobj["weaponPart"]));
                }
                ItemProperties = Inventory.ParseItemProperties(ItemPropertiesJson, WeaponIDs, WeaponParts);

                this.Invoke((MethodInvoker)delegate
                {
                    int height = Height;
                    //remove all existing ItemViews
                    foreach (ItemView view in ItemViewList.ItemViews)
                    {
                        view.Dispose();
                        height -= 80;

                    }
                    ItemViewList.ItemViews = new List<ItemView>();

                    //remove all series except AverageSeries
                    Series averageSerie = KillStats_chart.Series["AverageKills"];
                    averageSerie.Points.Clear();
                    KillStats_chart.Series.Clear();
                    KillStats_chart.Series.Add(averageSerie);

                    //create new views
                    if (WeaponIDs.Count != 0)
                    {
                        this.Height = height + ItemViewList.CreateViews(ItemProperties, new Point(25, 25), 75, 400);
                    }
                    else
                    {
                        this.Height = height;
                    }

                    //create new series
                    foreach (ItemView itemView in ItemViewList.ItemViews)
                    {
                        RedrawAverageSeries(itemView);
                    }
                    try
                    {
                        RedrawTotalAverageSeries();
                        TrackStats.RefreshStats();
                    }
                    catch { }
                });


            }

        }

        private void WeaponPresets_presetList_NameEdited(object oldName, object oldPath, object targetItem, EventArgs e)
        {
            //change name of a preset file
            PresetItem item = targetItem as PresetItem;
            string dir = Path.GetDirectoryName(oldPath.ToString());
            string newPath = dir + @"\" + item.Name + ".json";
            item.FilePath = newPath;
            File.Move(oldPath.ToString(), newPath);
            ActivePreset = newPath;
        }

        private void WeaponPresets_presetList_Removed(object targetItem, EventArgs e)
        {
            //delete a preset file
            PresetItem item = targetItem as PresetItem;
            File.Delete(item.FilePath);
        }

        private void Settings_button_Click(object sender, EventArgs e)
        {
            //show setting form
            Settings.Show();
            Settings.Settings_tabControl.SelectTab(0);
        }

        private void AddItem_itemView_Click(object sender, EventArgs e)
        {
            Settings.Show();
            Settings.Settings_tabControl.SelectTab(1);
        }

        private void OpenStats_button_Click(object sender, EventArgs e)
        {
            StatsForm statsForm = new StatsForm(AllItems.ToArray(), SteamID64);
            statsForm.Show();
        }

        private void ApplyStats(uint[][] itemPoints, float[][][][] itemStats)
        {
            //check if ItemViews are existing, ItemPoints are existing and ItemStats are existing
            if (ItemViewList.ItemViews != null && itemPoints.Length != 0 && itemStats.Length != 0)
            {
                float totalAverage = 0;
                //apply stats for every single ItemView
                foreach (ItemView itemView in ItemViewList.ItemViews)
                {
                    int index = ItemViewList.ItemViews.IndexOf(itemView);
                    //parse the kills done in the last hour and parse current average kills
                    float lasthour = itemStats[index][itemStats[index].Length - 1][itemView.WeaponPart][0];
                    float average = itemStats[index][itemStats[index].Length - 1][itemView.WeaponPart][1];

                    totalAverage += average;

                    this.Text = string.Format("KillStats [{0}]", TrackStats.Iterations);
                    itemView.ItemNameLabel.Text = string.Format("{0}: {1}", itemView.ItemName, itemPoints[index][itemView.WeaponPart]);
                    itemView.ItemTotalLabel.Text = "Total last hour: " + lasthour.ToString("0");
                    itemView.ItemAverageLabel.Text = "Points Avg: " + average.ToString("0.00");

                    //Check if series already exists
                    bool isAlreadyExisting = false;
                    Series weaponAverage_series;
                    foreach (Series serie in KillStats_chart.Series)
                    {
                        if (serie.Name.Contains(itemView.ItemName + itemView.WeaponPart))
                            isAlreadyExisting = true;
                    }

                    if (!isAlreadyExisting)
                    {
                        weaponAverage_series = new Series
                        {
                            BorderWidth = 5,
                            ChartArea = "ChartArea1",
                            ChartType = SeriesChartType.Spline,
                            Name = itemView.ItemName + itemView.WeaponPart,
                            Color = itemView.ItemImageBorder.BackColor
                        };
                        itemView.ItemSeries = weaponAverage_series;
                        KillStats_chart.Series.Add(weaponAverage_series);
                        isAlreadyExisting = true;
                    }

                    //Draw average into chart, if average isn't 0
                    if (isAlreadyExisting && average != 0)
                    {
                        KillStats_chart.Series[itemView.ItemName + itemView.WeaponPart].Color = itemView.ItemImageBorder.BackColor;
                        //check if point exists already
                        bool pointExists = false;
                        foreach (DataPoint point in KillStats_chart.Series[itemView.ItemName + itemView.WeaponPart].Points)
                        {
                            pointExists = point.XValue == (double)itemStats[index].Length / 2;
                        }

                        if (!pointExists)
                            KillStats_chart.Series[itemView.ItemName + itemView.WeaponPart].Points.AddXY((double)itemStats[index].Length / 2, average);
                    }
                }

                if (totalAverage != 0)
                {
                    bool pointExists = false;
                    foreach (DataPoint point in KillStats_chart.Series["AverageKills"].Points)
                    {
                        pointExists = point.XValue == (double)itemStats[0].Length / 2;
                    }

                    if (!pointExists)
                        KillStats_chart.Series["AverageKills"].Points.AddXY((double)itemStats[0].Length / 2, totalAverage);
                }
            }

            else if (ItemViewList.ItemViews != null && itemPoints.Length != 0 && itemStats.Length < 1)
            {
                foreach (ItemView itemView in ItemViewList.ItemViews)
                {
                    int index = ItemViewList.ItemViews.IndexOf(itemView);
                    itemView.ItemNameLabel.Text = string.Format("{0}: {1}", itemView.ItemName, itemPoints[index][itemView.WeaponPart]);
                }
            }
        }

        private void RemoveItem(ItemView targetView, EventArgs e)
        {
            Height -= 80;
            WeaponParts.RemoveAt(WeaponIDs.IndexOf(targetView.WeaponID));
            WeaponIDs.Remove(targetView.WeaponID);

            //Remove series of removed view
            foreach (System.Windows.Forms.DataVisualization.Charting.Series serie in KillStats_chart.Series)
            {
                if (serie.Name.Contains(targetView.ItemName))
                {
                    KillStats_chart.Series.Remove(KillStats_chart.Series[targetView.ItemName + targetView.WeaponPart]);
                    break;
                }
            }

            //Redraw main series
            RedrawTotalAverageSeries();

            //Save changes in config
            string config = System.IO.File.ReadAllText(ActivePreset);
            JArray wpIDsJArr = (JArray)JObject.Parse(config)["weaponIDs"];
            foreach (JObject jobj in wpIDsJArr)
            {
                if (jobj["weaponID"].ToString() == targetView.WeaponID && (byte)jobj["weaponPart"] == targetView.WeaponPart)
                {
                    wpIDsJArr.Remove(jobj);
                    break;
                }
            }
            JObject configJObj = new JObject();
            configJObj.Add("weaponIDs", wpIDsJArr);

            System.IO.File.WriteAllText(ActivePreset, configJObj.ToString());
        }

        public void AddItem(string weaponID, byte weaponPart)
        {
            List<string[]> itemProps;
            Thread LoadSteamData = new Thread(new ThreadStart(loadItem));
            LoadSteamData.Start();

            void loadItem()
            {
                itemProps = Inventory.ParseItemProperties(ItemPropertiesJson, new List<string>(new string[] { weaponID }), new List<byte>(new byte[] { weaponPart }));

                this.Invoke((MethodInvoker)delegate
                {
                    applyItem();
                });
            }

            void applyItem()
            {
                if (ItemViewList.ItemViews.Count < 6)
                {
                    ItemView newView = new ItemView(itemProps[0][0], itemProps[0][1], itemProps[0][2], itemProps[0][3], new Point(25, 25), 400, 75, ItemViewList.ItemViews.Count);
                    newView.WeaponID = weaponID;
                    newView.WeaponPart = weaponPart;
                    newView.BackColor = Color.FromArgb(22, 32, 45);

                    ItemViewList.AddView(newView);
                    TrackStats.RefreshStats();

                    RedrawAverageSeries(newView);
                    RedrawTotalAverageSeries();
                }
            }
        }

        private void RedrawAverageSeries(ItemView newView)
        {
            Series weaponAverage_series = null;
            try
            {
                //if series is already existing
                int index = KillStats_chart.Series.IndexOf(newView.ItemName + newView.WeaponPart);
                KillStats_chart.Series.RemoveAt(index);
            }
            catch
            {

            }

            weaponAverage_series = new Series
            {
                BorderWidth = 5,
                ChartArea = "ChartArea1",
                ChartType = SeriesChartType.Spline,
                Name = newView.ItemName + newView.WeaponPart,
                Color = newView.ItemImageBorder.BackColor
            };

            //clear all Points to have a blanc series
            weaponAverage_series.Points.Clear();

            newView.ItemSeries = weaponAverage_series;

            //get all average values for newView
            float[][][][] itemStats = TrackStats.FilterStats(new string[] { newView.WeaponID });
            List<float> average = new List<float>();

            if (itemStats.Length > 0)
            {
                //parse all data into average 
                for (int i = 0; i < itemStats[0].Length; i++)
                {
                    float avg = itemStats[0][i][newView.WeaponPart][1];
                    average.Add(avg);
                }

                //apply data from average
                for (int i = 0; i < average.Count; i++)
                {
                    if (average[i] > 0)
                        weaponAverage_series.Points.AddXY((double)(i + 1) / 2, average[i]);
                }
            }

            KillStats_chart.Series.Add(weaponAverage_series);
        }

        private void RedrawTotalAverageSeries()
        {
            KillStats_chart.Series["AverageKills"].Points.Clear();
            float[][][][] itemStats = TrackStats.FilterStats(WeaponIDs.ToArray());
            List<float> totalAverage = new List<float>();

            if (itemStats.Length != 0)
            {
                for (int i = 0; i < itemStats[0].Length; i++)
                {
                    float average = 0;
                    foreach (ItemView itemView in ItemViewList.ItemViews)
                    {
                        int index = ItemViewList.ItemViews.IndexOf(itemView);
                        average += itemStats[index][i][itemView.WeaponPart][1];
                    }
                    totalAverage.Add(average);
                }

                for (int i = 0; i < totalAverage.Count; i++)
                {
                    if (WeaponIDs.Count > 1 && totalAverage[i] > 0)
                        KillStats_chart.Series["AverageKills"].Points.AddXY((double)(i + 1) / 2, totalAverage[i]);
                }
            }

        }

        private Color GetItemColor(int count)
        {
            string colorsJson = System.IO.File.ReadAllText(Application.StartupPath + @"\config\colors_normal.json");
            JArray colorsArray = (JArray)JObject.Parse(colorsJson)["colors"];
            string color;
            try
            {
                color = colorsArray[count]["hex"].ToString();
            }
            catch
            {
                return Color.White;
            }

            ColorConverter colorConverter = new ColorConverter();
            return (Color)colorConverter.ConvertFromString(color);
        }


    }
}
