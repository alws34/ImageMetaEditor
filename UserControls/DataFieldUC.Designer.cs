namespace ImageMetaEditor.UserControls
{
    partial class DataFieldUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxfieldText = new System.Windows.Forms.TextBox();
            this.label_field_name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxfieldText
            // 
            this.textBoxfieldText.Location = new System.Drawing.Point(3, 27);
            this.textBoxfieldText.Name = "textBoxfieldText";
            this.textBoxfieldText.Size = new System.Drawing.Size(304, 31);
            this.textBoxfieldText.TabIndex = 0;
            // 
            // label_field_name
            // 
            this.label_field_name.AutoSize = true;
            this.label_field_name.Location = new System.Drawing.Point(0, 0);
            this.label_field_name.Name = "label_field_name";
            this.label_field_name.Size = new System.Drawing.Size(59, 25);
            this.label_field_name.TabIndex = 1;
            this.label_field_name.Text = "label1";
            // 
            // DataFieldUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_field_name);
            this.Controls.Add(this.textBoxfieldText);
            this.Name = "DataFieldUC";
            this.Size = new System.Drawing.Size(310, 64);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxfieldText;
        private Label label_field_name;
    }
}
