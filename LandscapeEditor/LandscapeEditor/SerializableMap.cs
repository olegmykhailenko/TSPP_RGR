using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LandscapeEditor
{
    [Serializable]
    class SerializableMap
    {
        int Width { get; set; }
        int Height { get; set; }
        Point Location { get; set; }
        Image Image { get; set; }
        Color BackColor { get; set; }
        List<CustomPictureBox> Controls { get; set; }

        public void restore(Map e)
        {
            e.Width = Width;
            e.Height = Height;
            e.Location = Location;
            e.Image = Image;
            e.BackColor = BackColor;
            foreach(CustomPictureBox current in Controls)
            {
                e.Controls.Add(current.restore());
            }
        }

        public SerializableMap(Map e)
        {
            Width = e.Width;
            Height = e.Height;
            Location = e.Location;
            Image = e.Image;
            BackColor = e.BackColor;
            Controls = new List<CustomPictureBox>();
            foreach(Control current in e.Controls)
            {
                Controls.Add(new CustomPictureBox((PictureBox)current));
            }
        }
    }
}
