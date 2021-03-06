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
using ImageMetaEditor.Classes;


namespace ImageMetaEditor.Forms
{
    //V0.1
    public partial class MainForm : Form
    {
        private static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
        private static readonly int NUM_OF_ROWS = 6; //change this to control amount of DataFieldUC
        private TagLib.File current_image = TagLib.File.Create("sample_image.jpg");
        private DataFieldUC personUC;


        private bool isMouseDown = false;
        public List<CustomRectangle> rectangles_lst = new List<CustomRectangle>();
        private List<int> rectangles_ids = new List<int>();
        Graphics g;
        Rectangle rect = new Rectangle();
        Point location_start_position;
        Point location_end_position;
        Pen current_pen = new Pen(Color.White, 2.5f);
        private enum TABS
        {
            ImageTab,
            AboutTab
        };

        public MainForm()
        {
            InitializeComponent();
            SetAbout();
            tabControl.SelectedIndexChanged += ResizeWindow;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            btnColorPicker.BackColor = current_pen.Color;
            //pictureBox.Enabled = false;
        }

        private void SetAbout(string version = "0.6.1")
        {
            lblVersion.Text = $"Version:\t{version}";
            lblAuthor.Text = "Author:\talws34";
            richTextBox1.Text = $"";
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
            progressBar.Value = 0;
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
            rectangles_lst.Clear();

            using (FileStream fs = new FileStream(image_path, FileMode.Open, FileAccess.Read))
                pictureBox.Image = Image.FromStream(fs);

            current_image = TagLib.File.Create(image_path);
            imageFieldsHandler(current_image);
        }

        private void imageFieldsHandler(TagLib.File image_file)
        {
            //Handels all editable fields.
            tableLayoutPanel.Controls.Clear();
            SetFields();
        }

        private void SetFields()
        {
            //pictureBox.Enabled = true;

            for (int i = 0; i < NUM_OF_ROWS; i++)
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 33));


            string filename = checkNull(Path.GetFileNameWithoutExtension(current_image.Name));
            string title = checkNull(current_image.Tag.Title);
            string description = checkNull(current_image.Tag.Description);
            string comment = checkNull(current_image.Tag.Comment);
            DateTime? date = checkNull(current_image.Tag.DateTagged);
            personUC = new DataFieldUC("Person", "Enter person's name");

            DataFieldUC[] dfu_list =
            {
                new DataFieldUC("File Name", filename, current_image.Name, "you cna change the file name"),
                new DataFieldUC("Title", title, "The title is most commonly the name of the image/song/episode or movie."),
                new DataFieldUC("Description", description, "A brief description of the object.\nThis is most common and usefull for movies"),
                new DataFieldUC("Comments", comment, "This field should be used to store user notes and comments. There is no constraint on the text stored here."),
                new DataFieldUC("Date Tagged", date, "the date at which the tag has beenwritten"),
                personUC
            };

            for (int i = 0; i < dfu_list.Length; i++)
                tableLayoutPanel.Controls.Add(dfu_list[i], 1, i + 1);
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
            SetNewFields();
        }

        private void SetNewFields()
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                DataFieldUC? dfu = control as DataFieldUC;
                if (dfu != null)
                {
                    string control_title = dfu.GetName();
                    switch (control_title)
                    {
                        //case "File Name":
                        //    RenameFile((string)dfu.GetNewValues(), dfu.GetTag().ToString());
                        //    break;
                        case "Title":
                            string title = (string)dfu.GetNewValues();
                            current_image.Tag.Title = title;
                            break;
                        case "Description":
                            string description = (string)dfu.GetNewValues();
                            current_image.Tag.Description = description;
                            break;
                        case "Comments":
                            string comment = (string)dfu.GetNewValues();
                            current_image.Tag.Comment = comment;
                            break;
                        case "Date Tagged":
                            current_image.Tag.DateTagged = (DateTime)dfu.GetNewValues();
                            break;
                        case "Person":

                            break;
                    }
                }
            }
            try
            {
                current_image.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RenameFile(string new_file_name, string file_full_path)
        {
            string path = Path.GetDirectoryName(file_full_path);
            string old_file_name = Path.GetFileNameWithoutExtension(file_full_path);
            string extension = Path.GetExtension(file_full_path).ToUpper();

            if (old_file_name != new_file_name)
            {
                if (ImageExtensions.Contains(extension))
                {
                    FileInfo fileInfo = new FileInfo(file_full_path);
                    string new_file = $"{path}\\{new_file_name}{extension.ToLower()}";
                    string copy = "";
                    while (System.IO.File.Exists(new_file))
                    {
                        copy += "(_copy_)";
                        new_file = $"{path}\\{new_file_name}{copy}{extension.ToLower()}";
                    }


                    fileInfo.CopyTo(new_file); //MoveTo 
                    current_image = TagLib.File.Create(new_file);
                    //if (System.IO.File.Exists(new_file))
                    //    System.IO.File.Delete(file_full_path);
                }
            }
        }

        private Rectangle GetCurrentDrawnRec()
        {
            rect = new Rectangle();
            rect.X = Math.Min(location_start_position.X, location_end_position.X);
            rect.Y = Math.Min(location_start_position.Y, location_end_position.Y);
            rect.Width = Math.Abs(location_start_position.X - location_end_position.X);
            rect.Height = Math.Abs(location_start_position.Y - location_end_position.Y);

            return rect;
        }

        private int GetRectangleID()
        {
            Random rand = new Random();
            int id = rand.Next(int.MaxValue);
            while (rectangles_ids.Contains(id))//avoid duplicates
                id = rand.Next(int.MaxValue);
            rectangles_ids.Add(id);
            return id;
        }
        
        private Color GenerateRandomColor()
        {
            Random r = new Random();
            return Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
        }
      
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView? tree_view = sender as TreeView;
            if (tree_view == null)
                return;

            TreeNode tree_node = tree_view.SelectedNode;
            string node_text = tree_node.Text;
            if (ImageExtensions.Contains(Path.GetExtension(node_text).ToUpperInvariant()))
                displayImage((string)tree_node.Tag);

        }

        private void ResizeWindow(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == (int)TABS.AboutTab)
                Size = new Size(650, 840);
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            isMouseDown = true;
            location_start_position = e.Location;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                personUC.SetBackgroundColor(Color.White);
                location_end_position = e.Location;
                pictureBox.Refresh();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            location_end_position = e.Location;
            CustomRectangle m = new CustomRectangle(GetCurrentDrawnRec(), GetRectangleID(), new Pen(current_pen.Color, 2f));
            rectangles_lst.Add(m);
            //pictureBox.Enabled = false;
            Color c = GenerateRandomColor();
            current_pen.Color = c;
            btnColorPicker.BackColor = c;
            personUC.SetBackgroundColor(Color.Yellow);

        }


        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = GetCurrentDrawnRec(); //get dynamically drawn rectangle
            e.Graphics.DrawRectangle(current_pen, r); // draw it

            foreach (CustomRectangle rec in rectangles_lst) //redraw all rectangles at their position
                e.Graphics.DrawRectangle(rec.Pen, rec.Rec);
            //richTextBox1.Text = rec.ToString().Replace("[-]","");
        }


        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            colorPickDialog.ShowDialog();
            current_pen.Color = colorPickDialog.Color;
            btnColorPicker.BackColor = colorPickDialog.Color;
        }


        private void btnUndo_Click(object sender, EventArgs e)
        {

        }
    }
}
