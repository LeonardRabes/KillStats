namespace KillStats
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ProfilePicture_pictureBox = new System.Windows.Forms.PictureBox();
            this.ProfileName_label = new System.Windows.Forms.Label();
            this.KillStats_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Settings_button = new System.Windows.Forms.Button();
            this.OpenStats_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicture_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KillStats_chart)).BeginInit();
            this.SuspendLayout();
            // 
            // ProfilePicture_pictureBox
            // 
            this.ProfilePicture_pictureBox.ImageLocation = "";
            this.ProfilePicture_pictureBox.InitialImage = null;
            this.ProfilePicture_pictureBox.Location = new System.Drawing.Point(444, 25);
            this.ProfilePicture_pictureBox.Name = "ProfilePicture_pictureBox";
            this.ProfilePicture_pictureBox.Size = new System.Drawing.Size(100, 100);
            this.ProfilePicture_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ProfilePicture_pictureBox.TabIndex = 3;
            this.ProfilePicture_pictureBox.TabStop = false;
            // 
            // ProfileName_label
            // 
            this.ProfileName_label.AutoSize = true;
            this.ProfileName_label.ForeColor = System.Drawing.Color.Gray;
            this.ProfileName_label.Location = new System.Drawing.Point(433, 9);
            this.ProfileName_label.Name = "ProfileName_label";
            this.ProfileName_label.Size = new System.Drawing.Size(54, 13);
            this.ProfileName_label.TabIndex = 4;
            this.ProfileName_label.Text = "Loading...";
            this.ProfileName_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KillStats_chart
            // 
            this.KillStats_chart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.KillStats_chart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(34)))));
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(34)))));
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight;
            chartArea1.BackImageTransparentColor = System.Drawing.Color.White;
            chartArea1.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.KillStats_chart.ChartAreas.Add(chartArea1);
            this.KillStats_chart.Location = new System.Drawing.Point(12, 156);
            this.KillStats_chart.Name = "KillStats_chart";
            series1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            series1.BorderWidth = 5;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.White;
            series1.LabelBackColor = System.Drawing.Color.White;
            series1.LabelBorderColor = System.Drawing.Color.White;
            series1.LabelForeColor = System.Drawing.Color.White;
            series1.Name = "AverageKills";
            series1.YValuesPerPoint = 2;
            this.KillStats_chart.Series.Add(series1);
            this.KillStats_chart.Size = new System.Drawing.Size(532, 300);
            this.KillStats_chart.TabIndex = 5;
            this.KillStats_chart.Text = "chart1";
            // 
            // Settings_button
            // 
            this.Settings_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Settings_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Settings_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(126)))), ((int)(((byte)(135)))));
            this.Settings_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.Settings_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.Settings_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Settings_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Settings_button.ForeColor = System.Drawing.Color.White;
            this.Settings_button.Location = new System.Drawing.Point(15, 127);
            this.Settings_button.Name = "Settings_button";
            this.Settings_button.Size = new System.Drawing.Size(75, 23);
            this.Settings_button.TabIndex = 6;
            this.Settings_button.Text = "Settings";
            this.Settings_button.UseVisualStyleBackColor = true;
            this.Settings_button.Click += new System.EventHandler(this.Settings_button_Click);
            this.Settings_button.MouseEnter += new System.EventHandler(this.ButtonDesign_OnMouseOver);
            this.Settings_button.MouseLeave += new System.EventHandler(this.ButtonDesign_OnMouseLeave);
            // 
            // OpenStats_button
            // 
            this.OpenStats_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OpenStats_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenStats_button.Enabled = false;
            this.OpenStats_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(126)))), ((int)(((byte)(135)))));
            this.OpenStats_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.OpenStats_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.OpenStats_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenStats_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenStats_button.ForeColor = System.Drawing.Color.White;
            this.OpenStats_button.Location = new System.Drawing.Point(96, 127);
            this.OpenStats_button.Name = "OpenStats_button";
            this.OpenStats_button.Size = new System.Drawing.Size(75, 23);
            this.OpenStats_button.TabIndex = 7;
            this.OpenStats_button.Text = "Statistics";
            this.OpenStats_button.UseVisualStyleBackColor = true;
            this.OpenStats_button.Click += new System.EventHandler(this.OpenStats_button_Click);
            this.OpenStats_button.MouseEnter += new System.EventHandler(this.ButtonDesign_OnMouseOver);
            this.OpenStats_button.MouseLeave += new System.EventHandler(this.ButtonDesign_OnMouseLeave);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(557, 468);
            this.Controls.Add(this.OpenStats_button);
            this.Controls.Add(this.Settings_button);
            this.Controls.Add(this.KillStats_chart);
            this.Controls.Add(this.ProfileName_label);
            this.Controls.Add(this.ProfilePicture_pictureBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "KillStats [Loading...]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.MouseEnter += new System.EventHandler(this.MainForm_MouseEnter);
            this.Move += new System.EventHandler(this.MainForm_Changed);
            this.Resize += new System.EventHandler(this.MainForm_Changed);
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicture_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KillStats_chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox ProfilePicture_pictureBox;
        private System.Windows.Forms.Label ProfileName_label;
        private System.Windows.Forms.DataVisualization.Charting.Chart KillStats_chart;
        private System.Windows.Forms.Button OpenStats_button;
        private System.Windows.Forms.Button Settings_button;
        private PresetList WeaponPresets_presetList;
        private ItemView AddItem_itemView;

        private void InitializeCustomComponent()
        {
            this.MaximizeBox = false;

            this.ProfilePicture_pictureBox.ImageLocation = System.Windows.Forms.Application.StartupPath + @"\resources\loading.gif";
            this.ProfilePicture_pictureBox.InitialImage = ProfilePicture_pictureBox.Image;

            this.WeaponPresets_presetList = new PresetList(new System.Drawing.Point(455, 130), 75, 23);
            this.WeaponPresets_presetList.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(118, 126, 135);
            this.WeaponPresets_presetList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.WeaponPresets_presetList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(27, 40, 56);
            this.WeaponPresets_presetList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WeaponPresets_presetList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WeaponPresets_presetList.ForeColor = System.Drawing.Color.White;
            this.WeaponPresets_presetList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WeaponPresets_presetList.MouseEnter += ButtonDesign_OnMouseOver;
            this.WeaponPresets_presetList.MouseLeave += ButtonDesign_OnMouseLeave;
            this.WeaponPresets_presetList.ItemAdded += WeaponPresets_presetList_Added;
            this.WeaponPresets_presetList.ItemRemoved += WeaponPresets_presetList_Removed;
            this.WeaponPresets_presetList.ItemSelected += WeaponPresets_presetList_Selected;
            this.WeaponPresets_presetList.ItemNameEdited += WeaponPresets_presetList_NameEdited;

            this.KillStats_chart.ChartAreas[0].AxisX.Title = "Time in Minutes";
            this.KillStats_chart.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Gray;
            this.KillStats_chart.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Gray;
            this.KillStats_chart.ChartAreas[0].AxisY.Title = "Average Points";
            this.KillStats_chart.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Gray;
            this.KillStats_chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Gray;

            this.AddItem_itemView = new ItemView
                (
                "Add Item",
                System.Drawing.Color.Gray,
                System.Windows.Forms.Application.StartupPath + @"\resources\add_icon_large.png",
                System.Drawing.Color.Empty,
                new System.Drawing.Point(25, 25),
                400, 75, 8
                );
            this.AddItem_itemView.ItemImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddItem_itemView.ItemNameLabel.Location = new System.Drawing.Point(80, 25);
            this.AddItem_itemView.ItemNameLabel.Font = new System.Drawing.Font("Motiva Sans", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddItem_itemView.ItemImageBorder.BackColor = System.Drawing.Color.Gray;
            this.AddItem_itemView.BackColor = System.Drawing.Color.FromArgb(22, 32, 45);
            this.AddItem_itemView.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.AddItem_itemView.ItemOverlayClicked += AddItem_itemView_Click;

            this.Controls.Add(WeaponPresets_presetList);
            this.Controls.Add(AddItem_itemView);
        }

        private void ButtonDesign_OnMouseOver(object sender, System.EventArgs e)
        {
            System.Windows.Forms.Button button = sender as System.Windows.Forms.Button;
            button.FlatAppearance.BorderColor = System.Drawing.Color.White;
        }

        private void ButtonDesign_OnMouseLeave(object sender, System.EventArgs e)
        {
            System.Windows.Forms.Button button = sender as System.Windows.Forms.Button;
            button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(118, 126, 135);
        }
    }
}

