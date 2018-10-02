using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;

namespace TeamBuilding.Tabs
{
    public partial class ProfileTab : UserControl
    {
        public TeamBuildingEntities TeamBuildingEntities = new TeamBuildingEntities();
        public ObservableCollection<Users> UsersList;
        public Users _user = null;

        public ProfileTab()
        {
            InitializeComponent();
        }

        public bool LoadUserData(Users user)
        {
            try
            {
                _user = user;
                var chosenOne = user;
                pictureBox1.Image = new Bitmap(@"Pictures\" + chosenOne.PicturePath);
                bunifuCustomLabel2.Text = chosenOne.Name;
                bunifuCustomLabel3.Text = "Joined: " + chosenOne.Registered;
                bunifuCustomLabel4.Text = "Your projects: " + chosenOne.Projects1.Count;

                try
                {
                    var userContacts = user.Contacts;
                    {
                        if (userContacts.PublicMail != null)
                            bunifuCustomLabel5.Text = "Contacts: Email: " + userContacts.PublicMail + "; ";
                        if (userContacts.Facebook != null)
                            bunifuCustomLabel5.Text += "Facebook: " + userContacts.Facebook + "; ";
                        if (userContacts.VKId != null)
                            bunifuCustomLabel5.Text += "VK: " + userContacts.VKId + "; ";
                        if (userContacts.Linkedin != null)
                            bunifuCustomLabel5.Text += "LinkedIn: " + userContacts.Linkedin + "; ";
                    }
                }

                catch (Exception)
                {
                    bunifuCustomLabel5.Text = "Contacts: none";
                }
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }

            return true;
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            ProjectTab tab=new ProjectTab();
            tab.Visible = true;
            tab.StartInfo();
            Controls.Add(tab);
            tab.Dock = DockStyle.Fill;
            tab.BringToFront();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            //_parent.
            SettingTab tab=new SettingTab(_user);
            tab.Visible = true;
            tab.StartInfo();
            Controls.Add(tab);
            tab.Dock = DockStyle.Fill;
            tab.BringToFront();
        }
    }
}