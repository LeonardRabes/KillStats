using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Newtonsoft.Json.Linq;
using SteamWebAPI;

namespace KillStats
{
    public partial class StatsForm : Form
    {
        private string[][] AllItems;

        public string FilePath;
        public string FileText;
        public string SteamID64;
        public List<int> Iterations;
        public List<DateTime> DateTimes;
        public List<string[]> WeaponIDs;
        public List<uint[][][]> ItemPoints;
        public List<float[][][][]> ItemStats;


        public StatsForm(string[][] allItems, string steamID64)
        {
            InitializeComponent();
            AllItems = allItems;
            SteamID64 = steamID64;
            this.KillStats_chart.ChartAreas[0].AxisX.Title = "Weapons";
            this.KillStats_chart.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Gray;
            this.KillStats_chart.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Gray;
            this.KillStats_chart.ChartAreas[0].AxisY.Title = "Points";
            this.KillStats_chart.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Gray;
            this.KillStats_chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Gray;

        }

        private void StatsForm_Load(object sender, EventArgs e)
        {
            try
            {
                FilePath = String.Format(@"saves\saves{0}.kss", SteamID64);
                if (Directory.Exists(@"saves"))
                {
                    if (File.Exists(FilePath))
                    {
                        FileText = Compress.UnZip(File.ReadAllText(FilePath));

                        JArray fileJArray = JArray.Parse(FileText);
                        DateTimes = new List<DateTime>();
                        Iterations = new List<int>();
                        WeaponIDs = new List<string[]>();
                        ItemPoints = new List<uint[][][]>();
                        ItemStats = new List<float[][][][]>();

                        foreach (JObject jobj in fileJArray)
                        {
                            DateTimes.Add((DateTime)jobj["Date"]);
                            Iterations.Add((int)jobj["Iterations"]);
                            WeaponIDs.Add(JArrayTo<string>((JArray)jobj["WeaponIDs"]));
                            ItemPoints.Add(JArrayTo<uint[][]>((JArray)jobj["Points"]));
                            ItemStats.Add(JArrayTo<float[][][]>((JArray)jobj["Stats"]));
                        }
                        FileTime_label.Text = "Statistics Duration: " + DateTimes[0].Date.ToString("dd/MM/yyyy") + " - " + DateTimes[DateTimes.Count - 1].Date.ToString("dd/MM/yyyy");

                        List<uint[]> itemKillsAllTime = new List<uint[]>();

                        foreach (string weaponID in WeaponIDs[WeaponIDs.Count - 1])
                        {
                            uint[][] itemPoints = GetWeaponPoints(weaponID);
                            List<uint> itemKills = new List<uint>();

                            for (int ind = 0; ind < itemPoints[0].Length; ind++)
                            {
                                itemKills.Add(itemPoints[itemPoints.Length - 1][ind] - itemPoints[0][ind]);
                            }

                            itemKillsAllTime.Add(itemKills.ToArray());
                        }

                        int index = 0;
                        foreach (uint[] valarr in itemKillsAllTime)
                        {
                            for (int i = 0; i < valarr.Length; i++)
                            {
                                int weaponIDIndex = itemKillsAllTime.IndexOf(valarr); ;
                                int modifierVal = 1;
                                string modifierStr = string.Empty;
                                Color modifierColor = Color.White;

                                if (editPart(weaponIDIndex).Contains("Allied Healing Done"))
                                {
                                    modifierVal = 100;
                                    modifierStr = " / 100";
                                    modifierColor = Color.Orange;
                                }

                                KillStats_chart.Series["StatsSeries"].Points.AddXY(index + 1, valarr[i] / modifierVal);
                                KillStats_chart.Series["StatsSeries"].Points[index].Tag = AllItems[weaponIDIndex][0] + " - " + editPart(weaponIDIndex) + ": " + valarr[i] + modifierStr;
                                KillStats_chart.Series["StatsSeries"].Points[index++].Color = modifierColor;

                                string editPart(int ind)
                                {
                                    string part = AllItems[ind][i + 1];
                                    if (part == string.Empty)
                                    {
                                        return AllItems[ind][i + 2].Substring(5, part.IndexOf(":") - (part.Length - 5));
                                    }

                                    else if (part.Contains("-"))
                                    {
                                        return part.Substring(part.IndexOf("-") + 2, part.IndexOf(":") - (part.IndexOf("-") + 2));
                                    }

                                    else
                                        return part.Substring(0, part.IndexOf(":")); ;
                                };
                            }
                        }
                    }
                    else
                    {
                        this.Close();
                        MessageBox.Show("No Statistics Found", "Statistics", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    this.Close();
                    MessageBox.Show("No Statistics Found", "Statistics", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
            

        }

        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();
        private void KillStats_chart_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            HitTestResult[] results = KillStats_chart.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    DataPoint prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                        int diff = (int)(KillStats_chart.ChartAreas["ChartArea1"].AxisX.MaximumAutoSize * KillStats_chart.Width / 100) / KillStats_chart.Series["StatsSeries"].Points.Count;
                        // check if the cursor is close to the point
                        if (Math.Abs(pos.X - pointXPixel) < diff - 2)
                        {
                            tooltip.Show((result.Object as DataPoint).Tag as string, this.KillStats_chart,
                                            pos.X, pos.Y - 15);
                        }
                    }
                }
            }
        }

        private uint[][] GetWeaponPoints(string weaponID)
        {
            List<uint[]> itemPoints = new List<uint[]>();

            for (int i = 0; i < WeaponIDs.Count; i++)
            {
                int index = new List<string>(WeaponIDs[i]).IndexOf(weaponID);
                if (index >= 0)
                {
                    foreach (uint[][] pt in ItemPoints[i])
                    {
                        itemPoints.Add(pt[index]);
                    }
                }
            }

            return itemPoints.ToArray();
        }

        private T[] JArrayTo<T>(JArray jarr)
        {
            List<T> TList = new List<T>();
            foreach (var jvar in jarr)
            {
                TList.Add(jvar.ToObject<T>());
            }
            return TList.ToArray();
        }


    }
}
