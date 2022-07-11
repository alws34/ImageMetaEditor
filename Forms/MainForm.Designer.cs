namespace ImageMetaEditor.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageImages = new System.Windows.Forms.TabPage();
            this.panelImage = new System.Windows.Forms.Panel();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.treeView = new System.Windows.Forms.TreeView();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPageImages.SuspendLayout();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.tabPageAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageImages);
            this.tabControl1.Controls.Add(this.tabPageAbout);
            this.tabControl1.Location = new System.Drawing.Point(11, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1579, 771);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPageImages
            // 
            this.tabPageImages.Controls.Add(this.panelImage);
            this.tabPageImages.Controls.Add(this.progressBar);
            this.tabPageImages.Controls.Add(this.treeView);
            this.tabPageImages.Controls.Add(this.textBoxPath);
            this.tabPageImages.Controls.Add(this.btnBrowse);
            this.tabPageImages.Location = new System.Drawing.Point(4, 34);
            this.tabPageImages.Name = "tabPageImages";
            this.tabPageImages.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageImages.Size = new System.Drawing.Size(1571, 733);
            this.tabPageImages.TabIndex = 0;
            this.tabPageImages.Text = "Images";
            this.tabPageImages.UseVisualStyleBackColor = true;
            // 
            // panelImage
            // 
            this.panelImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelImage.Controls.Add(this.tableLayoutPanel);
            this.panelImage.Controls.Add(this.btnSaveData);
            this.panelImage.Controls.Add(this.pictureBox);
            this.panelImage.Location = new System.Drawing.Point(586, 7);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(981, 721);
            this.panelImage.TabIndex = 8;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Location = new System.Drawing.Point(11, 3);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(310, 644);
            this.tableLayoutPanel.TabIndex = 3;
            // 
            // btnSaveData
            // 
            this.btnSaveData.Location = new System.Drawing.Point(58, 662);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(211, 47);
            this.btnSaveData.TabIndex = 2;
            this.btnSaveData.Text = "Save Data to image";
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.Location = new System.Drawing.Point(329, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(649, 716);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(6, 43);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(573, 33);
            this.progressBar.TabIndex = 7;
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView.Location = new System.Drawing.Point(6, 83);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(573, 645);
            this.treeView.TabIndex = 6;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(6, 7);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(535, 31);
            this.textBoxPath.TabIndex = 5;
            this.textBoxPath.TextChanged += new System.EventHandler(this.textBoxPath_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(547, 7);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(31, 32);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.lblAuthor);
            this.tabPageAbout.Controls.Add(this.lblVersion);
            this.tabPageAbout.Controls.Add(this.richTextBox1);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 34);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAbout.Size = new System.Drawing.Size(1571, 733);
            this.tabPageAbout.TabIndex = 1;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAuthor.Location = new System.Drawing.Point(27, 95);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(112, 45);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "label1";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblVersion.Location = new System.Drawing.Point(27, 21);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(112, 45);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "label1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(27, 179);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(554, 539);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1598, 784);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Metadata Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPageImages.ResumeLayout(false);
            this.tabPageImages.PerformLayout();
            this.panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ToolTip toolTip;
        private TabControl tabControl1;
        private TabPage tabPageImages;
        private ProgressBar progressBar;
        private TreeView treeView;
        private TextBox textBoxPath;
        private Button btnBrowse;
        private TabPage tabPageAbout;
        private Panel panelImage;
        private PictureBox pictureBox;
        private Button btnSaveData;
        private TableLayoutPanel tableLayoutPanel;
        private RichTextBox richTextBox1;
        private Label lblAuthor;
        private Label lblVersion;
    }
}