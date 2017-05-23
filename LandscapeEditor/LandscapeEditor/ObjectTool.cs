using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LandscapeEditor
{
    class ObjectTool : AbstractTool //Control
    {
        //PictureBox toolImage;
        public ObjectTool(Image image)
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
            this.Visible = false;
            this.Location = Cursor.Position;
        }

        public override Control FactoryMethod()
        {
            CustomPictureBox newObject = new CustomPictureBox();
            newObject.Image = this.Image;
            newObject.SizeMode = PictureBoxSizeMode.StretchImage;
            newObject.BackColor = Color.Transparent;
            return newObject;
        }
    }
}
