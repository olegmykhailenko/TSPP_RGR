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
    class CustomTextBox : CustomControl
    {
        int Width { get; set; }
        int Height { get; set; }
        Point Location { get; set; }
        Color BackColor { get; set; }
        string Text { get; set; }

        override public Control restore()
        {
            TextBox e = new TextBox();
            e.Width = Width;
            e.Height = Height;
            e.Location = Location;
            e.BackColor = BackColor;
            e.Text = Text;
            return e;
        }
        public CustomTextBox(TextBox e)
        {
            Width = e.Width;
            Height = e.Height;
            Location = e.Location;
            BackColor = e.BackColor;
            Text = e.Text;
        }
    }
}
