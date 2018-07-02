namespace KillStats
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.Settings_tabControl = new System.Windows.Forms.TabControl();
            this.settings_tabPage = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.CreateShortcut_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.steamAPI_linkLabel = new System.Windows.Forms.LinkLabel();
            this.SaveSettings_button = new System.Windows.Forms.Button();
            this.SettingsAPIK_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SettingsSID_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SettingsCURL_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectItem_tabPage = new System.Windows.Forms.TabPage();
            this.SelectPart_comboBox = new System.Windows.Forms.ComboBox();
            this.SearchItems_textBox = new System.Windows.Forms.TextBox();
            this.SelectItem_button = new System.Windows.Forms.Button();
            this.SelectableItems_listView = new System.Windows.Forms.ListView();
            this.name_header = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type_header = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Autorun_button = new System.Windows.Forms.Button();
            this.Settings_tabControl.SuspendLayout();
            this.settings_tabPage.SuspendLayout();
            this.selectItem_tabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // Settings_tabControl
            // 
            this.Settings_tabControl.Controls.Add(this.settings_tabPage);
            this.Settings_tabControl.Controls.Add(this.selectItem_tabPage);
            this.Settings_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Settings_tabControl.Location = new System.Drawing.Point(0, 0);
            this.Settings_tabControl.Name = "Settings_tabControl";
            this.Settings_tabControl.SelectedIndex = 0;
            this.Settings_tabControl.Size = new System.Drawing.Size(661, 417);
            this.Settings_tabControl.TabIndex = 0;
            this.Settings_tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.Settings_tabControl_Selected);
            // 
            // settings_tabPage
            // 
            this.settings_tabPage.Controls.Add(this.Autorun_button);
            this.settings_tabPage.Controls.Add(this.label5);
            this.settings_tabPage.Controls.Add(this.CreateShortcut_button);
            this.settings_tabPage.Controls.Add(this.label4);
            this.settings_tabPage.Controls.Add(this.steamAPI_linkLabel);
            this.settings_tabPage.Controls.Add(this.SaveSettings_button);
            this.settings_tabPage.Controls.Add(this.SettingsAPIK_textBox);
            this.settings_tabPage.Controls.Add(this.label3);
            this.settings_tabPage.Controls.Add(this.SettingsSID_textBox);
            this.settings_tabPage.Controls.Add(this.label2);
            this.settings_tabPage.Controls.Add(this.SettingsCURL_textBox);
            this.settings_tabPage.Controls.Add(this.label1);
            this.settings_tabPage.Location = new System.Drawing.Point(4, 22);
            this.settings_tabPage.Name = "settings_tabPage";
            this.settings_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.settings_tabPage.Size = new System.Drawing.Size(653, 391);
            this.settings_tabPage.TabIndex = 0;
            this.settings_tabPage.Text = "Settings";
            this.settings_tabPage.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Start KillStats with TF2: ";
            // 
            // CreateShortcut_button
            // 
            this.CreateShortcut_button.Location = new System.Drawing.Point(133, 188);
            this.CreateShortcut_button.Name = "CreateShortcut_button";
            this.CreateShortcut_button.Size = new System.Drawing.Size(91, 23);
            this.CreateShortcut_button.TabIndex = 9;
            this.CreateShortcut_button.Text = "Create Shortcut";
            this.CreateShortcut_button.UseVisualStyleBackColor = true;
            this.CreateShortcut_button.Click += new System.EventHandler(this.CreateShortcut_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(419, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tip: If you do not have a Domain, just type \"none\" in the text box of the steam w" +
    "ebsite. ";
            this.label4.Visible = false;
            // 
            // steamAPI_linkLabel
            // 
            this.steamAPI_linkLabel.AutoSize = true;
            this.steamAPI_linkLabel.LinkArea = new System.Windows.Forms.LinkArea(23, 27);
            this.steamAPI_linkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.steamAPI_linkLabel.Location = new System.Drawing.Point(92, 155);
            this.steamAPI_linkLabel.Name = "steamAPI_linkLabel";
            this.steamAPI_linkLabel.Size = new System.Drawing.Size(153, 17);
            this.steamAPI_linkLabel.TabIndex = 7;
            this.steamAPI_linkLabel.TabStop = true;
            this.steamAPI_linkLabel.Text = "Get your Steam API Key here";
            this.steamAPI_linkLabel.UseCompatibleTextRendering = true;
            this.steamAPI_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SteamAPI_linkLabel_LinkClicked);
            this.steamAPI_linkLabel.MouseHover += new System.EventHandler(this.SteamAPI_linkLabel_MouseHover);
            // 
            // SaveSettings_button
            // 
            this.SaveSettings_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveSettings_button.Location = new System.Drawing.Point(570, 360);
            this.SaveSettings_button.Name = "SaveSettings_button";
            this.SaveSettings_button.Size = new System.Drawing.Size(75, 23);
            this.SaveSettings_button.TabIndex = 6;
            this.SaveSettings_button.Text = "Save";
            this.SaveSettings_button.UseVisualStyleBackColor = true;
            this.SaveSettings_button.Click += new System.EventHandler(this.SaveSettings_button_Click);
            // 
            // SettingsAPIK_textBox
            // 
            this.SettingsAPIK_textBox.Location = new System.Drawing.Point(95, 132);
            this.SettingsAPIK_textBox.Name = "SettingsAPIK_textBox";
            this.SettingsAPIK_textBox.Size = new System.Drawing.Size(276, 20);
            this.SettingsAPIK_textBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Steam API Key:";
            // 
            // SettingsSID_textBox
            // 
            this.SettingsSID_textBox.Enabled = false;
            this.SettingsSID_textBox.Location = new System.Drawing.Point(95, 21);
            this.SettingsSID_textBox.Name = "SettingsSID_textBox";
            this.SettingsSID_textBox.Size = new System.Drawing.Size(276, 20);
            this.SettingsSID_textBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Steam ID:";
            // 
            // SettingsCURL_textBox
            // 
            this.SettingsCURL_textBox.Location = new System.Drawing.Point(95, 74);
            this.SettingsCURL_textBox.Name = "SettingsCURL_textBox";
            this.SettingsCURL_textBox.Size = new System.Drawing.Size(276, 20);
            this.SettingsCURL_textBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Profile URL:";
            // 
            // selectItem_tabPage
            // 
            this.selectItem_tabPage.Controls.Add(this.SelectPart_comboBox);
            this.selectItem_tabPage.Controls.Add(this.SearchItems_textBox);
            this.selectItem_tabPage.Controls.Add(this.SelectItem_button);
            this.selectItem_tabPage.Controls.Add(this.SelectableItems_listView);
            this.selectItem_tabPage.Location = new System.Drawing.Point(4, 22);
            this.selectItem_tabPage.Name = "selectItem_tabPage";
            this.selectItem_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.selectItem_tabPage.Size = new System.Drawing.Size(653, 391);
            this.selectItem_tabPage.TabIndex = 1;
            this.selectItem_tabPage.Text = "Select Item";
            this.selectItem_tabPage.UseVisualStyleBackColor = true;
            // 
            // SelectPart_comboBox
            // 
            this.SelectPart_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectPart_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectPart_comboBox.Enabled = false;
            this.SelectPart_comboBox.FormattingEnabled = true;
            this.SelectPart_comboBox.Location = new System.Drawing.Point(395, 360);
            this.SelectPart_comboBox.Name = "SelectPart_comboBox";
            this.SelectPart_comboBox.Size = new System.Drawing.Size(169, 21);
            this.SelectPart_comboBox.TabIndex = 3;
            // 
            // SearchItems_textBox
            // 
            this.SearchItems_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SearchItems_textBox.Location = new System.Drawing.Point(6, 363);
            this.SearchItems_textBox.Name = "SearchItems_textBox";
            this.SearchItems_textBox.Size = new System.Drawing.Size(177, 20);
            this.SearchItems_textBox.TabIndex = 2;
            this.SearchItems_textBox.TextChanged += new System.EventHandler(this.SearchItems_textBox_TextChanged);
            // 
            // SelectItem_button
            // 
            this.SelectItem_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectItem_button.Enabled = false;
            this.SelectItem_button.Location = new System.Drawing.Point(570, 360);
            this.SelectItem_button.Name = "SelectItem_button";
            this.SelectItem_button.Size = new System.Drawing.Size(75, 23);
            this.SelectItem_button.TabIndex = 1;
            this.SelectItem_button.Text = "Select";
            this.SelectItem_button.UseVisualStyleBackColor = true;
            this.SelectItem_button.Click += new System.EventHandler(this.SelectItem_button_Click);
            // 
            // SelectableItems_listView
            // 
            this.SelectableItems_listView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectableItems_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name_header,
            this.type_header});
            this.SelectableItems_listView.Location = new System.Drawing.Point(6, 6);
            this.SelectableItems_listView.MultiSelect = false;
            this.SelectableItems_listView.Name = "SelectableItems_listView";
            this.SelectableItems_listView.Size = new System.Drawing.Size(639, 348);
            this.SelectableItems_listView.TabIndex = 0;
            this.SelectableItems_listView.UseCompatibleStateImageBehavior = false;
            this.SelectableItems_listView.View = System.Windows.Forms.View.Details;
            this.SelectableItems_listView.Click += new System.EventHandler(this.SelectableItems_listView_Click);
            // 
            // name_header
            // 
            this.name_header.Text = "Name";
            this.name_header.Width = 367;
            // 
            // type_header
            // 
            this.type_header.Text = "Type";
            this.type_header.Width = 243;
            // 
            // Autorun_button
            // 
            this.Autorun_button.Location = new System.Drawing.Point(230, 188);
            this.Autorun_button.Name = "Autorun_button";
            this.Autorun_button.Size = new System.Drawing.Size(141, 23);
            this.Autorun_button.TabIndex = 11;
            this.Autorun_button.Text = "Autorun KillStats with Tf2";
            this.Autorun_button.UseVisualStyleBackColor = true;
            this.Autorun_button.Click += new System.EventHandler(this.Autorun_button_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 417);
            this.Controls.Add(this.Settings_tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KsSettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.KsSettingsForm_Load);
            this.Settings_tabControl.ResumeLayout(false);
            this.settings_tabPage.ResumeLayout(false);
            this.settings_tabPage.PerformLayout();
            this.selectItem_tabPage.ResumeLayout(false);
            this.selectItem_tabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage settings_tabPage;
        private System.Windows.Forms.TabPage selectItem_tabPage;
        private System.Windows.Forms.ListView SelectableItems_listView;
        private System.Windows.Forms.ColumnHeader name_header;
        private System.Windows.Forms.ColumnHeader type_header;
        private System.Windows.Forms.Button SelectItem_button;
        private System.Windows.Forms.Button SaveSettings_button;
        private System.Windows.Forms.TextBox SettingsAPIK_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SettingsSID_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SettingsCURL_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel steamAPI_linkLabel;
        public System.Windows.Forms.TabControl Settings_tabControl;
        private System.Windows.Forms.TextBox SearchItems_textBox;
        private System.Windows.Forms.ComboBox SelectPart_comboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button CreateShortcut_button;
        private System.Windows.Forms.Button Autorun_button;
    }
}