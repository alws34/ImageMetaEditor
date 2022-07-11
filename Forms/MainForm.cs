using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TagLib;
using System.Media;
using ImageMetaEditor.UserControls;

namespace ImageMetaEditor.Forms
{
    //V0.1
    public partial class MainForm : Form
    {
        private static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
        private TagLib.File image_file = TagLib.File.Create("sample_image.jpg");
        private enum TABS
        {
            ImageTab,
            AboutTab
        };
        public MainForm()
        {
            InitializeComponent();
            tabControl1.SelectedIndexChanged += ResizeWindow;
            SetAbout();
        }
        private void SetAbout(string version = "0.6")
        {
            lblVersion.Text = $"Version: {version}";
            lblAuthor.Text = "Alon W";
        }

        public void LoadDirectory(string Dir)
        {
            DirectoryInfo di = new DirectoryInfo(Dir);
            //Setting ProgressBar Maximum Value  
            progressBar.Maximum = Directory.GetFiles(Dir, "*.*", SearchOption.AllDirectories).Length + Directory.GetDirectories(Dir, "**", SearchOption.AllDirectories).Length;
            TreeNode tds = treeView.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            LoadFiles(Dir, tds);
            LoadSubDirectories(Dir, tds);
        }

        private void LoadSubDirectories(string dir, TreeNode td)
        {
            // Get all subdirectories  
            string[] subdirectoryEntries = Directory.GetDirectories(dir);
            // Loop through them to see if they have any other subdirectories  
            foreach (string subdirectory in subdirectoryEntries)
            {
                DirectoryInfo di = new DirectoryInfo(subdirectory);
                TreeNode tds = td.Nodes.Add(di.Name);
                tds.StateImageIndex = 0;
                tds.Tag = di.FullName;
                LoadFiles(subdirectory, tds);
                LoadSubDirectories(subdirectory, tds);
                UpdateProgress();
            }
        }

        private void LoadFiles(string dir, TreeNode tn)
        {
            string[] Files = Directory.GetFiles(dir, "*.*");

            // Loop through them to see files  
            foreach (string file in Files)
            {
                if (ImageExtensions.Contains(Path.GetExtension(file).ToUpperInvariant()))
                {
                    FileInfo file_info = new FileInfo(file);
                    TreeNode tree_node = tn.Nodes.Add(file_info.Name);
                    tree_node.Tag = file_info.FullName;
                    tree_node.StateImageIndex = 1;
                    UpdateProgress();
                }
            }
            progressBar.Value = 0;
        }

        private void UpdateProgress()
        {
            if (progressBar.Value < progressBar.Maximum)
            {
                progressBar.Value++;
                int percent = (int)(((double)progressBar.Value / (double)progressBar.Maximum) * 100);
                progressBar.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(progressBar.Width / 2 - 10, progressBar.Height / 2 - 7));
                Application.DoEvents();
            }
        }

        private void displayImage(string image_path)
        {
            using (FileStream fs = new FileStream(image_path, FileMode.Open, FileAccess.Read))
            {
                pictureBox.Image = Image.FromStream(fs);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            image_file = TagLib.File.Create(image_path);
            imageFieldsHandler(image_file);
        }

        private void imageFieldsHandler(TagLib.File image_file)
        {
            //Handels all editable fields.
            tableLayoutPanel.Controls.Clear();

            string title = checkNull(image_file.Tag.Title);
            string description = checkNull(image_file.Tag.Description);
            string comment = checkNull(image_file.Tag.Comment);
            DateTime? date = checkNull(image_file.Tag.DateTagged);

            //tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute,65));//add more rows.
            DataFieldUC titleUC = new DataFieldUC("Title", title, "The title is most commonly the name of the image/song/episode or movie.");
            DataFieldUC descriptionUC = new DataFieldUC("Description", description, "A brief description of the object.\nThis is most common and usefull for movies");
            DataFieldUC commentUC = new DataFieldUC("Comments", comment, "This field should be used to store user notes and comments. There is no constraint on the text stored here.");
            DataFieldUC dateUC = new DataFieldUC("Date Tagged", date, "the date at which the tag has beenwritten");

            tableLayoutPanel.Controls.Add(titleUC, 1, 1);
            tableLayoutPanel.Controls.Add(descriptionUC, 1, 2);
            tableLayoutPanel.Controls.Add(commentUC, 1, 3);
            tableLayoutPanel.Controls.Add(dateUC, 1, 4);

        }

        private string checkNull(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";
            return str;
        }

        private DateTime? checkNull(DateTime? date)
        {
            if (!date.HasValue)
                return DateTime.Now;
            return date;
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                if (control.Name == "DataFieldUC")
                {
                    DataFieldUC? dfu = control as DataFieldUC;
                    if (dfu != null)
                    {
                        string control_title = dfu.GetTitle();
                        switch (control_title)
                        {
                            case "Title":
                                image_file.Tag.Title = (string)dfu.GetNewValues();
                                break;
                            case "Description":
                                image_file.Tag.Description = (string)dfu.GetNewValues();
                                break;
                            case "Comments":
                                image_file.Tag.Comment = (string)dfu.GetNewValues();
                                break;
                            case "Date Tagged":
                                image_file.Tag.DateTagged = (DateTime)dfu.GetNewValues();
                                break;
                        }
                    }
                }
            }
            image_file.Save();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView? tree_view = sender as TreeView;
            if (tree_view == null)
                return;

            TreeNode tree_node = tree_view.SelectedNode;
            string node_text = tree_node.Text;
            if (ImageExtensions.Contains(Path.GetExtension(node_text).ToUpperInvariant()))
                displayImage(tree_node.Tag.ToString());

        }

        private void ResizeWindow(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == (int)TABS.AboutTab)
                Size = new Size(650, 835);
            else
                Size = new Size(1620, 840);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fbd = new OpenFileDialog()
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder Selection."
            };

            if (fbd.ShowDialog() == DialogResult.OK)
                textBoxPath.Text = Path.GetDirectoryName(fbd.FileName);
        }

        private void textBoxPath_TextChanged(object sender, EventArgs e)
        {
            // Setting Inital Value of Progress Bar  
            progressBar.Value = 0;
            // Clear All Nodes if Already Exists  
            treeView.Nodes.Clear();
            toolTip.ShowAlways = true;
            if (textBoxPath.Text != "" && Directory.Exists(textBoxPath.Text))
                LoadDirectory(textBoxPath.Text);
        }

    }
}
