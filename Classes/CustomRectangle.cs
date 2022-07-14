using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMetaEditor.Classes
{
    public class CustomRectangle
    {
        private Rectangle rectangle = new Rectangle();
        Pen pen;
        int id;//rectangle_id
        string person; // who is this person?
        string image_full_path; //this connect a picture to a rectangle
        string tooltip_txt;

        public CustomRectangle(Rectangle rec, int id, Pen pen, string image_full_path = "", string person = "", string tooltip_txt = "")
        {
            Rec = rec;
            ID = id;
            Pen = pen;
            Image_full_path = image_full_path;
            Person = person;
            ID = iD;
            Tooltip_txt = tooltip_txt;
        }

        public Rectangle Rec { get => rectangle; set => rectangle = value; }
        public int ID { get => id; set => id = value; }
        public string Person { get => person; set => person = value; }
        public string Image_full_path { get => image_full_path; set => image_full_path = value; }
        public Pen Pen { get => pen; set => pen = value; }
        public string Tooltip_txt { get => tooltip_txt; set => tooltip_txt = value; }

        public override string ToString()
        {
            return $"ID:{ID}\nPen: {pen.Color.ToString()}\n";
        }
    }
}
