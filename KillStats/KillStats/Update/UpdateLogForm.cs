using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillStats
{
    public partial class UpdateLogForm : Form
    {
        public UpdateLogForm(string log, string path)
        {
            InitializeComponent();
            UpdateLog_textBox.Text = log;
            Path = path;
        }
        private string Path;

        private void CloseLog_button_Click(object sender, EventArgs e)
        {
            if (DeleteLog_checkBox.Checked)
            {
                File.Delete(Path);
            }

            Close();
        }
    }
}
