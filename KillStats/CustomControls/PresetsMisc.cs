using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KillStats
{
    public class ItemCollection<T> : List<T>
    {
        public delegate void ItemAddedEventHandler(object sender, object newItem, EventArgs e);
        public event ItemAddedEventHandler ItemAdded;
        protected virtual void OnItemAdded(object item)
        {
            ItemAdded?.Invoke(this, item, EventArgs.Empty);
        }

        new public void Add(T item)
        {
            base.Add(item);
            OnItemAdded(item);
        }

        new public void AddRange(IEnumerable<T> items)
        {
            base.AddRange(items);
            OnItemAdded(items);
        }
    }

    public class PresetItem 
    {
        public string Name { get; set; }
        public string FilePath { get; set; }

        public PresetItem(string name, string filePath)
        {
            Name = name;
            FilePath = filePath;
        }
    }

    public class PresetItemControl : Panel
    {
        public PresetItem Item;
        public Label Name_Label;
        public TextBox Name_TextBox;

        private PictureBox DeletePreset_PictureBox;

        public PresetItemControl(PresetItem item, Point location, int width, int height)
        {
            Item = item;
            Location = location;
            Width = width;
            Height = height;
            BackColor = Color.FromArgb(22, 32, 45);

            Name_Label = new Label();
            Name_Label.Location = new Point(this.Width / 15, this.Height / 4);
            //nameLabel.Location = new Point(0, 0);
            Name_Label.AutoSize = true;
            Name_Label.Text = item.Name;
            Name_Label.Cursor = Cursors.IBeam;
            Name_Label.ForeColor = Color.Gray;
            Name_Label.BackColor = Color.Transparent;

            Name_TextBox = new TextBox();
            Name_TextBox.Location = new Point(this.Width / 15, this.Height / 4);
            Name_TextBox.Size = Name_Label.Size;
            Name_TextBox.Text = item.Name;
            Name_TextBox.Visible = false;

            DeletePreset_PictureBox = new PictureBox();
            DeletePreset_PictureBox.Size = new Size(this.Width / 10, this.Height);
            DeletePreset_PictureBox.Location = new Point(this.Width - DeletePreset_PictureBox.Width - 2, 0);
            DeletePreset_PictureBox.BorderStyle = BorderStyle.None;
            DeletePreset_PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            DeletePreset_PictureBox.Cursor = Cursors.Hand;
            DeletePreset_PictureBox.ImageLocation = Application.StartupPath + @"\resources\close_icon.png";

            this.Click += This_Click;
            Name_Label.Click += NameLabel_Click;
            DeletePreset_PictureBox.Click += DeletePresetPictureBox_Click;
            Name_TextBox.KeyDown += NameTextBox_KeyDown;

            this.Controls.Add(Name_TextBox);
            this.Controls.Add(Name_Label);
            this.Controls.Add(DeletePreset_PictureBox);
        }

        public delegate void RemovedEventHandler(object sender, EventArgs e);
        public delegate void NameEditedEventHandler(object oldName, object oldUrl, object targetItem, EventArgs e);

        public event RemovedEventHandler PresetItemRemoved;
        public event EventHandler NameEditActivated;
        public event NameEditedEventHandler NameEdited;

        protected virtual void OnPresetItemRemoved()
        {
            PresetItemRemoved?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnNameEditActivated()
        {
            NameEditActivated?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnNameEdited(string oldName, string oldPath, object targetItem)
        {
            NameEdited?.Invoke(oldName, oldPath, targetItem, EventArgs.Empty);
        }

        private void This_Click(object sender, EventArgs e)
        {
            ((this.Parent as Panel).Tag as PresetList).SelectItem(this);
        }

        private void NameLabel_Click(object sender, EventArgs e)
        {
            Name_TextBox.BringToFront();
            Name_TextBox.Visible = true;
            Name_TextBox.Focus();
            Name_TextBox.SelectionStart = Name_TextBox.Text.Length;

            OnNameEditActivated();
        }

        private void DeletePresetPictureBox_Click(object sender, EventArgs e)
        {
            OnPresetItemRemoved();
            this.Dispose();
        }

        private void NameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            string oldName = Name_Label.Text;
            string newName = Name_TextBox.Text;
            string oldPath = this.Item.FilePath;

            if (e.KeyCode == Keys.Enter)
            {
                Name_Label.Text = newName;
                Item.Name = newName;

                Name_TextBox.Visible = false;

                OnNameEdited(oldName, oldPath, this.Item);
            }

           
        }
    }
}
