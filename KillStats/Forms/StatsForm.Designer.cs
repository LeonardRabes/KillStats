namespace KillStats
{
    partial class StatsForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.KillStats_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.FileTime_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.KillStats_chart)).BeginInit();
            this.SuspendLayout();
            // 
            // KillStats_chart
            // 
            this.KillStats_chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KillStats_chart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(34)))));
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(34)))));
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight;
            chartArea1.BackImageTransparentColor = System.Drawing.Color.White;
            chartArea1.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.KillStats_chart.ChartAreas.Add(chartArea1);
            this.KillStats_chart.Location = new System.Drawing.Point(12, 53);
            this.KillStats_chart.Name = "KillStats_chart";
            series1.ChartArea = "ChartArea1";
            series1.Name = "StatsSeries";
            this.KillStats_chart.Series.Add(series1);
            this.KillStats_chart.Size = new System.Drawing.Size(851, 341);
            this.KillStats_chart.TabIndex = 6;
            this.KillStats_chart.Text = "chart1";
            this.KillStats_chart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.KillStats_chart_MouseMove);
            // 
            // FileName_label
            // 
            this.FileTime_label.AutoSize = true;
            this.FileTime_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileTime_label.ForeColor = System.Drawing.Color.Gray;
            this.FileTime_label.Location = new System.Drawing.Point(12, 20);
            this.FileTime_label.Name = "FileName_label";
            this.FileTime_label.Size = new System.Drawing.Size(106, 26);
            this.FileTime_label.TabIndex = 7;
            this.FileTime_label.Text = "FileName";
            // 
            // StatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(875, 406);
            this.Controls.Add(this.FileTime_label);
            this.Controls.Add(this.KillStats_chart);
            this.Name = "StatsForm";
            this.Text = "Statistics";
            this.Load += new System.EventHandler(this.StatsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.KillStats_chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart KillStats_chart;
        private System.Windows.Forms.Label FileTime_label;
    }
}