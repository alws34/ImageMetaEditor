using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageMetaEditor.UserControls
{

    public partial class DataFieldUC : UserControl
    {
        private ToolTip toolTip = new ToolTip();
        DateTime dt;
        public DataFieldUC(string field_name, string field_value, string tooltip = "")
        {
            InitializeComponent();
            SetTextBox(field_name, field_value, tooltip);
        }


        public DataFieldUC(string field_name, DateTime? date_tagged, string tooltip = "")
        {
            InitializeComponent();
            SetDateBox(field_name, date_tagged, tooltip);
        }

        private void SetTextBox(string field_name, string field_value, string tooltip = "")
        {
            label_field_name.Text = field_name;
            textBoxfieldText.Text = field_value;

            if (field_name != "Title")
            {
                this.textBoxfieldText.Multiline = true;
                this.textBoxfieldText.Size = new Size(300, 106);
                this.textBoxfieldText.Location = new Point(3, 27);
                this.Size = new Size(305, 140);
            }
            SetToolTip(textBoxfieldText, tooltip);
        }

        private void SetDateBox(string field_name, DateTime? date_tagged, string tooltip = "")
        {
            label_field_name.Text = field_name;
            if (field_name.Contains("Date"))
            {
                DateTimePicker dtp = new DateTimePicker();
                Size size = new Size(300, 31);
                Point location = new Point(3, 27);
                this.Controls.Remove(textBoxfieldText);
                if (date_tagged.HasValue)
                    dtp.Value = (DateTime)date_tagged;
                else
                    dtp.Value = DateTime.Now;
                dtp.Location = location;
                dtp.Size = size;
                dtp.Name = "DateTimePicker";
                dtp.ValueChanged += ChangeDateTime;
                toolTip.SetToolTip(dtp, tooltip);
                this.Controls.Add(dtp);
            }
        }

        private void ChangeDateTime(object sender, EventArgs e)
        {
            DateTimePicker? dtp = sender as DateTimePicker;
            if (dtp != null)
                dt = dtp.Value;
            else
                dt = DateTime.Now;
        }

        private void SetToolTip(Control control, string caption)
        {
            toolTip.SetToolTip(control, caption);
        }

        public string GetTitle()
        {
            return label_field_name.Text;
        }

        public object GetNewValues()
        {
            foreach (Control c in Controls)
            {
                if (c.Name == "TextBox" || c.Name == "textBoxfieldText")
                    return this.textBoxfieldText.Text;
                if (c.Name == "DateTimePicker")
                {
                    DateTimePicker? dtp = c as DateTimePicker;
                    if (dtp != null)
                        return dtp.Value;
                }
            }
            return null;
        }
    }
}
