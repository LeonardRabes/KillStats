﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace KillStats
{
    class ItemView : Panel
    {
        private static List<ItemView> existingItemViews;
        public static Action<ItemView> OnRemove;
        public static Func<int, Color> GetItemColor;

        public static List<ItemView> CreateDynamicViews(List<string[]> ItemProperties, Point Location, int Height, int Width, Form sender)
        {
            List<ItemView> itemViewList = new List<ItemView>();
            Point location = Location;

            foreach(string[] itemProp in ItemProperties)
            {
                ItemView itemView = new ItemView(itemProp[0], itemProp[1], itemProp[2], itemProp[3], location, Width, Height, itemViewList.Count);
                itemView.WeaponID = itemProp[4];
                itemView.WeaponPart = Convert.ToByte(itemProp[5]);
                itemView.TabIndex = sender.Controls.Count + 1;
                itemView.BackColor = Color.FromArgb(22, 32, 45);
                itemViewList.Add(itemView);
                sender.Controls.Add(itemView);

                location.Y += Height + 5;
                sender.Height += Height + 5;
            }

            existingItemViews = itemViewList;
            return itemViewList;
        }

        public static void RemoveView(ItemView targetView)
        {
            Point startinglocation = existingItemViews[0].Location;
            
            for (int i = existingItemViews.IndexOf(targetView); i < existingItemViews.Count; i++)
            {
                existingItemViews[i].Location = new Point(existingItemViews[i].Location.X, existingItemViews[i].Location.Y - targetView.Height - 5);
            }
            OnRemove(targetView);
            targetView.Dispose();
        }

        public uint StrangePoints { get; set; }
        public int TotalPoints { get; set; }
        public float AveragePoints { get; set; }
        public string WeaponID   { get; set; }
        public byte WeaponPart { get; set; }
        public string ItemName   { get; set; }
        public string ItemImageUrl { get; set; }
        public Color ItemNameColor { get; set; }
        public Color ItemBackColor { get; set; }
        public Color ItemBorderColor { get; set; }

        public PictureBox ItemImage { get; set; }
        public Panel ItemImageOverlay { get; set; }
        public Panel ItemImageBorder    { get; set; }
        public Label ItemNameLabel  { get; set; }
        public Label ItemTotalLabel { get; set; }
        public Label ItemAverageLabel { get; set; }

        public ItemView()
        {
            ItemImage = new PictureBox();
            ItemImage.ImageLocation = "http://community.edgecast.steamstatic.com/economy/image/" + ItemImageUrl;
            ItemImage.SizeMode = PictureBoxSizeMode.Zoom;
            ItemImage.Height = (Height * 90) / 100;
            ItemImage.Width = ItemImage.Height;
            ItemImage.Location = new Point(Height * 5 / 100, Height * 6 / 100);
            ItemImage.BorderStyle = BorderStyle.None;
            ItemImage.BackColor = ItemBackColor;

            ItemNameLabel = new Label();
            ItemNameLabel.Location = new Point(Width * 18 / 100, Height * 20 / 100);
            ItemNameLabel.Text = ItemName + ": Loading";
            ItemNameLabel.Font = new Font("Motiva Sans", 10F, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            ItemNameLabel.AutoSize = true;
            ItemNameLabel.ForeColor = ItemNameColor;

            this.Controls.Add(ItemImage);
            this.Controls.Add(ItemNameLabel);
        }

        public ItemView(string itemname, string itemname_color, string itemimage_url, string itemback_color, Point location, int width, int height, int count)
        {
            this.Location = location;
            this.ItemName = itemname;
            for(int i = 0; i <= 5; i++)
            {
                if(ItemName.Length > 30)
                {
                    if (i == 0)
                        ItemName = ItemName.Replace("Strange ", "S. ");
                    if (i == 1)
                        ItemName = ItemName.Replace("Professional ", "Prof. ");
                    if (i == 1)
                        ItemName = ItemName.Replace("Specialized ", "Spec. ");
                    if (i == 2)
                        ItemName = ItemName.Replace("Killstreak ", "Ks. ");
                    if (i == 3)
                        ItemName = ItemName.Replace("Festive ", "F. ");
                    if (i == 3)
                        ItemName = ItemName.Replace("Botkiller ", "Botk. ");
                    if (i == 4)
                        ItemName = ItemName.Replace("Carbonado ", "Carb. ");
                    if (i == 5)
                        ItemName = ItemName.Substring(0, 30) + "...";
                }
            }
            this.Name = this.ItemName + "_itemView";
            this.Size = new System.Drawing.Size(width, height);
            this.ItemImageUrl = itemimage_url;
            ColorConverter colorConverter = new ColorConverter();
            this.ItemNameColor = (Color)colorConverter.ConvertFromString("#" + itemname_color);
            this.ItemBackColor = (Color)colorConverter.ConvertFromString("#" + itemback_color);

            ItemImage = new PictureBox();
            ItemImage.ImageLocation = "http://community.edgecast.steamstatic.com/economy/image/" + ItemImageUrl;
            ItemImage.SizeMode = PictureBoxSizeMode.Zoom;
            ItemImage.Height = (Height * 90) / 100;
            ItemImage.Width = ItemImage.Height;
            ItemImage.Location = new Point(Height * 6 / 100, Height * 6 / 100);
            ItemImage.BorderStyle = BorderStyle.None;
            ItemImage.BackColor = ItemBackColor;

            ItemImageBorder = new Panel();
            ItemImageBorder.Location = new Point(0, 0);
            ItemImageBorder.Height = Height;
            ItemImageBorder.Width = Height;
            Random rnd = new Random();
            ItemBorderColor = GetItemColor(count);
            ItemImageBorder.BackColor = ItemBorderColor;;

            ItemImageOverlay = new Panel();
            ItemImageOverlay.Location = ItemImage.Location;
            ItemImageOverlay.Size = ItemImage.Size;
            ItemImageOverlay.BackColor = Color.FromArgb(100, ItemBorderColor);
            Label ItemImageOverlayLabel = new Label();
            ItemImageOverlayLabel.Location = new Point(0, ItemImage.Height / 3);
            ItemImageOverlayLabel.AutoSize = true;
            ItemImageOverlayLabel.Text = "  Click to \n  Remove";
            ItemImageOverlayLabel.TextAlign = ContentAlignment.MiddleLeft;
            ItemImageOverlayLabel.Font = new Font("Motiva Sans", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            ItemImageOverlayLabel.ForeColor = Color.FromArgb(ItemBorderColor.ToArgb()^0xffffff);
            ItemImageOverlayLabel.BackColor = Color.Transparent;
            ItemImageOverlay.Controls.Add(ItemImageOverlayLabel);

            this.ItemImage.MouseEnter += new EventHandler(this.ItemImage_OnMouseEnter);
            this.ItemImageOverlay.MouseLeave += new EventHandler(this.ItemImageOverlay_OnMouseLeave);
            this.ItemImageOverlay.MouseClick += new MouseEventHandler(this.ItemImageOverlay_OnMouseClick);
            ItemImageOverlayLabel.MouseClick += new MouseEventHandler(this.ItemImageOverlay_OnMouseClick);


            ItemNameLabel = new Label();
            ItemNameLabel.Location = new Point(Width * 19 / 100, Height * 20 / 100);
            ItemNameLabel.Text = ItemName + ": " + StrangePoints;
            ItemNameLabel.Font = new Font("Motiva Sans", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            ItemNameLabel.AutoSize = true;
            ItemNameLabel.ForeColor = ItemNameColor;


            ItemTotalLabel = new Label();
            ItemTotalLabel.Location = new Point(Width * 21 / 100, Height * 55 / 100);
            ItemTotalLabel.Text = "Total last hour: 0";
            ItemTotalLabel.Font = new Font("Motiva Sans", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            ItemTotalLabel.AutoSize = true;
            ItemTotalLabel.ForeColor = ItemNameColor;


            ItemAverageLabel = new Label();
            ItemAverageLabel.Location = new Point(Width * 21 / 100, Height * 75 / 100);
            ItemAverageLabel.Text = "Points Avg: 0";
            ItemAverageLabel.Font = new Font("Motiva Sans", 8F, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            ItemAverageLabel.AutoSize = true;
            ItemAverageLabel.ForeColor = ItemNameColor;

            Thread.Sleep(15);

            this.Controls.Add(ItemImage);
            this.Controls.Add(ItemImageBorder);
            this.Controls.Add(ItemImageOverlay);
            this.Controls.Add(ItemNameLabel);
            this.Controls.Add(ItemTotalLabel);
            this.Controls.Add(ItemAverageLabel);
        }

        private void ItemImage_OnMouseEnter(object sender, EventArgs e)
        {
            this.ItemImageOverlay.BringToFront();
        }

        private void ItemImageOverlay_OnMouseLeave(object sender, EventArgs e)
        {
            this.ItemImageOverlay.SendToBack();
        }

        private void ItemImageOverlay_OnMouseClick(object sender, MouseEventArgs e)
        {
            RemoveView(this);
        }
    }
}
