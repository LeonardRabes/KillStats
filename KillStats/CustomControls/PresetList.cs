using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KillStats
{
    public class PresetList : Button
    {
        private PictureBox AddPreset_PictureBox;
        private List<PresetItemControl> Item_Controls;
        private PresetItemControl ActiveItem;
        private int ListItemLocationY;

        public Panel PresetPanel { get; set; }
        public ItemCollection<PresetItem> Items { get; set; }
        public int ListItemHeight { get; set; }

        public PresetList(Point location, int width, int height) //constructor
        {
            Items = new ItemCollection<PresetItem>();
            Item_Controls = new List<PresetItemControl>();

            this.Width = width;
            this.Height = height;
            this.Location = location;
            this.Text = "Presets";
            this.UseVisualStyleBackColor = true;
            this.ListItemLocationY = Height / 2 + 6;
            this.ListItemHeight = 30;

            PresetPanel = new Panel();
            PresetPanel.Width = this.Width * 2;
            PresetPanel.Height = this.Height / 2 + 6;
            PresetPanel.Location = new Point(this.Location.X - this.Width, this.Location.Y + this.Height);
            PresetPanel.BackColor = Color.FromArgb(27, 40, 56);
            PresetPanel.BorderStyle = BorderStyle.FixedSingle;
            PresetPanel.Visible = false;
            PresetPanel.Tag = this;

            AddPreset_PictureBox = new PictureBox();
            AddPreset_PictureBox.Width = width / 3;
            AddPreset_PictureBox.Height = height / 2;
            AddPreset_PictureBox.Location = new Point(1, 2);
            AddPreset_PictureBox.ImageLocation = Application.StartupPath + @"\resources\add_icon_small.png";
            AddPreset_PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            AddPreset_PictureBox.Cursor = Cursors.Hand;
            PresetPanel.Controls.Add(AddPreset_PictureBox);

            this.Items.ItemAdded += Items_ItemAdded;
            this.Click += PresetList_Click;
            this.ParentChanged += PresetList_ParentChanged;
            this.PresetPanel.MouseLeave += PresetPanel_MouseLeave;
            this.AddPreset_PictureBox.Click += AddPresetButton_Click;
        }

        public void SelectItem(PresetItemControl item)
        {
            if(ActiveItem != null)
                ActiveItem.BackColor = Color.FromArgb(22, 32, 45);

            item.BackColor = Color.FromArgb(23, 26, 33);
            ActiveItem = item;

            OnItemSelected(item.Item);
        }
        public void SelectItem(PresetItem item)
        {
            if (ActiveItem != null)
                ActiveItem.BackColor = Color.FromArgb(22, 32, 45);

            foreach(PresetItemControl control in Item_Controls)
            {
                if(control.Item == item)
                {
                    control.BackColor = Color.FromArgb(23, 26, 33);
                    ActiveItem = control;
                } 
            }
            OnItemSelected(item);
        }

        public delegate void ItemRemovedHandler(object removedItem, EventArgs e);
        public delegate void ItemAddedHandler(object addedItem, EventArgs e);
        public delegate void ItemSelectedHandler(object selectedItem, EventArgs e);
        public delegate void ItemNameEditedHandler(object oldName, object oldPath, object targetItem, EventArgs e);

        public event ItemRemovedHandler ItemRemoved;
        public event ItemAddedHandler ItemAdded;
        public event ItemSelectedHandler ItemSelected;
        public event ItemNameEditedHandler ItemNameEdited;

        protected virtual void OnItemRemoved(object targetItem)
        {
            ItemRemoved?.Invoke(targetItem, EventArgs.Empty);
        }
        protected virtual void OnItemAdded(object targetItem)
        {
            ItemAdded?.Invoke(targetItem, EventArgs.Empty);
        }
        protected virtual void OnItemSelected(object targetItem)
        {
            ItemSelected?.Invoke(targetItem, EventArgs.Empty);
        }
        protected virtual void OnItemNameEdited(object oldName, object oldPath, object targetItem)
        {
            ItemNameEdited?.Invoke(oldName, oldPath, targetItem, EventArgs.Empty);
        }

        private void AddPresetButton_Click(object sender, EventArgs e)
        {
            if(Items.Count < 9)
            {
                PresetItem item = new PresetItem("Preset" + (Items.Count + 1), Application.StartupPath + @"\config\presets\Preset" + (Items.Count + 1) + ".json");
                Items.Add(item);
                OnItemAdded(item);
            }  
        }

        private void Items_ItemAdded(object sender, object newItem, EventArgs e)
        {
            if (newItem is PresetItem)
            {
                PresetItem item = newItem as PresetItem;
                PresetItemControl itemControl = new PresetItemControl(item, new Point(2, ListItemLocationY), PresetPanel.Width - 4, ListItemHeight);

                itemControl.PresetItemRemoved += PresetControl_Removed;
                itemControl.NameEditActivated += PresetControl_NameEditActivated;
                itemControl.NameEdited += PresetControl_NameEdited;

                PresetPanel.Controls.Add(itemControl);
                Item_Controls.Add(itemControl);

                ListItemLocationY += ListItemHeight + 2;
                PresetPanel.Height += ListItemHeight + 2;
            }

            else if (newItem is PresetItem[])
            {
                List<PresetItem> items = new List<PresetItem>();
                items.AddRange(newItem as PresetItem[]);
                foreach(PresetItem item in items)
                {
                    PresetItemControl itemControl = new PresetItemControl(item, new Point(2, ListItemLocationY), PresetPanel.Width - 4, ListItemHeight);

                    itemControl.PresetItemRemoved += PresetControl_Removed;
                    itemControl.NameEditActivated += PresetControl_NameEditActivated;
                    itemControl.NameEdited += PresetControl_NameEdited;

                    PresetPanel.Controls.Add(itemControl);
                    Item_Controls.Add(itemControl);

                    ListItemLocationY += ListItemHeight + 2;
                    PresetPanel.Height += ListItemHeight + 2;
                    if (items.IndexOf(item) == 0 && ActiveItem == null) { SelectItem(itemControl); }
                }
            }
        }

        private void PresetList_ParentChanged(object sender, EventArgs e)
        {
            this.Name = "PresetList" + ((Parent as Control).Controls.Count + 1);

            (Parent as Control).Controls.Add(PresetPanel);
        }

        private void PresetList_Click(object sender, EventArgs e)
        {
            PresetPanel.BringToFront();
            PresetPanel.Visible = !PresetPanel.Visible;
        }

        private void PresetPanel_MouseLeave(object sender, EventArgs e)
        {
            Point mouseLocation = (Parent as Control).PointToClient(Cursor.Position);

            if (mouseLocation.X >= PresetPanel.Location.X + PresetPanel.Width || mouseLocation.X <= PresetPanel.Location.X ||
               mouseLocation.Y >= PresetPanel.Location.Y + PresetPanel.Height || mouseLocation.Y <= PresetPanel.Location.Y)
            {
                //PresetPanel.Visible = false;
            }

        }

        private void PresetControl_Removed(object sender, EventArgs e)
        {
            PresetItemControl itemControl = sender as PresetItemControl;

            Item_Controls.Remove(itemControl);
            Items.Remove(itemControl.Item);
            PresetPanel.Height -= itemControl.Height + 2;
            this.ListItemLocationY = Height / 2 + 6;

            foreach(PresetItemControl control in Item_Controls)
            {
                control.Location = new Point(0, ListItemLocationY);
                this.ListItemLocationY += ListItemHeight + 2;
            }

            if(ActiveItem == itemControl && Item_Controls.Count > 0)
            {
                SelectItem(Item_Controls[0]);
            }

            OnItemRemoved(itemControl.Item);
        }

        private void PresetControl_NameEditActivated(object sender, EventArgs e)
        {
            SelectItem(sender as PresetItemControl);
            foreach(PresetItemControl ctrl in Item_Controls)
            {
                if(sender as PresetItemControl != ctrl)
                {
                    ctrl.Name_TextBox.Visible = false;
                }
            }  
        }

        private void PresetControl_NameEdited(object oldName, object oldPath, object targetItem, EventArgs e)
        {
            OnItemNameEdited(oldName, oldPath, targetItem);
        }
    }
}
