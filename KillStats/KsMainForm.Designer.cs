namespace KillStats
{
    partial class KsMainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KsMainForm));
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.ProfilePicture_pictureBox = new System.Windows.Forms.PictureBox();
            this.ProfileName_label = new System.Windows.Forms.Label();
            this.KillStats_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Settings_button = new System.Windows.Forms.Button();
            this.ClientNotOnServer_panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicture_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KillStats_chart)).BeginInit();
            this.ClientNotOnServer_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 30000;
            this.refreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
            // 
            // ProfilePicture_pictureBox
            // 
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
            this.KillStats_chart.Location = new System.Drawing.Point(12, 131);
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
            this.Settings_button.Location = new System.Drawing.Point(15, 102);
            this.Settings_button.Name = "Settings_button";
            this.Settings_button.Size = new System.Drawing.Size(75, 23);
            this.Settings_button.TabIndex = 6;
            this.Settings_button.Text = "Settings";
            this.Settings_button.UseVisualStyleBackColor = true;
            this.Settings_button.Click += new System.EventHandler(this.Settings_button_Click);
            // 
            // ClientNotOnServer_panel
            // 
            this.ClientNotOnServer_panel.AutoSize = true;
            this.ClientNotOnServer_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.ClientNotOnServer_panel.Controls.Add(this.label1);
            this.ClientNotOnServer_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientNotOnServer_panel.Location = new System.Drawing.Point(0, 0);
            this.ClientNotOnServer_panel.Name = "ClientNotOnServer_panel";
            this.ClientNotOnServer_panel.Size = new System.Drawing.Size(557, 443);
            this.ClientNotOnServer_panel.TabIndex = 7;
            this.ClientNotOnServer_panel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(123, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player is currently not on a server";
            // 
            // KsMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(557, 443);
            this.Controls.Add(this.Settings_button);
            this.Controls.Add(this.KillStats_chart);
            this.Controls.Add(this.ProfileName_label);
            this.Controls.Add(this.ProfilePicture_pictureBox);
            this.Controls.Add(this.ClientNotOnServer_panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KsMainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "KillStats [Loading...]";
            this.Load += new System.EventHandler(this.KsMainForm_Load);
            this.Shown += new System.EventHandler(this.KsMainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePicture_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KillStats_chart)).EndInit();
            this.ClientNotOnServer_panel.ResumeLayout(false);
            this.ClientNotOnServer_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.PictureBox ProfilePicture_pictureBox;
        private System.Windows.Forms.Label ProfileName_label;
        private System.Windows.Forms.DataVisualization.Charting.Chart KillStats_chart;
        public System.Windows.Forms.Button Settings_button;
        private System.Windows.Forms.Panel ClientNotOnServer_panel;
        private System.Windows.Forms.Label label1;
    }
}

