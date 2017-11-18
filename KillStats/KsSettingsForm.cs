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
    public partial class KsSettingsForm : Form
    {
        private KsMainForm sender;

        List<string[]> allItems;
        private string steamID64;
        private string profileURL;
        private string weaponID;
        private string APIkey;
        private bool IsFirstStartup;


        public KsSettingsForm(object _sender, bool _IsFirstStartup)
        {
            InitializeComponent();
            sender = (KsMainForm)_sender;
            IsFirstStartup = _IsFirstStartup;
        }

        private void KsSettingsForm_Load(object sender, EventArgs e)
        {
            //Load settings.json and apply values
            string config = System.IO.File.ReadAllText(Application.StartupPath + @"\config\settings.json");
            JObject APIsettings = (JObject)JObject.Parse(config)["APIsettings"];
            steamID64 = (string)APIsettings["steamID64"];
            profileURL = (string)APIsettings["profileURL"];
            APIkey = (string)APIsettings["APIkey"];
            weaponID = (string)APIsettings["weaponID"];

            //Print values in textBox
            SettingsSID_textBox.Text = steamID64;
            SettingsCURL_textBox.Text = profileURL;
            SettingsAPIK_textBox.Text = APIkey;
        }

        private void KsSettingsForm_FormClosing(object _sender, FormClosingEventArgs e)
        {
            if (IsFirstStartup == true)
                sender.Refresh();
            sender.Settings_button.Enabled = true;
        }

        private void SaveSettings_button_Click(object _sender, EventArgs e)
        {
            try
            {
                string _steamID64 = Client.GetSteamID64(SettingsCURL_textBox.Text, SettingsAPIK_textBox.Text);

                //If steamID could be received
                if (_steamID64 != null)
                {
                    SettingsSID_textBox.Text = _steamID64;
                    profileURL = SettingsCURL_textBox.Text;
                    APIkey = SettingsAPIK_textBox.Text;

                    //Save changes in settings.json
                    string config = System.IO.File.ReadAllText(Application.StartupPath + @"\config\settings.json");
                    JObject configJson = JObject.Parse(config);
                    configJson["APIsettings"]["steamID64"] = _steamID64;
                    configJson["APIsettings"]["profileURL"] = profileURL;
                    configJson["APIsettings"]["APIkey"] = APIkey;
                    System.IO.File.WriteAllText(Application.StartupPath + @"\config\settings.json", configJson.ToString());

                    //If steamID is different; restart application
                    if (_steamID64 != steamID64)
                    {
                        System.Diagnostics.Process.Start(Application.ExecutablePath);
                        sender.Close();
                    }

                    this.Close();
                }
                //Reset steamID and URL to prev values (undo changes)
                else
                {
                    steamID64 = SettingsSID_textBox.Text;
                    SettingsCURL_textBox.Text = profileURL;
                }

                APIkey = SettingsAPIK_textBox.Text;
            }
            //If API key is wrong, json parser throws an error => reset APIkey and URL
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                SettingsAPIK_textBox.Text = APIkey;
                //SettingsCURL_textBox.Text = profileURL;
            }
        }

        private void Settings_tabControl_Selected(object sender, TabControlEventArgs e)
        {
            //If item tab is opened
            if(e.TabPage.Name == "selectItem_tabPage")
            {
                allItems = Inventory.GetAllItems(steamID64, "strange");

                foreach(string[] item in allItems)
                {
                    ListViewItem liitem = new ListViewItem(item);
                    this.SelectableItems_listView.Items.Add(liitem);
                }
            }
        }

        private void SelectableItems_listView_Click(object _sender, EventArgs e)
        {
            //Save selected item
            ListViewItem.ListViewSubItemCollection selectedItem = ((ListView)_sender).SelectedItems[0].SubItems;
            weaponID = selectedItem[selectedItem.Count - 1].Text;

            selectPart_comboBox.Items.Clear();
            string part1 = selectedItem[1].Text;

            if (part1 != "")
            {
                part1 = part1.Substring(part1.IndexOf("-") + 2, part1.Length - (part1.IndexOf("-") + 2));
                selectPart_comboBox.Items.Add(part1);
            }

            for (int i = 2; i < selectedItem.Count - 1; i++)
            {
                selectPart_comboBox.Items.Add(selectedItem[i].Text);
            }

            selectPart_comboBox.SelectedIndex = 0;
        }

        private void SelectItem_button_Click(object _sender, EventArgs e)
        {
            //Reset main form to default with new weaponID
            sender.weaponIDs.Add(weaponID);
            sender.weaponParts.Add(Convert.ToByte(selectPart_comboBox.SelectedIndex));
            sender.Restart();

            //save weaponID changes in settings.json
            string config = System.IO.File.ReadAllText(Application.StartupPath + @"\config\settings.json");
            JObject configJson = JObject.Parse(config);
            string weaponIDsJson = "";

            for(int i = 0; i < sender.weaponIDs.Count && i < sender.weaponParts.Count; i++)
            {
                weaponIDsJson += "{\"weaponID\": \"" + sender.weaponIDs[i] + "\",\"weaponPart\": \""+ sender.weaponParts[i] + "\"},";
            }
            configJson["APIsettings"]["weaponIDs"] = JArray.Parse("[" + weaponIDsJson +"]");
            System.IO.File.WriteAllText(Application.StartupPath + @"\config\settings.json", configJson.ToString());

            this.Close();
        }

        private void SteamAPI_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://steamcommunity.com/dev/apikey");
        }

        private void steamAPI_linkLabel_MouseHover(object sender, EventArgs e)
        {
            label4.Visible = true;
        }

        private void SearchItems_textBox_TextChanged(object sender, EventArgs e)
        {
            List<string[]> filteredItems = new List<string[]>();

            foreach (string[] item in allItems)
            {
                if (item[0].ToLower().Contains(SearchItems_textBox.Text.ToLower()))
                {
                    filteredItems.Add(item);
                }
            }

            SelectableItems_listView.Items.Clear();

            foreach (string[] item in filteredItems)
            {
                ListViewItem liitem = new ListViewItem(item);
                this.SelectableItems_listView.Items.Add(liitem);
            }
        }
    }
}
