using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using SteamWebAPI;

namespace KillStats
{
    public partial class KsMainForm : Form
    {
        public KsMainForm()
        {
            InitializeComponent();
            ItemView.OnRemove = new Action<ItemView>(DeleteWeapon);
            ItemView.GetItemColor = new Func<int, Color>(GetItemColor);
            this.ClientNotOnServer_panel.BackColor = Color.FromArgb(255, 27, 40, 56);
            this.KillStats_chart.ChartAreas[0].AxisX.Title = "Time in Minutes";
            this.KillStats_chart.ChartAreas[0].AxisX.TitleForeColor = Color.Gray;
            this.KillStats_chart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Gray;
            this.KillStats_chart.ChartAreas[0].AxisY.Title = "Average Points";
            this.KillStats_chart.ChartAreas[0].AxisY.TitleForeColor = Color.Gray;
            this.KillStats_chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Gray;
        }

        public string steamID64;
        public string profileURL;
        public string APIkey;
        public List<string> weaponIDs = new List<string>();
        public List<byte> weaponParts = new List<byte>();

        List<uint[][]> strangePointsCountList = new List<uint[][]>(); //All weapon strange counts for last 120 iterations

        List<uint[][]> allStrangePointsCountList = new List<uint[][]>(); //All weapon strange counts for every iteration
        List<float[][]> allStrangePartsAverageList = new List<float[][]>(); //All weapon strange average for every iteration

        List<uint[]> strangePointsCount = new List<uint[]>();
        List<uint[]> startupStrangePointsCount = new List<uint[]>();
        uint iterations = 0;

        List<string[]> itemProperties;
        List<ItemView> itemViews;

        private void KsMainForm_Load(object sender, EventArgs e)
        {
            //Load settings.json and apply values
            string config = System.IO.File.ReadAllText(Application.StartupPath + @"\config\settings.json");
            JObject APIsettings = (JObject)JObject.Parse(config)["APIsettings"];
            steamID64 = (string)APIsettings["steamID64"];
            profileURL = (string)APIsettings["profileURL"];
            APIkey = (string)APIsettings["APIkey"];
            JArray weaponIDsJson = (JArray)APIsettings["weaponIDs"];
            foreach (JObject jobj in weaponIDsJson)
            {
                weaponIDs.Add(jobj["weaponID"].ToString());
            }
            foreach (JObject jobj in weaponIDsJson)
            {
                weaponParts.Add(Convert.ToByte(jobj["weaponPart"]));
            }

            //On first startup, prevent error
            if (steamID64 == "" || APIkey == "" || profileURL == "")
            {
                return;
            }

            //Get item name from steam inventory
            if (weaponIDs.Count != 0)
            {
                itemProperties = Inventory.GetItemProperties(steamID64, weaponIDs, weaponParts);
                itemViews = ItemView.CreateDynamicViews(itemProperties, new Point(25, 25), 75, 400, this);
            }

            ApplyStats();
            refreshTimer.Enabled = true;

            //Get and apply player information: 0:Name; 1:ProfPicture; 2:Game; 3:ServerIP; 4:CountryCode
            string[] playerSummary = Client.PlayerSummary(steamID64, APIkey);
            ProfileName_label.Text = playerSummary[0];
            ProfilePicture_pictureBox.ImageLocation = playerSummary[1];
        }

        private void KsMainForm_Shown(object sender, EventArgs e)
        {
            if (steamID64 == "" || APIkey == "" || profileURL == "")
            {
                KsSettingsForm settings = new KsSettingsForm(this, true);
                settings.Show();
                settings.Activate();
                Settings_button.Enabled = false;
            }

            else if (weaponIDs.Count == 0)
            {
                KsSettingsForm settings = new KsSettingsForm(this, true);
                settings.Show();
                settings.Activate();
                Settings_button.Enabled = false;
                settings.Settings_tabControl.SelectTab(1);
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            ApplyStats();
        }

        private void Settings_button_Click(object sender, EventArgs e)
        {
            KsSettingsForm settings = new KsSettingsForm(this, false);
            settings.Show();
            Settings_button.Enabled = false;
        }

        private void ApplyStats()
        {
            List<uint[]> itemKills = Inventory.GetItemStats(steamID64, APIkey, weaponIDs); //Array:[status: 200(success) || 500(fail), weapon1:[part1: val, part2:val, ...], ...]
            List<float[]> averageAllWeapons = new List<float[]>(); //Array:[weapon1:[average1, average2, ...], weapon2:[average1, average2, ...]]

            bool clientIsOnServer = true;//Client.IsOnServer(steamID64, APIkey);
            int weaponCount = 0;
            int strangePart = 0;

            ClientNotOnServer_panel.Visible = !clientIsOnServer;
            strangePointsCount = new List<uint[]>();

            if (clientIsOnServer)
            {
                iterations++;
            }

            if (itemViews != null)
            {
                foreach (ItemView itemView in itemViews)
                {
                    #region Player is on server and json was received

                    if (itemKills[0][0] == 200 && clientIsOnServer)
                    {
                        strangePointsCount.Add(itemKills[weaponCount + 1]);
                        strangePart = itemView.WeaponPart;
                        itemView.StrangePoints = itemKills[weaponCount + 1][strangePart];
                        itemView.ItemNameLabel.Text = itemView.ItemName + ": " + strangePointsCount[weaponCount][strangePart];
                        this.Text = "KillStats [" + iterations + "]";

                        //Save first retrieved kill value
                        if (strangePointsCountList.Count == 0)
                        {
                            startupStrangePointsCount = strangePointsCount;
                        }

                        //Limit saved kill values to 1 hour
                        if (strangePointsCountList.Count > 120)
                        {
                            strangePointsCountList.RemoveAt(0);
                        }

                        //Calculate kills done during last hour and kill average since start of application
                        if (strangePointsCountList.Count != 0)
                        {
                            List<float> averageCurrentWeapon = new List<float>();
                            float lasthour = strangePointsCount[weaponCount][strangePart] - strangePointsCountList[0][weaponCount][strangePart];
                            
                            for(int i = 0; i < strangePointsCount[weaponCount].Length; i++)
                            {
                                float average = (strangePointsCount[weaponCount][i] - startupStrangePointsCount[weaponCount][i]) / ((float)iterations) * 120;
                                averageCurrentWeapon.Add(average);
                            }

                            itemView.TotalPoints = Convert.ToInt32(lasthour);
                            itemView.AveragePoints = averageCurrentWeapon[strangePart];
                            itemView.ItemTotalLabel.Text = "Total last hour: " + lasthour.ToString();
                            itemView.ItemAverageLabel.Text = "Points Avg: " + averageCurrentWeapon[strangePart].ToString();

                            averageAllWeapons.Add(averageCurrentWeapon.ToArray());
                        }

                        //Check if series already exists
                        bool isAlreadyExisting = false;
                        System.Windows.Forms.DataVisualization.Charting.Series weaponAverage_series;
                        foreach (System.Windows.Forms.DataVisualization.Charting.Series serie in KillStats_chart.Series)
                        {
                            if (serie.Name.Contains(itemView.ItemName + itemView.WeaponPart))
                                isAlreadyExisting = true;
                        }
                        if (!isAlreadyExisting)
                        {
                            weaponAverage_series = new System.Windows.Forms.DataVisualization.Charting.Series
                            {
                                BorderWidth = 5,
                                ChartArea = "ChartArea1",
                                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline,
                                Name = itemView.ItemName + itemView.WeaponPart,
                                Color = itemView.ItemBorderColor
                            };

                            KillStats_chart.Series.Add(weaponAverage_series);
                        }

                        //Draw average into chart, if average isn't 0
                        if (averageAllWeapons.Count != 0 && averageAllWeapons[weaponCount][strangePart] != 0)
                        {
                            KillStats_chart.Series[itemView.ItemName + itemView.WeaponPart].Color = itemView.ItemBorderColor;
                            KillStats_chart.Series[itemView.ItemName + itemView.WeaponPart].Points.AddXY((double)iterations / 2, averageAllWeapons[weaponCount][strangePart]);
                        }
                    }

                    #endregion

                    #region Player is on server and json wasn't received

                    else if (strangePointsCountList.Count != 0 && itemKills[0][0] == 500 && clientIsOnServer)
                    {
                        strangePointsCount.Add(new uint[] { itemView.StrangePoints });
                        this.Text = "KillStats [" + iterations + "] Error";
                        if (strangePointsCountList.Count > 120)
                        {
                            strangePointsCountList.RemoveAt(0);
                        }
                    }

                    #endregion

                    #region Player isn't on server

                    else if (!clientIsOnServer)
                    {
                    }

                    #endregion

                    weaponCount++;
                }
            }

            #region Save current kill value and draw average graph

            if (clientIsOnServer == true)
            {
                //Save current kill and average value
                if (strangePointsCountList.Count != 0 && itemKills[0][0] == 500 || itemKills[0][0] == 200)
                {
                    strangePointsCountList.Add(strangePointsCount.ToArray());
                    allStrangePointsCountList.Add(strangePointsCount.ToArray());
                    if (averageAllWeapons.Count != 0)
                        allStrangePartsAverageList.Add(averageAllWeapons.ToArray());
                }


                float totalAverage = 0;
                if(itemViews != null)
                    foreach (ItemView itemView in itemViews)
                    {
                        totalAverage += itemView.AveragePoints;
                    }

                if (itemKills[0][0] == 200 && totalAverage != 0)
                {
                    KillStats_chart.Series["AverageKills"].Points.AddXY((double)iterations / 2, totalAverage);
                }
            }

            #endregion
        }

        private void DeleteWeapon(ItemView targetView)
        {
            Height -= 75;
            itemViews.Remove(targetView);
            weaponParts.RemoveAt(weaponIDs.IndexOf(targetView.WeaponID)); 
            weaponIDs.Remove(targetView.WeaponID);

            foreach (System.Windows.Forms.DataVisualization.Charting.Series serie in KillStats_chart.Series)
            {
                if (serie.Name.Contains(targetView.ItemName))
                {
                    KillStats_chart.Series.Remove(KillStats_chart.Series[targetView.ItemName + targetView.WeaponPart]);
                    break;
                }
            }

            string config = System.IO.File.ReadAllText(Application.StartupPath + @"\config\settings.json");
            string weaponIDJson = "{\"weaponID\":\"" + targetView.WeaponID + "\",\"weaponPart\":\""+ targetView.WeaponPart + "\"}";
            config = config.Replace("\n", string.Empty);
            config = config.Replace("\r", string.Empty);
            config = config.Replace(" ", string.Empty);
            config = config.Replace("   ", string.Empty);
            config = config.Replace(weaponIDJson, string.Empty);
            config = config.Replace(",,", ",");
            config = config.Replace("[,", "[");

            System.IO.File.WriteAllText(Application.StartupPath + @"\config\settings.json", JObject.Parse(config).ToString());
        }

        private Color GetItemColor(int count)
        {
            string colorsJson = System.IO.File.ReadAllText(Application.StartupPath + @"\config\colors_normal.json");
            JArray colorsArray = (JArray)JObject.Parse(colorsJson)["colors"];
            string color = colorsArray[count]["hex"].ToString();

            ColorConverter colorConverter = new ColorConverter();
            return (Color)colorConverter.ConvertFromString(color);
        }

        public void Restart() //Reset all values to default
        {
            strangePointsCountList = new List<uint[][]>();

            strangePointsCount = new List<uint[]>();
            startupStrangePointsCount = new List<uint[]>();
            iterations = 0;

            if (itemViews != null)
                foreach (ItemView itemView in itemViews)
                {
                    itemView.Dispose();
                    this.Height -= itemView.Height + 5;
                }

            itemProperties = Inventory.GetItemProperties(steamID64, weaponIDs, weaponParts);
            itemViews = ItemView.CreateDynamicViews(itemProperties, new Point(25, 25), 75, 400, this);

            string[] playerSummary = Client.PlayerSummary(steamID64, APIkey);
            ProfileName_label.Text = playerSummary[0];
            ProfilePicture_pictureBox.ImageLocation = playerSummary[1];

            foreach (var series in KillStats_chart.Series)
            {
                if(series != null)
                    series.Points.Clear();
            }

            ApplyStats();
        }


    }
}
