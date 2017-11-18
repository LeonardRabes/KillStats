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
    class Client
    {
        public static string GetSteamID64(string profileUrl, string APIkey)
        {
            Uri customUri = new Uri(profileUrl);
            string steamID64 = null;

            if (customUri.Segments[1] == "id/")
            {
                string response = Json.GET(String.Format("http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?vanityurl={0}&key={1}", customUri.Segments[2].Replace("/", string.Empty), APIkey));
                steamID64 = (string)JObject.Parse(response)["response"]["steamid"];
            }
            if (customUri.Segments[1] == "profiles/")
            {
                steamID64 = customUri.Segments[2].Replace("/", string.Empty);
            }

            return steamID64;
        }

        public static bool IsOnServer(string steamID64, string APIkey)
        {
            string response = Json.GET(String.Format("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?steamids={0}&key={1}", steamID64, APIkey));
            JObject summary = (JObject)JObject.Parse(response)["response"]["players"][0];
            return summary["gameserverip"] != null;
        }

        public static string[] PlayerSummary(string steamID64, string APIkey)
        {
            string response = Json.GET(String.Format("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?steamids={0}&key={1}", steamID64, APIkey));
            JObject summary = (JObject)JObject.Parse(response)["response"]["players"][0];
            return new string[]
            {
                (string)summary["personaname"],
                (string)summary["avatarfull"],
                (string)summary["gameextrainfo"],
                (string)summary["gameserverip"],
                (string)summary["loccountrycode"]
            };
        }
    }
}
