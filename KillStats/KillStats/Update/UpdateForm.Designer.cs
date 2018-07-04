namespace KillStats
{
    partial class UpdateForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.CancelUpd_button = new System.Windows.Forms.Button();
            this.StartUpd_button = new System.Windows.Forms.Button();
            this.Download_progressBar = new System.Windows.Forms.ProgressBar();
            this.Status_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "A new Update is available. Do you want to install it?\r\n";
            // 
            // CancelUpd_button
            // 
            this.CancelUpd_button.Location = new System.Drawing.Point(12, 40);
            this.CancelUpd_button.Name = "CancelUpd_button";
            this.CancelUpd_button.Size = new System.Drawing.Size(75, 23);
            this.CancelUpd_button.TabIndex = 1;
            this.CancelUpd_button.Text = "No";
            this.CancelUpd_button.UseVisualStyleBackColor = true;
            this.CancelUpd_button.Click += new System.EventHandler(this.CancelUpd_button_Click);
            // 
            // StartUpd_button
            // 
            this.StartUpd_button.Location = new System.Drawing.Point(191, 40);
            this.StartUpd_button.Name = "StartUpd_button";
            this.StartUpd_button.Size = new System.Drawing.Size(75, 23);
            this.StartUpd_button.TabIndex = 2;
            this.StartUpd_button.Text = "Yes";
            this.StartUpd_button.UseVisualStyleBackColor = true;
            this.StartUpd_button.Click += new System.EventHandler(this.StartUpd_button_Click);
            // 
            // Download_progressBar
            // 
            this.Download_progressBar.Location = new System.Drawing.Point(12, 91);
            this.Download_progressBar.Name = "Download_progressBar";
            this.Download_progressBar.Size = new System.Drawing.Size(254, 23);
            this.Download_progressBar.TabIndex = 3;
            // 
            // Status_label
            // 
            this.Status_label.AutoSize = true;
            this.Status_label.Location = new System.Drawing.Point(12, 75);
            this.Status_label.Name = "Status_label";
            this.Status_label.Size = new System.Drawing.Size(61, 13);
            this.Status_label.TabIndex = 4;
            this.Status_label.Text = "Preparing...";
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(279, 133);
            this.Controls.Add(this.Status_label);
            this.Controls.Add(this.Download_progressBar);
            this.Controls.Add(this.StartUpd_button);
            this.Controls.Add(this.CancelUpd_button);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UpdateForm";
            this.Text = "Update";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CancelUpd_button;
        private System.Windows.Forms.Button StartUpd_button;
        private System.Windows.Forms.ProgressBar Download_progressBar;
        public System.Windows.Forms.Label Status_label;
    }
}