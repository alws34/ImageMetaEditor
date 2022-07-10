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
        private List<string> images_path_lst = new List<string>();
        private TagLib.File image_file = TagLib.File.Create("sample_image.jpg");
        public MainForm()
        {
            InitializeComponent();
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
                    FileInfo fi = new FileInfo(file);
                    TreeNode tns = tn.Nodes.Add(fi.Name);
                    images_path_lst.Add(file);
                    tns.Tag = fi.FullName;
                    tns.StateImageIndex = 1;
                    UpdateProgress();
                }
            }
            //progressBar.Value = 0;
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

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView? tree_view = sender as TreeView;
            if (tree_view == null)
                return;

            TreeNode tree_node = tree_view.SelectedNode;
            string node_text = tree_node.Text;
            if (ImageExtensions.Contains(Path.GetExtension(node_text).ToUpperInvariant()))
                displayImage(textBoxPath.Text + "\\" + node_text);

        }

        private void displayImage(string image_path)
        {
            using (FileStream fs = new FileStream(image_path, FileMode.Open, FileAccess.Read))
            {
                pictureBox.Image = Image.FromStream(fs);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            image_file = TagLib.File.Create(image_path);
            CreateEditField(image_file);
        }
       
        private void CreateEditField(TagLib.File image_file)
        {
            string title = checkNull(image_file.Tag.Title);
            string description = checkNull(image_file.Tag.Description);
            string comment = checkNull(image_file.Tag.Comment);
            DateTime? date = checkNull(image_file.Tag.DateTagged);
            tableLayoutPanel.Controls.Clear();
            //tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute,65));//add more rows.
            tableLayoutPanel.Controls.Add(new DataFieldUC("Title", title, "The title is most commonly the name of the image/song/episode or movie."),1,1);
            tableLayoutPanel.Controls.Add(new DataFieldUC("Description", description,"A brief description of the object.\nThis is most common and usefull for movies"),1,2);
            tableLayoutPanel.Controls.Add(new DataFieldUC("Comments",comment, "This field should be used to store user notes and comments. There is no constraint on the text stored here."),1,3);
            tableLayoutPanel.Controls.Add(new DataFieldUC("Date Tagged", date, "the date at which the tag has beenwritten"), 1, 4);

            //image_file.Tag.DateTagged = DateTime.Now;
            //image_file.Save();
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
            image_file.Save();
        }
    }
}
