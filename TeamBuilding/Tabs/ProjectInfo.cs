using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;

namespace TeamBuilding.Tabs
{
    public partial class ProjectInfo : UserControl
    {
        public TeamBuildingEntities TeamBuildingEntities = new TeamBuildingEntities();
        public ObservableCollection<Users> UsersList;
        public Projects _project = null;
        public ProjectListTab _tab = null;

        public ProjectInfo()
        {
            InitializeComponent();
        }

        public bool LoadProjectData(Projects project, ProjectListTab tab)
        {
            try
            {
                _tab = tab;
                _project = project;
                picture.Image = new Bitmap(@"Pictures\" + _project.PrjtImagePath);
                NameBox.Text = project.PrjtName;
                CreatedBox.Text = "Created: " + project.PrjtCreated;
                DescriptionField.Text = _project.PrjtDescription;
                ClassList.Items.Clear();
                foreach (var Class in _project.PrjtClasses)
                {
                    ClassList.Items.Add(Class.Classes.ClassName);
                }
                SkillsList.Items.Clear();
                foreach (var skills in _project.Skills)
                {
                    SkillsList.Items.Add(skills.SklName);
                }
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }

            return true;
        }
        

        private void CloseButton_Click(object sender, EventArgs e)
        {
            _tab.Controls.Clear();
            _tab.ShowProjects();
            Visible = false;
        }
    }
}