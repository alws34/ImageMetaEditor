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

namespace ImageMetaEditor.Forms
{
    //V0.1
    public partial class MainForm : Form
    {
        private static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
        private List<string> images_path_lst = new List<string>();
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
            else
                MessageBox.Show("Select Directory!!");
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

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView? t = sender as TreeView;
            if (t == null)
                return;

            TreeNode tn = t.SelectedNode;
            string node_text = tn.Text;
            if (ImageExtensions.Contains(Path.GetExtension(node_text).ToUpperInvariant()))
                displayImage(textBoxPath.Text + "\\" + node_text);

        }

        private void displayImage(string image_path)
        {
            pictureBox.Image = Image.FromFile(image_path);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            //var tfile = TagLib.File.Create(image_path);
            //var data_tags = tfile.Tag;// as TagLib.Image.CombinedImageTag;
            //if (data_tags != null)
            //{

            //}
        }

        private void editImage(string image_path)
        {
            var tfile = TagLib.File.Create(image_path);
            var data_tags = tfile.Tag;// as TagLib.Image.CombinedImageTag;
            if (data_tags != null)
            {

            }
        }
    }
}
