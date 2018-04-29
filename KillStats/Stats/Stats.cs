using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SteamWebAPI;
using System.Windows.Forms;


namespace KillStats
{
    class Stats
    {
        private Thread trackStatsThread;
        private object Sender;
        private uint[][] CurrentItemPoints;

        public int Sleep { get; set; }
        public int Iterations { get; set; }
        public bool ClientIsOnServer { get; set; }
        public string[] WeaponIDs { get; }
        public string APIkey { get; set; }
        public string SteamID { get; set; }
        public List<float[][][]> ItemStats { get; }
        public List<uint[][]> ItemPoints { get; }

        public Stats(object sender, string[] weaponIDs, string apiKey, string steamID, int sleep)
        {
            trackStatsThread = new Thread(TrackStats);
            Sender = sender;
            Iterations = -1;
            ItemStats = new List<float[][][]>();
            ItemPoints = new List<uint[][]>();
            trackStatsThread.Start();
            WeaponIDs = weaponIDs;
            APIkey = apiKey;
            SteamID = steamID;
            Sleep = sleep;
        }

        public delegate void StatsRefreshedEventHandler(object sender, EventArgs args);
        public event StatsRefreshedEventHandler StatsRefreshed;

        protected virtual void OnStatsRefreshed()
        {
            StatsRefreshed?.Invoke(this, EventArgs.Empty);
        }

        public uint[][] FilterPoints(string[] weaponIDs)
        {
            List<uint[]> items = new List<uint[]>();
            if (weaponIDs.Length != 0)
                foreach (string weaponID in weaponIDs)
                {
                    int weaponIndex = new List<string>(WeaponIDs).IndexOf(weaponID);

                    if (CurrentItemPoints != null)
                    {
                        if (weaponIndex >= 0 && CurrentItemPoints.Length > 0)
                        {
                            items.Add(CurrentItemPoints[weaponIndex]);
                        }
                    }

                    //items.Add(ItemPoints[ItemPoints.Count - 1][weaponIndex]);

                }

            return items.ToArray();
        }

        public float[][][][] FilterStats(string[] weaponIDs)
        {
            List<float[][][]> filteredItemStats = new List<float[][][]>();

            foreach (string weaponID in weaponIDs)
            {
                List<float[][]> filteredItem = new List<float[][]>();
                int weaponIndex = new List<string>(WeaponIDs).IndexOf(weaponID);

                for (int i = 0; i < ItemStats.Count; i++)
                {
                    if (weaponIndex >= 0 && ItemStats.Count > 0)
                        filteredItem.Add(ItemStats[i][weaponIndex]);
                }
                if (filteredItem.Count > 0)
                    filteredItemStats.Add(filteredItem.ToArray());
            }

            return filteredItemStats.ToArray();
        }

        public void RefreshStats()
        {
            OnStatsRefreshed();
        }

        public void StopTracking()
        {
            trackStatsThread.Abort();
        }

        private void TrackStats()
        {
            for (int iterations = 0; true; iterations++)
            {
                ClientIsOnServer = Client.IsOnServer(SteamID, APIkey);
                List<uint[]> itemPointsArray = Inventory.GetItemStats(SteamID, APIkey, new List<string>(WeaponIDs));
                List<float[][]> allItemsStats = new List<float[][]>();

                if (itemPointsArray.Count > 0)
                {
                    if (itemPointsArray[0][0] == 200 && ClientIsOnServer)
                    {
                        itemPointsArray.RemoveAt(0);
                        ItemPoints.Add(itemPointsArray.ToArray());
                        CurrentItemPoints = itemPointsArray.ToArray();
                        this.Iterations = iterations;

                        foreach (uint[] itemPoints in itemPointsArray)
                        {
                            List<float[]> item = new List<float[]>();
                            foreach (uint itemPoint in itemPoints)
                            {
                                int itemArrayIndex = itemPointsArray.IndexOf(itemPoints);
                                int itemIndex = (new List<uint>(itemPoints)).IndexOf(itemPoint);

                                uint itemHourAgo = ItemPoints[Math.Max(0, iterations - 120)][itemArrayIndex][itemIndex];
                                uint itemStartUp = ItemPoints[0][itemArrayIndex][itemIndex];

                                float lasthour = itemPoint - itemStartUp;
                                float average = ((itemPoint - itemStartUp) / (float)Math.Max(1, iterations)) * 120.0f;
                                //
                                //
                                /*Random rnd = new Random();
                                lasthour = (float)rnd.NextDouble();
                                average = (float)rnd.NextDouble();
                                Thread.Sleep(10);*/
                                //
                                //

                                item.Add(new float[] { lasthour, average });
                            }
                            allItemsStats.Add(item.ToArray());
                        }

                        ItemStats.Add(allItemsStats.ToArray());

                        (Sender as Form).Invoke((MethodInvoker)delegate
                        {
                            OnStatsRefreshed();
                        });

                    }

                    else if (itemPointsArray[0][0] == 200 && ItemStats.Count < 1 && !ClientIsOnServer)
                    {
                        itemPointsArray.RemoveAt(0);
                        CurrentItemPoints = itemPointsArray.ToArray();

                        (Sender as Form).Invoke((MethodInvoker)delegate
                        {
                            (Sender as Form).Text = "KillStats [Paused]";
                            OnStatsRefreshed();
                        });

                        iterations -= 1;
                    }

                    else if (itemPointsArray[0][0] == 200 && ItemStats.Count != 0 && !ClientIsOnServer)
                    {
                        (Sender as Form).Invoke((MethodInvoker)delegate
                        {
                            (Sender as Form).Text = "KillStats [Paused]";
                        });

                        iterations -= 1;
                    }

                    else if (itemPointsArray[0][0] == 500 && ItemStats.Count != 0 && ClientIsOnServer)
                    {
                        itemPointsArray.RemoveAt(0);
                        ItemPoints.Add(ItemPoints[ItemPoints.Count - 1]);
                        ItemStats.Add(ItemStats[ItemStats.Count - 1]);

                        (Sender as Form).Invoke((MethodInvoker)delegate
                        {
                            (Sender as Form).Text = string.Format("KillStats [{0}]", iterations);
                        });
                    }

                    else if (itemPointsArray[0][0] == 500 && !ClientIsOnServer)
                    {
                        (Sender as Form).Invoke((MethodInvoker)delegate
                        {
                            (Sender as Form).Text = "KillStats [No Response]";
                        });

                        iterations -= 1;
                    }
                }

                this.Iterations = iterations;
                Thread.Sleep(Sleep);
            }
        }
    }
}
