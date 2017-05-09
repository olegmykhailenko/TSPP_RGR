using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LandscapeEditor
{
    class Tool : Control
    {
        PictureBox toolImage;
        public Tool(Image image)
        {
            toolImage = new PictureBox();
            toolImage.Image = new Bitmap(image);
            toolImage.SizeMode = PictureBoxSizeMode.StretchImage;
            toolImage.Width = Cursor.Size.Width;
            toolImage.Height = Cursor.Size.Height;
            toolImage.Location = Cursor.Position;
            this.Controls.Add(toolImage);
        }

        public Control createObject()
        {
            PictureBox newObject = new PictureBox();
            newObject.Image = toolImage.Image;
            newObject.SizeMode = PictureBoxSizeMode.StretchImage;
            return newObject;
        }
    }
}
