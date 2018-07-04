using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace KillStats
{
    public class ItemViewList
    {
        private object Sender;
        public List<ItemView> ItemViews { get; set; }
        public Func<int, Color> GetItemColor;

        public ItemViewList(object sender)
        {
            ItemViews = new List<ItemView>();
            Sender = sender;
        }

        //Event OnItemViewRemoved
        public delegate void OnItemViewRemoveEventHandler(ItemView targetView, EventArgs e);
        public event OnItemViewRemoveEventHandler ItemViewRemoved;
        protected virtual void OnItemViewRemoved(ItemView targetView)
        {
            ItemViewRemoved?.Invoke(targetView, EventArgs.Empty);
        }

        public int CreateViews(List<string[]> ItemProperties, Point Location, int Height, int Width)
        {
            Form sender = Sender as Form;
            List<ItemView> itemViewList = new List<ItemView>();
            Point location = Location;
            int height = 0;

            foreach (string[] itemProp in ItemProperties)
            {
                ItemView itemView = new ItemView(itemProp[0], itemProp[1], itemProp[2], itemProp[3], location, Width, Height, itemViewList.Count);
                itemView.WeaponID = itemProp[4];
                itemView.WeaponPart = Convert.ToByte(itemProp[5]);
                itemView.TabIndex = sender.Controls.Count + 1;
                itemView.BackColor = Color.FromArgb(22, 32, 45);
                itemView.ItemViewList = this;

                itemViewList.Add(itemView);
                sender.Controls.Add(itemView);

                location.Y += Height + 5;
                height += Height + 5;
            }
            SyncColors(itemViewList);
            ItemViews = itemViewList;
            return height;
        }

        public void AddView(ItemView newView)
        {
            Form sender = Sender as Form;

            if (ItemViews.Count - 1 != -1)
            {
                newView.Location = ItemViews[ItemViews.Count - 1].Location;
                newView.Location = new Point(newView.Location.X, ItemViews[ItemViews.Count - 1].Location.Y + newView.Height + 5);
            }
            newView.ItemViewList = this;

            sender.Height += newView.Height + 5;
            sender.Controls.Add(newView);
            ItemViews.Add(newView);
            SyncColors(ItemViews);
        }

        public void RemoveView(ItemView targetView)
        {
            targetView.Dispose();

            Point startinglocation = ItemViews[0].Location;

            for (int i = ItemViews.IndexOf(targetView); i < ItemViews.Count; i++)
            {
                ItemViews[i].Location = new Point(ItemViews[i].Location.X, ItemViews[i].Location.Y - targetView.Height - 5);
            }

            ItemViews.Remove(targetView);
            SyncColors(ItemViews);
            OnItemViewRemoved(targetView);
        }

        private void SyncColors(List<ItemView> itemViews)
        {
            if(itemViews.Count != 0)
                for(int i = 0; i < itemViews.Count; i++)
                {
                    itemViews[i].ItemImageBorder.BackColor = GetItemColor(i);
                    itemViews[i].ItemImageOverlay.BackColor = Color.FromArgb(100, GetItemColor(i));
                    itemViews[i].ItemImageOverlay.Controls[0].ForeColor = Color.FromArgb(GetItemColor(i).ToArgb() ^ 0xffffff);
                    if(itemViews[i].ItemSeries != null)
                        itemViews[i].ItemSeries.Color = GetItemColor(i);
                }
        }
    }
}
