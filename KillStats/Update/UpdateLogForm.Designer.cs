namespace KillStats
{
    partial class UpdateLogForm
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
            this.CloseLog_button = new System.Windows.Forms.Button();
            this.UpdateLog_textBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DeleteLog_checkBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CloseLog_button
            // 
            this.CloseLog_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseLog_button.Location = new System.Drawing.Point(331, 273);
            this.CloseLog_button.Name = "CloseLog_button";
            this.CloseLog_button.Size = new System.Drawing.Size(75, 23);
            this.CloseLog_button.TabIndex = 0;
            this.CloseLog_button.Text = "OK";
            this.CloseLog_button.UseVisualStyleBackColor = true;
            this.CloseLog_button.Click += new System.EventHandler(this.CloseLog_button_Click);
            // 
            // UpdateLog_textBox
            // 
            this.UpdateLog_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateLog_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateLog_textBox.Location = new System.Drawing.Point(12, 25);
            this.UpdateLog_textBox.Name = "UpdateLog_textBox";
            this.UpdateLog_textBox.ReadOnly = true;
            this.UpdateLog_textBox.Size = new System.Drawing.Size(394, 242);
            this.UpdateLog_textBox.TabIndex = 1;
            this.UpdateLog_textBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Update";
            // 
            // DeleteLog_checkBox
            // 
            this.DeleteLog_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteLog_checkBox.AutoSize = true;
            this.DeleteLog_checkBox.Location = new System.Drawing.Point(214, 277);
            this.DeleteLog_checkBox.Name = "DeleteLog_checkBox";
            this.DeleteLog_checkBox.Size = new System.Drawing.Size(111, 17);
            this.DeleteLog_checkBox.TabIndex = 3;
            this.DeleteLog_checkBox.Text = "Don\'t show again.";
            this.DeleteLog_checkBox.UseVisualStyleBackColor = true;
            // 
            // UpdateLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 308);
            this.Controls.Add(this.DeleteLog_checkBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UpdateLog_textBox);
            this.Controls.Add(this.CloseLog_button);
            this.Name = "UpdateLogForm";
            this.Text = "Update Log";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CloseLog_button;
        private System.Windows.Forms.RichTextBox UpdateLog_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox DeleteLog_checkBox;
    }
}