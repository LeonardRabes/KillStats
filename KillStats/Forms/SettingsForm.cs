using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using IWshRuntimeLibrary;
using Newtonsoft.Json.Linq;
using SteamWebAPI;

namespace KillStats
{
    public partial class SettingsForm : Form
    {
        private List<string[]> _AllItems;
        public List<string[]> AllItems
        {
            get
            {
                return _AllItems;
            }
            set
            {
                _AllItems = value;
                if (AllItems != null)
                {
                    foreach (string[] item in AllItems)
                    {
                        ListViewItem liitem = new ListViewItem(item);
                        this.SelectableItems_listView.Items.Add(liitem);
                    }
                }
            }
        }

        private MainForm sender;
        private string SteamID64;
        private string ProfileURL;
        private string WeaponID;
        private string APIkey;

        public SettingsForm(object _sender)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            sender = (MainForm)_sender;
        }

        private void KsSettingsForm_Load(object _sender, EventArgs e)
        {
            this.Location = new Point(sender.Location.X + sender.Width - 15, sender.Location.Y);
            this.Height = sender.Height;
            //Load settings.json and apply values
            string config = System.IO.File.ReadAllText(Application.StartupPath + @"\config\settings.json");
            JObject APIsettings = (JObject)JObject.Parse(config)["APIsettings"];
            SteamID64 = (string)APIsettings["steamID64"];
            ProfileURL = (string)APIsettings["profileURL"];
            APIkey = (string)APIsettings["APIkey"];
            WeaponID = (string)APIsettings["weaponID"];

            //Print values in textBox
            SettingsSID_textBox.Text = SteamID64;
            SettingsCURL_textBox.Text = ProfileURL;
            SettingsAPIK_textBox.Text = APIkey;
        }

        private void KsSettingsForm_FormClosing(object _sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
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
                    ProfileURL = SettingsCURL_textBox.Text;
                    APIkey = SettingsAPIK_textBox.Text;

                    //Save changes in settings.json
                    string config = System.IO.File.ReadAllText(Application.StartupPath + @"\config\settings.json");
                    JObject configJson = JObject.Parse(config);
                    configJson["APIsettings"]["steamID64"] = _steamID64;
                    configJson["APIsettings"]["profileURL"] = ProfileURL;
                    configJson["APIsettings"]["APIkey"] = APIkey;
                    System.IO.File.WriteAllText(Application.StartupPath + @"\config\settings.json", configJson.ToString());

                    //If steamID is different; restart application
                    if (_steamID64 != SteamID64)
                    {
                        System.Diagnostics.Process.Start(Application.ExecutablePath);
                        sender.Close();
                    }

                    this.Close();
                }
                //Reset steamID and URL to prev values (undo changes)
                else
                {
                    SteamID64 = SettingsSID_textBox.Text;
                    SettingsCURL_textBox.Text = ProfileURL;
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
            if (e.TabPage.Name == "selectItem_tabPage" && AllItems != null)
            {
            }
        }

        private void SelectableItems_listView_Click(object _sender, EventArgs e)
        {
            //Save selected item
            ListViewItem.ListViewSubItemCollection selectedItem = ((ListView)_sender).SelectedItems[0].SubItems;
            SelectItem_button.Enabled = true;
            SelectPart_comboBox.Enabled = true;
            WeaponID = selectedItem[selectedItem.Count - 1].Text;

            SelectPart_comboBox.Items.Clear();
            string part1 = selectedItem[1].Text;

            if (part1 != "")
            {
                part1 = part1.Substring(part1.IndexOf("-") + 2, part1.Length - (part1.IndexOf("-") + 2));
                SelectPart_comboBox.Items.Add(part1);
            }

            for (int i = 2; i < selectedItem.Count - 1; i++)
            {
                SelectPart_comboBox.Items.Add(selectedItem[i].Text);
            }

            SelectPart_comboBox.SelectedIndex = 0;
        }

        private void SelectItem_button_Click(object _sender, EventArgs e)
        {
            if (sender.ItemViewList.ItemViews.Count < 6)
            {
                //Add weapon
                List<string> weaponIDs = sender.WeaponIDs;
                List<byte> weaponParts = sender.WeaponParts;
                weaponIDs.Add(WeaponID);
                weaponParts.Add(Convert.ToByte(SelectPart_comboBox.SelectedIndex));

                sender.AddItem(WeaponID, Convert.ToByte(SelectPart_comboBox.SelectedIndex));

                //save weaponID changes in settings.json
                string config = System.IO.File.ReadAllText(sender.ActivePreset);
                JObject configJson = JObject.Parse(config);
                string weaponIDsJson = "";

                for (int i = 0; i < sender.WeaponIDs.Count && i < sender.WeaponParts.Count; i++)
                {
                    weaponIDsJson += "{\"weaponID\": \"" + weaponIDs[i] + "\",\"weaponPart\": \"" + weaponParts[i] + "\"},";
                }
                configJson["weaponIDs"] = JArray.Parse("[" + weaponIDsJson + "]");
                System.IO.File.WriteAllText(sender.ActivePreset, configJson.ToString());
            }
        }

        private void SteamAPI_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://steamcommunity.com/dev/apikey");
        }

        private void SteamAPI_linkLabel_MouseHover(object sender, EventArgs e)
        {
            label4.Visible = true;
        }

        private void SearchItems_textBox_TextChanged(object sender, EventArgs e)
        {
            List<string[]> filteredItems = new List<string[]>();

            foreach (string[] item in AllItems)
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

        private void CreateShortcut_button_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"/config/launchtf.bat";
            System.IO.File.Create(path).Close();
            System.IO.File.WriteAllText(path, "@echo off \nstart steam://rungameid/440 \ncd \"" + Application.StartupPath + "\" \nstart KillStats.exe \nexit\"");

            object shDesktop = (object)"Desktop";
            WshShell shell = new WshShell();
            string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\Team Fortress 2.lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "Shortcut for TF2 with Stats";
            shortcut.IconLocation = Application.StartupPath + @"\KillStats.ico";
            shortcut.TargetPath = path;
            shortcut.Save();
        }
    }
}
