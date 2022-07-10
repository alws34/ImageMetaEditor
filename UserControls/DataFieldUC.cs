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
        DateTimePicker dtp = new DateTimePicker();
        public DataFieldUC(string field_name, string field_value, string tooltip = "")
        {
            InitializeComponent();
            setTitle(this.label_field_name, field_name);
            textBoxfieldText.Text = field_value;

            SetToolTip(textBoxfieldText, tooltip);

            if (field_name != "Title")
            {
                this.textBoxfieldText.Multiline = true;
                this.textBoxfieldText.Size = new Size(233, 106);
                this.textBoxfieldText.Location = new Point(3, 27);
                this.Size = new Size(242, 140);
            }

        }
        public DataFieldUC(string field_name, DateTime? date_tagged, string tooltip = "")
        {
            InitializeComponent();
            setTitle(this.label_field_name, field_name);
            if (field_name.Contains("Date"))
            {
                Size size = new Size(233, 31);
                Point location = new Point(3, 27);
                this.Controls.Remove(textBoxfieldText);
                if (date_tagged.HasValue)
                    dtp.Value = (DateTime)date_tagged;
                else
                    dtp.Value = DateTime.Now;
                dtp.Location = location;
                dtp.Size = size;
                this.Controls.Add(dtp);
            }
        }

        private void SetToolTip(Control control, string caption)
        {
            toolTip.SetToolTip(control, caption);
        }

        private void setTitle(Control c, string title)
        {
            c.Text = title;
        }

        public string GetNewValue()
        {
            return this.textBoxfieldText.Text;
        }
        public DateTime GetNewTagDate()
        {
            return this.dtp.Value;
        }
    }
}
