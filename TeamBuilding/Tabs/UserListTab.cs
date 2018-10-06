using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ns1;

namespace TeamBuilding.Tabs
{
    public partial class UserListTab : UserControl
    {
        public TeamBuildingEntities TeamBuildingEntities = Connection.Instance();
        public ObservableCollection<Users> UsersList;

        public int Counter;
        private bool Liked = false;

        public UserListTab()
        {
            InitializeComponent();
        }

        public void ShowUsers()
        {
            try
            {
                Counter = 0;
                UsersList = new ObservableCollection<Users>(TeamBuildingEntities.Users);
                Users chosenUser = null;
                var avatarY = 75;
                var NameY = 140;

                for (int i = 0; i < UsersList.Count; i++)
                {
                    BunifuThinButton2 thinButton = new BunifuThinButton2 { Name = "UserButton" + i };
                    chosenUser = UsersList[Counter];
                    thinButton.ButtonText = $"{chosenUser.Name} {chosenUser.LastName}";
                    thinButton.Size = new Size(655, 55);
                    thinButton.IdleLineColor = Color.White;
                    thinButton.IdleCornerRadius = 1;
                    thinButton.IdleForecolor = Color.Black;
                    thinButton.ActiveCornerRadius = 1;
                    thinButton.ActiveFillColor = Color.White;
                    thinButton.ActiveLineColor = Color.Black;
                    thinButton.ActiveForecolor = Color.FromArgb(12, 185, 102);
                    thinButton.TextAlign = ContentAlignment.MiddleLeft;
                    thinButton.Location = new Point(50, NameY);
                    thinButton.Tag = (object)UsersList[Counter];
                    NameY += 400;
                    thinButton.Click += GetProfileInfo;
                    thinButton.Font = new Font("Century Gothic", 12);
                    thinButton.Visible = true;

                    PictureBox pictureBox = new PictureBox();
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Size = new Size(100, 100);
                    pictureBox.Location = new Point(150, avatarY);
                    if (File.Exists(@"Pictures\" + chosenUser.PicturePath))
                        pictureBox.Image = new Bitmap(@"Pictures\" + chosenUser.PicturePath);
                    else pictureBox.Image = new Bitmap(@"Pictures\default.jpg");
                    pictureBox.Visible = true;
                    avatarY += 400;

                    Controls.Add(thinButton);
                    Controls.Add(pictureBox);
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
            ProfileTab tab = new ProfileTab();
            tab.Visible = true;
            tab.LoadInfo((Users)(sender as BunifuThinButton2).Tag, this);
            Controls.Clear();
            Controls.Add(tab);
            tab.Dock = DockStyle.Fill;
            tab.BringToFront();
        }
    }
}