using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace SteamWebAPI
{
    static class Inventory
    {
        public static List<uint[]> GetItemStats(string steamID64, string APIkey, List<string> weaponIDs)
        {
            List<uint[]> weaponParts = new List<uint[]>();
            uint status = 200;

            try
            {
                string response = Json.GET(String.Format("http://api.steampowered.com/IEconItems_440/GetPlayerItems/v0001/?steamid={0}&key={1}", steamID64, APIkey));
                if (response.Contains("result") == true)
                {
                    weaponParts.Add(new uint[] { status });
                }
                else
                {
                    weaponParts.Add(new uint[] { status = 500 });
                }
                JArray items = (JArray)JObject.Parse(response)["result"]["items"];
                foreach (string weaponID in weaponIDs)
                {
                    foreach (JObject item in items)
                    {
                        if (((string)item["id"]).Contains(weaponID))
                        {
                            JArray attributes = (JArray)item["attributes"];
                            List<uint> weaponStats = new List<uint>();

                            foreach (JObject attribute in attributes)
                            {


                                if ((int)attribute["defindex"] == 214)
                                {
                                    weaponStats.Add(Convert.ToUInt32(attribute["value"]));
                                }

                                if ((int)attribute["defindex"] == 294)
                                {
                                    weaponStats.Add(Convert.ToUInt32(attribute["value"]));
                                }

                                if ((int)attribute["defindex"] == 379)
                                {
                                    weaponStats.Add(Convert.ToUInt32(attribute["value"]));
                                }

                                if ((int)attribute["defindex"] == 381)
                                {
                                    weaponStats.Add(Convert.ToUInt32(attribute["value"]));
                                }

                                if ((int)attribute["defindex"] == 383)
                                {
                                    weaponStats.Add(Convert.ToUInt32(attribute["value"]));
                                }

                            }

                            if (weaponStats.Count > 0)
                                weaponParts.Add(weaponStats.ToArray());
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return weaponParts;
        }

        public static List<string[]> GetItemProperties(string steamID64, List<string> weaponIDs, List<byte> weaponParts)
        {
            List<string[]> weaponName = new List<string[]>();
            try
            {
                string response = Json.GET(String.Format("http://steamcommunity.com/inventory/{0}/440/2?count=5000", steamID64));
                JArray assets = (JArray)JObject.Parse(response)["assets"];
                for (int i = 0; i < weaponIDs.Count && i < weaponParts.Count; i++)
                {
                    foreach (JObject asset in assets)
                    {
                        if (((string)asset["assetid"]).Contains(weaponIDs[i]))
                        {
                            string classid = (string)asset["classid"];
                            string instanceID = (string)asset["instanceid"];
                            JArray descriptions = (JArray)JObject.Parse(response)["descriptions"];
                            foreach (JObject description in descriptions)
                            {
                                if ((string)description["classid"] == classid && (string)description["instanceid"] == instanceID)
                                {
                                    weaponName.Add(new string[]
                                    {
                                        (string)description["name"],
                                        (string)description["name_color"],
                                        (string)description["icon_url_large"],
                                        (string)description["background_color"],
                                        weaponIDs[i],
                                        weaponParts[i].ToString()
                                    });
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return weaponName;
        }

        public static List<string[]> ParseItemProperties(string itemPropertiesJson, List<string> weaponIDs, List<byte> weaponParts)
        {
            List<string[]> weaponName = new List<string[]>();
            try
            {
                string response = itemPropertiesJson;
                JArray assets = (JArray)JObject.Parse(response)["assets"];
                for (int i = 0; i < weaponIDs.Count && i < weaponParts.Count; i++)
                {
                    foreach (JObject asset in assets)
                    {
                        if (((string)asset["assetid"]).Contains(weaponIDs[i]))
                        {
                            string classid = (string)asset["classid"];
                            string instanceID = (string)asset["instanceid"];
                            JArray descriptions = (JArray)JObject.Parse(response)["descriptions"];
                            foreach (JObject description in descriptions)
                            {
                                if ((string)description["classid"] == classid && (string)description["instanceid"] == instanceID)
                                {
                                    weaponName.Add(new string[]
                                    {
                                        (string)description["name"],
                                        (string)description["name_color"],
                                        (string)description["icon_url_large"],
                                        (string)description["background_color"],
                                        weaponIDs[i],
                                        weaponParts[i].ToString()
                                    });
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return weaponName;
        }

        public static List<string[]> GetAllItems(string steamID64, string quality)
        {
            List<string[]> items = new List<string[]>();
            try
            {
                string response = Json.GET(String.Format("http://steamcommunity.com/inventory/{0}/440/2?count=5000", steamID64));
                JArray descriptions = (JArray)JObject.Parse(response)["descriptions"];
                JArray assets = (JArray)JObject.Parse(response)["assets"];

                for (int i = 0; i < descriptions.Count; i++)
                {
                    string name = "";
                    string type = "";
                    List<string> parts = new List<string>();
                    string assetID = "";

                    if (((string)descriptions[i]["tags"][0]["internal_name"]).ToLower().Contains(quality.ToLower()))
                    {
                        name = (string)descriptions[i]["name"];
                        type = (string)descriptions[i]["type"];

                        //find parts
                        try
                        {
                            foreach (JObject part in (JArray)descriptions[i]["descriptions"])
                            {
                                if ((string)part["color"] == "756b5e")
                                {
                                    string partStr = ((string)part["value"]).Replace("(", string.Empty).Replace(")", string.Empty);
                                    parts.Add(partStr);
                                }
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.ToString()); 
                        }

                        //find assetid
                        foreach (JObject asset in assets)
                        {
                            if((string)descriptions[i]["classid"] == (string)asset["classid"] && (string)descriptions[i]["instanceid"] == (string)asset["instanceid"])
                            {
                                assetID = (string)asset["assetid"];
                            }
                        }

                        List<string> item = new List<string>()
                        {
                            name,
                            type
                        };

                        item.AddRange(parts);
                        item.Add(assetID);
                        items.Add(item.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return items;
        }
    }
}
