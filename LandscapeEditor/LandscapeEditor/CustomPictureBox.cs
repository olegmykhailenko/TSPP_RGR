﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LandscapeEditor
{
    [Serializable]
    class CustomPictureBox : CustomControl
    {
        int Width { get; set; }
        int Height { get; set; }
        Point Location { get; set; }
        Image Image { get; set; }
        Color BackColor { get; set; }
        PictureBoxSizeMode SizeMode { get; set; }

        override public Control restore()
        {
            PictureBox e = new PictureBox();
            e.Width = Width;
            e.Height = Height;
            e.Location = Location;
            e.Image = Image;
            e.BackColor = BackColor;
            e.SizeMode = SizeMode;
            return e;
        }
        public CustomPictureBox(PictureBox e)
        {
            Width = e.Width;
            Height = e.Height;
            Location = e.Location;
            Image = e.Image;
            BackColor = e.BackColor;
            SizeMode = e.SizeMode;
        }
    }
}
