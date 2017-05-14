using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LandscapeEditor
{
    class Tool : PictureBox //Control
    {
        //PictureBox toolImage;
        public Tool(Image image)
        {
            //toolImage = new PictureBox();
            //toolImage.Image = new Bitmap(image);
            //toolImage.SizeMode = PictureBoxSizeMode.StretchImage;
            //toolImage.Width = Cursor.Size.Width;
            //toolImage.Height = Cursor.Size.Height;
            //toolImage.Location = Cursor.Position;
            //this.Controls.Add(toolImage);

            this.Image = new Bitmap(image);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Width = Cursor.Size.Width;
            this.Height = Cursor.Size.Height;
            this.Location = Cursor.Position;
        }

        public Control createObject()
        {
            PictureBox newObject = new PictureBox();
            newObject.Image = this.Image;
            newObject.SizeMode = PictureBoxSizeMode.StretchImage;
            newObject.BackColor = Color.Transparent;
            return newObject;
        }
    }
}
