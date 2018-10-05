﻿using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ns1;

namespace TeamBuilding.Tabs
{
    public partial class ProjectListTab : UserControl
    {
        public TeamBuildingEntities TeamBuildingEntities = new TeamBuildingEntities();
        public ObservableCollection<Projects> ProjectsList;

        public int Counter = 0;
        private bool Liked = false;

        public ProjectListTab()
        {
            InitializeComponent();
        }

        public void ShowProjects()
        {
            try
            {
                Counter = 0;
                ProjectsList = new ObservableCollection<Projects>(TeamBuildingEntities.Projects);
                var chosenProject = ProjectsList[0];
                var thinButtonY = 325;
                var pictureBoxY = 75;
                var separatorY = 375;
                var customLabelY = 400;
                var likeButtonY = 395;

                for (int i = 0; i < ProjectsList.Count; i++)
                {
                    BunifuThinButton2 thinButton = new BunifuThinButton2 { Name = "thinButton" + i };
                    chosenProject = ProjectsList[Counter];
                    thinButton.ButtonText = chosenProject.PrjtName;
                    thinButton.Size = new Size(655, 55);
                    thinButton.IdleLineColor = Color.White;
                    thinButton.IdleCornerRadius = 1;
                    thinButton.IdleForecolor = Color.Black;
                    thinButton.ActiveCornerRadius = 1;
                    thinButton.ActiveFillColor = Color.White;
                    thinButton.ActiveLineColor = Color.Black;
                    thinButton.ActiveForecolor = Color.FromArgb(12, 185, 102);
                    thinButton.TextAlign = ContentAlignment.MiddleLeft;
                    thinButton.Location = new Point(50, thinButtonY);
                    thinButton.Tag =(object)ProjectsList[Counter];
                    thinButtonY += 400;
                    thinButton.Click += GetProfileInfo;
                    thinButton.Font = new Font("Century Gothic", 12);

                    PictureBox pictureBox = new PictureBox();
                    pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox.Size = new Size(450, 250);
                    pictureBox.Location = new Point(150, pictureBoxY);
                    if (File.Exists(@"Pictures\" + chosenProject.PrjtImagePath))
                    pictureBox.Image = new Bitmap(@"Pictures\" + chosenProject.PrjtImagePath);
                    else pictureBox.Image = new Bitmap(@"Pictures\default.jpg");
                    pictureBoxY += 400;

                    BunifuSeparator separator = new BunifuSeparator();
                    separator.Size = new Size(655, 15);
                    separator.LineColor = Color.FromArgb(12, 185, 102);
                    separator.LineThickness = 3;
                    separator.Location = new Point(50, separatorY);
                    separatorY += 400;

                    BunifuCustomLabel customLabel = new BunifuCustomLabel();
                    customLabel.Font = new Font("Century Gothic", 12);
                    customLabel.Text = "Likes: " + chosenProject.PjrtLikeCounter;
                    customLabel.Location = new Point(50, customLabelY);
                    customLabelY += 400;

                    BunifuImageButton likeButton = new BunifuImageButton() { Name = "imageButton" + i };
                    likeButton.Zoom = 15;
                    likeButton.Size = new Size(30, 30);
                    likeButton.BackColor = Color.Transparent;
                    likeButton.SizeMode = PictureBoxSizeMode.StretchImage;
                    likeButton.Image = NotLikedProject.Image;
                    likeButton.Location = new Point(675, likeButtonY);
                    likeButtonY += 400;
                    likeButton.Click += new EventHandler(Like_project);

                    Controls.Add(thinButton);
                    Controls.Add(pictureBox);
                    Controls.Add(separator);
                    Controls.Add(customLabel);
                    Controls.Add(likeButton);
                    ++Counter;
                }
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void GetProfileInfo(object sender, object e)
        {
            ProjectInfo tab = new ProjectInfo();
            tab.Visible = true;
            tab.LoadInfo((Projects)(sender as BunifuThinButton2).Tag,this);
            Controls.Clear();
            Controls.Add(tab);
            tab.Dock = DockStyle.Fill;
            tab.BringToFront();
        }

        private void Like_project(object sender, EventArgs e)
        {
            BunifuImageButton button = sender as BunifuImageButton;

            if (!Liked)
            {
                button.Image = LikedProject.Image;
                Liked = true;
            }
            else
            {
                button.Image = NotLikedProject.Image;
                Liked = false;
            }
        }
    }
}